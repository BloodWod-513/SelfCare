using DataBase.Repositories;
using Models;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = "DiagnosticApp";
            var page = new WelcomePage.MainWelcomePage();
            NavigationPage.SetHasNavigationBar(page, false);
            MainPage = new NavigationPage(page);
            //MainPage = new Views.AnonTestPage.EnterInfoAnonTestPage(new ViewModels.AccountViewModel());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
