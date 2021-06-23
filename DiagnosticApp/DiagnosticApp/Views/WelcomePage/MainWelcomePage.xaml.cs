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
    public partial class MainWelcomePage : TabbedPage
    {
        public MainWelcomePage()
        {
            InitializeComponent();
            CurrentPage = Children[1];
        }
    }
}