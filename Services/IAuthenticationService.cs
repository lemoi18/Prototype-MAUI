using MauiApp8.Model;


namespace MauiApp8.Services
{
    public interface IAuthenticationService 
    {


        Test User { get; set; }

        Task<Test> AuthenticateAsync();

        //Task<Test> GetUserInfo(string accessToken);


        Task SignOutAsync();

    }
}
