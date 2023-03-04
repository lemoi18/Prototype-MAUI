using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MauiApp8.Model
{
    public class LoginRespons
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }

        public string TokenType { get; set; }
        public string Email { get; set; }


    }
}
