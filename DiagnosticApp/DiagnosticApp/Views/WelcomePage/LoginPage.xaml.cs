using DataBase.Repositories;
using DiagnosticApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Models;
using MonkeyCache.SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.WelcomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public AccountViewModel ViewModel { get; private set; }
        public LoginPage()
        {
            InitializeComponent();
        }
        public LoginPage(AccountViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            var email = Barrel.Current.Get<string>("userEmail");
            if (!string.IsNullOrEmpty(email))
            {
                //_ = Navigation.PushAsync(new ProfilePage());
            }
        }

        private async void Login(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                await DisplayAlert("Ошибка", "Поля пустые", "Ок");
                return;
            }
            var userRepository = new UserRepository();
            var user = await userRepository.Get(txtEmail.Text);
            if (user is null)
            {
                await DisplayAlert("Ошибка", "Такого аккаунта не существует", "Ок");
                return;
            }
            if (CheckedPassword(user, txtPassword.Text))
            {
                Barrel.Current.Add("userEmail", user.Email, TimeSpan.FromDays(7));
                Barrel.Current.Add("userPassword", user.Password, TimeSpan.FromDays(7));

                var page = new Views.ProfilePage.ProfileTabbedPage();
                NavigationPage.SetHasNavigationBar(page, false);
                await Navigation.PushAsync(page);
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный пароль", "Ок");
            }
        }

        private bool CheckedPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
        }

        private void MoveToRegistration(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new RegistrationPage(new AccountViewModel() { CreateViewModel = new ViewModels.CreateAccountViewModel(), Navigation = this.Navigation }));
        }
    }
}