using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.Views.ProfilePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
            //Application.Current.MainPage = new AnonTestPage.StartAnonTestPage(new ViewModels.AccountViewModel());
            //_ = Navigation.PushAsync(new Views.AnonTestPage.StartAnonTestPage(new ViewModels.AccountViewModel() { CreateViewModel = new ViewModels.CreateAccountViewModel(), Navigation = this.Navigation }));
        }

        private void signInButton_Clicked(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new AnonTestPage.StartAnonTestPage(new ViewModels.AccountViewModel() { CreateViewModel = new ViewModels.CreateAccountViewModel(), Navigation = this.Navigation }));
        }
    }
}