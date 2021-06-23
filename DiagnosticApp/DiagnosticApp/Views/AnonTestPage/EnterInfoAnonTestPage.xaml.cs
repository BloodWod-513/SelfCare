using DataBase.Repositories;
using DiagnosticApp.ViewModels;
using MonkeyCache.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.Views.AnonTestPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterInfoAnonTestPage : ContentPage
    {
        public AccountViewModel ViewModel { get; private set; }
        public EnterInfoAnonTestPage(AccountViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            BindingContext = ViewModel;
            txtWeight.Text = "50";
        }
        private void Slider_WeightValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (txtWeight != null && string.Empty != txtWeight.Text)
            {
                int value = (int)e.NewValue;
                txtWeight.Text = string.Format("{0}", value);
            }
        }
        private void WeightEntry_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (txtWeight != null && string.Empty != txtWeight.Text)
                weightSlider.Value = int.Parse(txtWeight.Text);
        }
        private void OnSexRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var check = (RadioButton)sender;
            ViewModel.Sex = (string)check.Value;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            var email = Barrel.Current.Get<string>("userEmail");
            if (!string.IsNullOrEmpty(email))
            {
                var userRepository = new UserRepository();
                var user = await userRepository.Get(email);

                if (user.Sex != ViewModel.Sex)
                {
                    user.Sex = ViewModel.Sex;
                }

                if (user.Weight != txtWeight.Text)
                {
                    user.Weight = txtWeight.Text;
                }

                if (user.DateOfBirth != dateOfBirth.Date)
                {
                    user.DateOfBirth = dateOfBirth.Date;
                }

                await userRepository.Update(user.Id, user);
            }

            await Navigation.PushAsync(new TestAnonTestPage());
        }
    }
}