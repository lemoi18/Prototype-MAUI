﻿using CommunityToolkit.Mvvm.ComponentModel;
using Google.Apis.PeopleService.v1.Data;
using MauiApp8.Model;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp8.Services
{
    internal class Authenticated_stub : ObservableObject, IAuthenticationService
    {

        public Authenticated_stub()
        {
            User = new Test();
        }
        public Test User { get; set; }

        public Task<Test> AuthenticateAsync()
        {
            string email = "uia@test.no";
            string name = " Test stub";
            string givenName = "Test";
            string familyName = "stub";
            string picture = "test";



            return Task.FromResult(new Test
            {
                Email = email,
                Name = name,
                GivenName = givenName,
                PictureUrl = picture,
                FamilyName = familyName,
                LoginSuccessful = true
            });

        }

        public Task SignOutAsync()
        {
            return Task.FromResult(this.User = null);

        }
    }
}