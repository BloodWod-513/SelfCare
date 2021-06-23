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
    public partial class ProfileTabbedPage : TabbedPage
    {
        public ProfileTabbedPage()
        {
            InitializeComponent();
            CurrentPage = Children[2];
        }
    }
}