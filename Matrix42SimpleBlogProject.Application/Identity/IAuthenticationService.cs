using Matrix42SimpleBlogProject.Application.Model.Authentication;

namespace Matrix42SimpleBlogProject.Application.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
