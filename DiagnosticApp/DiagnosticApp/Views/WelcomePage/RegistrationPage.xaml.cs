using DataBase.Repositories;
using DiagnosticApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Models;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.WelcomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {

        public AccountViewModel ViewModel { get; private set; }

        public RegistrationPage(AccountViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            BindingContext = ViewModel;
        }

        private async void Registration(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                await DisplayAlert("Ошибка", "Поля пустые", "Ок");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                await DisplayAlert("Ошибка", "Пароли не совпадают", "ОК");
                return;
            }

            var userRepository = new UserRepository();
            var user = await userRepository.Get(txtEmail.Text);
            if (user != null)
            {
                await DisplayAlert("Ошибка", "Такой аккаунт уже существует", "ОК");
                return;
            }

            var passwordHasher = new PasswordHasher<User>();

            user = new User();
            user.Email = txtEmail.Text;
            user.Password = passwordHasher.HashPassword(user, txtPassword.Text);

            await userRepository.Add(user);

            Barrel.Current.Add("userEmail", user.Email, TimeSpan.FromDays(7));
            Barrel.Current.Add("userPassword", txtPassword.Text, TimeSpan.FromDays(7));

            _ = Navigation.PushAsync(new Views.EnterUserInfoAccountPage(ViewModel));
        }
    }
}