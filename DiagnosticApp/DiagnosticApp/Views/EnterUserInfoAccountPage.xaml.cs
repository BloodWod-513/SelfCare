using DataBase.Repositories;
using DiagnosticApp.ViewModels;
using MonkeyCache.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterUserInfoAccountPage : ContentPage
    {
        private string userSex;
        public AccountViewModel ViewModel { get; private set; }
        public EnterUserInfoAccountPage(AccountViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            BindingContext = ViewModel;
        }
        public EnterUserInfoAccountPage()
        {
            InitializeComponent();
        }
        private void OnSexRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var check = (RadioButton)sender;
            userSex = (string)check.Value;
        }
        private async void СreateAccountButton_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                await DisplayAlert("Ошибка", "Заполните поля \"Имя\" и \"Фамилия\"", "Ок");
                return;
            }

            var email = Barrel.Current.Get<string>("userEmail");
            if (string.IsNullOrEmpty(email))
            {
                return;
            }

            var userRepository = new UserRepository();
            var user = await userRepository.Get(email);

            user.Name = txtName.Text;
            user.Surname = txtSurname.Text;
            user.Patronymic = txtPatronymic.Text;
            user.Weight = txtWeight.Text;
            user.DateOfBirth = dateOfBirth.Date;
            user.Sex = userSex;

            await userRepository.Update(user.Id, user);

            var page = new ProfilePage.ProfileTabbedPage();
            NavigationPage.SetHasNavigationBar(page, false);
            await Navigation.PushAsync(page);
        }
    }
}