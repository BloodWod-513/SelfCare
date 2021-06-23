using DataBase.Repositories;
using DiagnosticApp.Views;
using Microsoft.AspNetCore.Identity;
using Models;
using MonkeyCache.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DiagnosticApp.WelcomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            CheckLogin();
        }
        private async void CheckLogin()
        {
            string email = Barrel.Current.Get<string>("userEmail");

            if (!string.IsNullOrEmpty(email))
            {
                var userRepository = new UserRepository();
                var user = await userRepository.Get(email);
                string password = Barrel.Current.Get<string>("userPassword");

                if (CheckedPassword(user, password))
                {
                    //Application.Current.MainPage = new Views.ProfilePage.ProfileTabbedPage();
                    // Переход на страницу пользователя
                }

                return;
            }
        }
        private bool CheckedPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
        }
        private void OnAnonTestButtonClicked(object sender, System.EventArgs e)
        {
            _ = Navigation.PushAsync(new Views.AnonTestPage.StartAnonTestPage(new ViewModels.AccountViewModel() { CreateViewModel = new ViewModels.CreateAccountViewModel(), Navigation = this.Navigation }));
        }
    }
}