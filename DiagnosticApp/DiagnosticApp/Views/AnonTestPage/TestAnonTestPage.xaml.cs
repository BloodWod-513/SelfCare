using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.Views.AnonTestPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestAnonTestPage : ContentPage
    {
        public ObservableCollection<Symptoms> SymptomList { get; set; } = new ObservableCollection<Symptoms>();

        private readonly string _uri = "https://lod.medlinx.online/terminology/api/v1/fhir/ValueSet/$expand?url=http://terminology.medlinx.online/ValueSet/helzy-search-vs";
        private readonly string  _display_lang = "displayLanguage=ru";
        private readonly string _offset = "offset=0";
        private readonly string _count = "count=6";
        private readonly string _filter = "filter=";
        private string _symptom;
        static object locker = new object();
        public TestAnonTestPage()
        {
            InitializeComponent();
            //SymptomList.Add("123");
            this.BindingContext = this;
        }

        private void ButtonClearEntry_Clicked(object sender, EventArgs e)
        {
            txtName.Text = "";
        }
        private void SymptomListRequest(string request)
        {
            lock(locker)
            {
                if (request[^1] == '=')
                {
                    return;
                }
                System.Net.WebRequest req = System.Net.WebRequest.Create(request);
                req.Timeout = 10 * 1000;
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.Stream stream = resp.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                string Out = sr.ReadToEnd();
                sr.Close();
                stream.Close();
                try
                {
                    Observations results = JsonConvert.DeserializeObject<Observations>(Out);
                    if (results.Expansion.Contains is null)
                    {
                        return;
                    }
                    IList<Container> symptoms = results.Expansion.Contains;
                    SymptomList.Clear();
                    foreach (Container symp in symptoms)
                    {
                        SymptomList.Add(new Symptoms() { Name = symp.Display });//Название симптома находится в display. Необходимо также сохранять code
                    }
                    //Thread.Sleep(2000);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            //SymptomList.Clear();
            _symptom = txtName.Text;
            string temp = _filter + _symptom;
            string request = String.Join("&", _uri, _display_lang, _offset, _count, temp);
            await Task.Run(() => SymptomListRequest(request));
        }

        private void GoToResult(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new ResultTestPage();
            Navigation.PushAsync(new ResultTestPage());
        }
    }
    public class Symptoms
    {
        public string Name { get; set; }
    }
    public class Observations
    {
        public string ResourceType { get; set; }
        public string Url { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public Expansions Expansion { get; set; }

    }
    public class Expansions
    {
        public string Identifier { get; set; }
        public string Timestamp { get; set; }
        public string Offset { get; set; }

        public IList<Container> Contains { get; set; }
    }
    public class Container
    {
        public string System { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
    }
}