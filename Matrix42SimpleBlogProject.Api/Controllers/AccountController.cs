using Matrix42SimpleBlogProject.Application.Identity;
using Matrix42SimpleBlogProject.Application.Model.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Matrix42SimpleBlogProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;
    public AccountController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
    {
        return Ok(await authenticationService.AuthenticateAsync(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
    {
        return Ok(await authenticationService.RegisterAsync(request));
    }
}