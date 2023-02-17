using MauiApp8.Model;

using System.Text.Json;

namespace MauiApp8.Services
{
    public class AuthService 
    {

        public AuthService(string external_Service) 
        {
            if (external_Service == "google")
            {

                auth_uri = "https://accounts.google.com/o/oauth2/auth";
                token_uri = "https://oauth2.googleapis.com/token";
                auth_uri = $"{this.auth_uri}?response_type=code" +
                    $"&redirect_uri=com.maui.login://" +
                    $"&client_id={this.client_id}" +
                    $"&scope=https://www.googleapis.com/auth/userinfo.email" +
                    $"&include_granted_scopes=true" +
                    $"&state=state_parameter_passthrough_value";

                callback_url = "com.maui.login://";

                

            }

        }
       
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string client_id { get; set; }
        public string auth_url { get; set; }
        public string callback_url { get; set; }

        public WebAuthenticatorOptions webAuthenticatorOptions { get; set; }

        public string[] Scopes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public async Task<string> AuthenticateAsync()
        {
            

                try
                {
                var response = await WebAuthenticator.AuthenticateAsync(new WebAuthenticatorOptions()
                {
                    Url = new Uri(this.auth_url),
                    CallbackUrl = new Uri(this.callback_url)
                });
                    

                    var codeToken = response.Properties["code"];

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

                        loginResponse = JsonSerializer.Deserialize<LoginRespons>(data);
                    return "Nice";
                    }
                    else { return "HttpClient 404"; }
                }
                catch (TaskCanceledException e)
                {
                // Use stopped auth
                    return "Task Canceled";
                }




            }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
      

        

   
    }   

