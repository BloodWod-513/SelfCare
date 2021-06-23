using DataBase.Repositories;
using DiagnosticApp.WelcomePage;
using Models;
using MonkeyCache.SQLite;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.Views.ProfilePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {


        public ProfilePage()
        {
            InitializeComponent();
        }



        private void Logout(object sender, System.EventArgs e)
        {
            Barrel.Current.EmptyAll();
            var page = new MainWelcomePage();
            NavigationPage.SetHasNavigationBar(page, false);
            Application.Current.MainPage = new NavigationPage(page);
        }
    }
}