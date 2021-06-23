using HtmlAgilityPack;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnosticApp.Views.AnonTestPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultTestPage : ContentPage
    {
        public ObservableCollection<Doctor> DoctorsList { get; set; } = new ObservableCollection<Doctor>();

        public ResultTestPage()
        {
            InitializeComponent();
            GetDoctors("Терапевт");
            doctorList.ItemsSource = DoctorsList;
            BindingContext = BindingContext;
        }

        public void GetDoctors(string specialization)
        {
            var url = @"https://prodoctorov.ru";
            var web = new HtmlWeb();
            var doc = web.Load(url + "/ekaterinburg/vrach/");
            var root = doc.DocumentNode.SelectNodes("//a[contains(@class, 'p-doctors-list-page__tab-item-link')]").Elements().ToList();

            var node = root.Where(x => x.InnerText == specialization).FirstOrDefault();

            var specializationUrl = url + node.ParentNode.Attributes.Where(x => x.Name.Equals("href")).FirstOrDefault().Value;

            doc = web.Load(specializationUrl);
            root = doc.DocumentNode.SelectNodes("//div[@class='b-doctor-card b-doctor-card_special-placement']").Take(3).ToList();

            foreach (var item in root)
            {
                var doctor = new Doctor();

                var nameNode = item.SelectSingleNode("//span[contains(@class, 'b-doctor-card__name-surname')]");
                var name = nameNode.InnerText;
                var doctorUrl = nameNode.ParentNode.GetAttributeValue("href", "");
                var spec = item.SelectSingleNode("//div[contains(@class, 'b-doctor-card__spec')]").InnerText.Trim();
                var exp = item.SelectSingleNode("//div[contains(@class, 'b-doctor-card__experience-years')]").InnerText.Trim();
                var geo = item.SelectSingleNode("//select[contains(@class, 'b-doctor-card__lpu-select')]");

                var prices = item.SelectNodes("//div[@class='b-type-tabs__tab']").Elements().ToList();

                foreach (var price in prices)
                {
                    var temp = item.SelectSingleNode("//div[contains(@class, 'ui-text ui-text_body-2')]").InnerText.Trim();
                    var temp2 = item.SelectSingleNode("//div[contains(@class, 'ui-text ui-text_subtitle-1')]").InnerText.Trim();

                    doctor.Prices.Add(temp + " " + temp2);
                }

                doctor.Prices = doctor.Prices.Distinct().ToList();
                doctor.Name = name;
                doctor.Url = url + doctorUrl;
                doctor.Specialization = spec;
                doctor.WorkExperience = exp;
                doctor.Geolocation = geo.GetAttributeValue("placeholder", "") + " (" + geo.GetAttributeValue("data-adit-text", "") + ")";

                item.Remove();
                DoctorsList.Add(doctor);
            }
        }
    }
}