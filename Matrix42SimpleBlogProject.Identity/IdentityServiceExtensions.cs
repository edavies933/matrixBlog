using System.Text;
using Matrix42SimpleBlogProject.Application.Identity;
using Matrix42SimpleBlogProject.Application.Model.Authentication;
using Matrix42SimpleBlogProject.Identity.Models;
using Matrix42SimpleBlogProject.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Npgsql;

namespace Matrix42SimpleBlogProject.Identity;

public static class IdentityServiceExtensions
{
    private static string? _connectionString;
    public const string DatabaseConfigurationKey = "DatabaseConfig";

    public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        var databaseConfig = configuration.GetSection(DatabaseConfigurationKey).Get<DatabaseConfig>();
        if (databaseConfig is null)
        {
            throw new NullReferenceException("Can't find db configuration");
        }

        if (_connectionString is null)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseConfig.Host,
                Port = databaseConfig.Port,
                Database = databaseConfig.Database,
                Username = databaseConfig.User,
                SearchPath = databaseConfig.SearchPath,
                Password = databaseConfig.Password,
                TrustServerCertificate = databaseConfig.TrustServerCertificate,
                SslMode = databaseConfig.SslMode,
                MaxPoolSize = databaseConfig.MaxPoolSize
            };
            _connectionString = builder.ConnectionString;
        }

        services.AddDbContext<BlogProjectIdentityDbContext>(options => options.UseNpgsql(_connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<BlogProjectIdentityDbContext>().AddDefaultTokenProviders();

        services.AddTransient<IAuthenticationService, AuthenticationService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? throw new InvalidOperationException()))
                };

                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject("401 Not authorized");
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject("403 Not authorized");
                        return context.Response.WriteAsync(result);
                    },
                };
            });
    }
}



