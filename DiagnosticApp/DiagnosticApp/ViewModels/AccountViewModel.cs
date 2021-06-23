using System.ComponentModel;
using DiagnosticApp.Models;
using Xamarin.Forms;
using System;

namespace DiagnosticApp.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public Account Account { get; private set; }
        CreateAccountViewModel cavm;
        public DateTime minDate = new DateTime(DateTime.Now.Year - 80, 01, 01);
        public DateTime maxDate = new DateTime(DateTime.Now.Year - 10, 01, 01);
        public AccountViewModel()
        {
            Account = new Account();
        }
        public DateTime MinDate
        {
            get { return minDate; }
            set
            {
                if (minDate != value)
                {
                    minDate = value;
                    OnPropertyChanged("minDate");
                }
            }
        }
        public DateTime MaxDate
        {
            get { return maxDate; }
            set
            {
                if (maxDate != value)
                {
                    maxDate = value;
                    OnPropertyChanged("MaxDate");
                }
            }
        }
        public CreateAccountViewModel CreateViewModel
        {
            get { return cavm; }
            set
            {
                if (cavm != value)
                {
                    cavm = value;
                    OnPropertyChanged("CreateViewModel");
                }
            }
        }
        public string Name
        {
            get => Account.Name;
            set
            {
                if (Account.Name != value)
                {
                    Account.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Surname
        {
            get => Account.Surname;
            set
            {
                if (Account.Surname != value)
                {
                    Account.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
        public string Patronymic
        {
            get => Account.Patronymic;
            set
            {
                if (Account.Patronymic != value)
                {
                    Account.Patronymic = value;
                    OnPropertyChanged("Patronymic");                   
                }
            }
        }
        public string Login
        {
            get => Account.Login;
            set
            {
                if (Account.Login != value)
                {
                    Account.Login = value.Trim();
                    OnPropertyChanged("Login");
                }
            }
        }
        public string Password
        {
            get => Account.Password;
            set
            {
                if (Account.Password != value)
                {
                    Account.Password = value.Trim();
                    OnPropertyChanged("Password");
                }
            }
        }
        public string ConfirmPassword
        {
            get => Account.ConfirmPassword;
            set
            {
                if (Account.ConfirmPassword != value)
                {
                    Account.ConfirmPassword = value.Trim();
                    OnPropertyChanged("ConfirmPassword");
                }
            }
        }
        public string Sex
        {
            get => Account.Sex;
            set
            {
                if (Account.Sex != value)
                {
                    Account.Sex = value;
                    OnPropertyChanged("Sex");
                }
            }
        }
        public string Weight
        {
            get => Account.Weight;
            set
            {
                if (Account.Weight != value)
                {
                    Account.Weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        public int Age
        {
            get => Account.Age;
            set
            {
                if (Account.Age != value)
                {
                    Account.Age = value;
                    OnPropertyChanged("Age");
                }
            }
        }
        public bool IsNullloginAndPas => !string.IsNullOrWhiteSpace(Login) &&
                    !string.IsNullOrWhiteSpace(Password);
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
