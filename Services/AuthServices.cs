using Google.Apis.Auth;
using MauiApp8.Model;
using Microsoft.Maui.Authentication;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Xml.Linq;
using System.Text.Json;
using System.Linq;

namespace MauiApp8.Services
{
    public class AuthService
    {

        public AuthService(string external_Service)
        {
            if (external_Service == "google")
            {

                client_id = "438312542461-555tgjs158r5jrj1vmvgfvrlccblg89a.apps.googleusercontent.com";
                auth_uri = "https://accounts.google.com/o/oauth2/auth";
                token_uri = "https://oauth2.googleapis.com/token";
                auth_url = $"{this.auth_uri}?response_type=code" +
                    $"&redirect_uri=com.companyname.mauiapp8://" +
                    $"&client_id={this.client_id}" +
                    $"&scope=https://www.googleapis.com/auth/userinfo.email" +
                    $"&include_granted_scopes=true" +
                    $"&state=state_parameter_passthrough_value";

                callback_url = "com.companyname.mauiapp8://";



            }

        }

        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string client_id { get; set; }
        public string auth_url { get; set; }
        public string callback_url { get; set; }

        public WebAuthenticatorOptions webAuthenticatorOptions { get; set; }

        public string[] Scopes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }




    

    public async Task<List<string>> GetUserInfo(string accessToken)
    {
        try
        {
            // Verify the access token using the Google API client library
            var payload = await GoogleJsonWebSignature.ValidateAsync(accessToken);

                
                // Retrieve the user's name from the payload and return it
                return new List<string> { payload.Email, payload.Name };

            }
            catch (Exception ex)
        {
            Console.WriteLine($"Error getting user name: {ex.Message}");
            return null;
        }
    }



    public async Task<string> AuthenticateAsync()
        {
            var scheme = "Google"; // Apple, Microsoft, Google, Facebook, etc.
            var authUrlRoot = "https://accounts.google.com/o/oauth2/auth";
            var client_id = "438312542461-555tgjs158r5jrj1vmvgfvrlccblg89a.apps.googleusercontent.com";
            WebAuthenticatorResult result = null;
            User user= null;

            if (scheme.Equals("Apple")
                && DeviceInfo.Platform == DevicePlatform.iOS
                && DeviceInfo.Version.Major >= 13)
            {
                // Use Native Apple Sign In API's
                result = await AppleSignInAuthenticator.AuthenticateAsync();
            }
            else
            {
                // Web Authentication flow
                var authUrl = new Uri($"{this.auth_url}{scheme}");
                var callbackUrl = new Uri("com.companyname.mauiapp8://");

                var extra = new KeyValuePair<string, string>("client_id", this.client_id);


                result = await WebAuthenticator.Default.AuthenticateAsync(authUrl, callbackUrl);
            }

            var authToken = string.Empty;

            if (result.Properties.TryGetValue("name", out string name) && !string.IsNullOrEmpty(name))
                authToken += $"Name: {name}{Environment.NewLine}";

            if (result.Properties.TryGetValue("email", out string email) && !string.IsNullOrEmpty(email))
                authToken += $"Email: {email}{Environment.NewLine}";

            // Note that Apple Sign In has an IdToken and not an AccessToken
            authToken += result?.AccessToken ?? result?.IdToken;

            
            

         



            try
            {


               
                var codeToken = result.Properties["code"];

                var parameters = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string,string>("grant_type","authorization_code"),
                        new KeyValuePair<string,string>("client_id",this.client_id),
                        new KeyValuePair<string,string>("redirect_uri",callback_url),
                        new KeyValuePair<string,string>("code",codeToken),
                    });



                HttpClient client = new HttpClient();
                var accessTokenResponse = await client.PostAsync(this.token_uri, parameters);

                LoginRespons loginResponse;
                

                if (accessTokenResponse.IsSuccessStatusCode)
                {
                    var data = await accessTokenResponse.Content.ReadAsStringAsync();
                    Dictionary<string,string> ss = data.ToDictionary();
                    loginResponse = JsonSerializer.Deserialize<LoginRespons>(data);
                    await GetUserInfo(loginResponse.AccessToken);

                    return "Nice";
                }
                else { return "HttpClient 404"; }
            }
            catch (TaskCanceledException e)
            {
                // Use stopped auth
                return "Task Canceled" + e;
            }




        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }





}

