using MauiApp8.Model;


namespace MauiApp8.Services
{
    public interface IAuthenticationService 
    {


        Account User { get; set; }

        Task<Account> AuthenticateAsync();

        //Task<Test> GetUserInfo(string accessToken);


        Task SignOutAsync();

    }
}
