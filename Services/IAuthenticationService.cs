

namespace MauiApp8.Services
{
    public interface IAuthenticationService 
    {



        string[] Scopes { get; set; }
        Task<string> AuthenticateAsync();
        Task SignOutAsync();

    }
}
