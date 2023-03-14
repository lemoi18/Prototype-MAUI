using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Authentication;
using Microsoft.IdentityModel.Tokens;
using MauiApp8.Model;
using System.Text.Json;


namespace MauiApp8.Services
{
    internal class RefactoredGoogleAuth : ObservableObject, IAuthenticationService
    {
        

        public string auth_uri { get; set; }
        public string scheme { get; set; }

        public string token_uri { get; set; }
        public string client_id { get; set; }
        public string auth_url { get; set; }
        public string callback_url { get; set; }
        

        public Test User { get; set; }

        public RefactoredGoogleAuth()
        {
            var scheme = "Google";


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


            User = new Test();
        }

        public async Task<Test> AuthenticateAsync()
        {
            var authUrl = new Uri($"{this.auth_url}{scheme}");
            var callbackUrl = new Uri(this.callback_url);

            WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(authUrl, callbackUrl);


            var codeToken = result.Properties["code"];
            var parameters = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","authorization_code"),
                new KeyValuePair<string,string>("client_id", this.client_id),
                new KeyValuePair<string,string>("redirect_uri", this.callback_url),
                new KeyValuePair<string,string>("code", codeToken),
            });

            using var client = new HttpClient();
            var accessTokenResponse = await client.PostAsync(token_uri, parameters);

            if (!accessTokenResponse.IsSuccessStatusCode)
            {
                throw new AuthenticationException("Failed to get access token");
            }

            var data = await accessTokenResponse.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginRespons>(data);
            var accessToken = loginResponse.id_token;

            try
            {
                return ValidateAccessToken(accessToken);
            }
            catch (Exception e)
            {
                throw new AuthenticationException("Failed to validate access token", e);
            }
        }

        public async Task SignOutAsync()
        {
            this.User = null;
            
        }

        private Test ValidateAccessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            var email = GetTokenClaim(token, "email");
            var name = GetTokenClaim(token, "name");
            var givenName = GetTokenClaim(token, "given_name");
            var picture = GetTokenClaim(token, "picture");
            var familyName = GetTokenClaim(token, "family_name");

            return new Test
            {
                Email = email,
                Name = name,
                GivenName = givenName,
                PictureUrl = picture,
                FamilyName = familyName,
                LoginSuccessful = true
            };
        }

        private string GetTokenClaim(JwtSecurityToken token, string claimType)
        {
            return token.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }

    }
}