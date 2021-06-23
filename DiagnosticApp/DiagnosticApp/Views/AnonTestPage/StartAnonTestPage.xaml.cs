using DiagnosticApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.Views.AnonTestPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartAnonTestPage : ContentPage
    {
        public AccountViewModel ViewModel { get; private set; }
        public StartAnonTestPage(AccountViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            BindingContext = ViewModel;
        }
        private void OnEnterInfoAnontTestPageButtonClicked(object sender, System.EventArgs e)
        {
            if (!Agreement.IsChecked)
            {
                Agreement.BackgroundColor = Color.Red;
                return;
            }
            _ = Navigation.PushAsync(new EnterInfoAnonTestPage(ViewModel));
        }
    }
}