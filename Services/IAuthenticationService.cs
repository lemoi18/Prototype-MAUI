

namespace MauiApp8.Services
{
    public interface IAuthenticationService 
    {



        string[] Scopes { get; set; }
        Task<string> AuthenticateAsync();

        Task<List<string>> GetUserInfo(string accessToken);
        

        Task SignOutAsync();

    }
}
