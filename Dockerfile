#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Matrix42SimpleBlogProject.Api/Matrix42SimpleBlogProject.Api.csproj", "Matrix42SimpleBlogProject.Api/"]
COPY ["Matrix42SimpleBlogProject.Application/Matrix42SimpleBlogProject.Application.csproj", "Matrix42SimpleBlogProject.Application/"]
COPY ["Matrix42SimpleBlogProject.Domain/Matrix42SimpleBlogProject.Domain.csproj", "Matrix42SimpleBlogProject.Domain/"]
COPY ["Matrix42SimpleBlogProject.Persistence/Matrix42SimpleBlogProject.Persistence.csproj", "Matrix42SimpleBlogProject.Persistence/"]
RUN dotnet restore "Matrix42SimpleBlogProject.Api/Matrix42SimpleBlogProject.Api.csproj"
COPY . .
WORKDIR "/src/Matrix42SimpleBlogProject.Api"
RUN dotnet build "Matrix42SimpleBlogProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Matrix42SimpleBlogProject.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Matrix42SimpleBlogProject.Api.dll"]