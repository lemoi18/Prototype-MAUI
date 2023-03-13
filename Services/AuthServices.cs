using Google.Apis.Auth;
using MauiApp8.Model;
using Microsoft.Maui.Authentication;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Xml.Linq;
using System.Text.Json;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.PeopleService.v1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.PeopleService.v1.Data;
using System.IdentityModel.Tokens.Jwt;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Oauth2.v2;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.IdentityModel.Tokens;

namespace MauiApp8.Services
{
    public class AuthService : ObservableObject, IAuthenticationService
    {
        public AuthService()
        {
            
                client_id = "438312542461-555tgjs158r5jrj1vmvgfvrlccblg89a.apps.googleusercontent.com";
                auth_uri = "https://accounts.google.com/o/oauth2/auth";
                token_uri = "https://oauth2.googleapis.com/token";
                auth_url = $"{this.auth_uri}?response_type=code" +
                    $"&redirect_uri=com.companyname.mauiapp8://" +
                    $"&client_id={this.client_id}" +
                    $"&scope=https://www.googleapis.com/auth/userinfo.profile" +
                    $"&include_granted_scopes=true" +
                    $"&state=state_parameter_passthrough_value";

                callback_url = "com.companyname.mauiapp8://";


        }

        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string client_id { get; set; }
        public string auth_url { get; set; }
        public string callback_url { get; set; }

        public Test User { get; set; }


        public string[] Scopes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }






        public async Task<Test> GetUserInfo(string accessToken)
        {
            
            

                // Verify the access token using the Google API client library
                //var payload = await GoogleJsonWebSignature.ValidateAsync(accessToken);

                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var token = handler.ReadJwtToken(accessToken);


                    // Extract the email claim
                    string email = token.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                    string name = token.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    string givenName = token.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
                    string picture = token.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
                    string FamilyName = token.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;


                    // Retrieve the user's name from the payload and return it


                    Test user = new Test();

                    user.Email = email;
                    user.Name = name;
                    user.GivenName = givenName;
                    user.FamilyName = FamilyName;
                    user.PictureUrl = picture;
                    user.LoginSuccessful = true;

                    return  user;

                }
                catch (SecurityTokenException ex)
                {

                Console.WriteLine($"Token validation error: {ex.Message}");
                 User.LoginSuccessful= false;
            }



            return null;

        }



        public async Task<Test> AuthenticateAsync()
        {
            var scheme = "Google"; // Apple, Microsoft, Google, Facebook, etc.
            var authUrlRoot = "https://accounts.google.com/o/oauth2/auth";
            WebAuthenticatorResult result = null;
            

           
                // Web Authentication flow
                var authUrl = new Uri($"{this.auth_url}{scheme}");
                var callbackUrl = new Uri("com.companyname.mauiapp8://");

                var extra = new KeyValuePair<string, string>("client_id", this.client_id);

                result = await WebAuthenticator.Default.AuthenticateAsync(authUrl, callbackUrl);

            try
            {

                var codeToken = result.Properties["code"];

                var parameters = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string,string>("grant_type","authorization_code"),
                        new KeyValuePair<string,string>("client_id",this.client_id),
                        new KeyValuePair<string,string>("redirect_uri",this.callback_url),
                        new KeyValuePair<string,string>("code",codeToken),
                    });



                HttpClient client = new HttpClient();
                var accessTokenResponse = await client.PostAsync(this.token_uri, parameters);

                LoginRespons loginResponse;


                if (accessTokenResponse.IsSuccessStatusCode)
                {



                    var data = await accessTokenResponse.Content.ReadAsStringAsync();
                    loginResponse = JsonSerializer.Deserialize<LoginRespons>(data);
                    return await GetUserInfo(loginResponse.id_token); 
                }
                else
                {
                    User.Name = "No user";
                    User.LoginSuccessful = false;

                    return User;
                }
               
            }
            catch (TaskCanceledException e)
            {

                User.Name = "TaskCanceled "+e;
                User.LoginSuccessful = false;
                return User;


            }




        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }





}

