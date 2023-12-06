using BaseStartingApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseStartingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonationPage : ContentPage
    {
        double collectedmoney;
        public DonationPage(Donation e)
        {
            InitializeComponent();
            id.Text=e.Id.ToString();
            title.Text=e.Name.ToString();
            description.Text=e.Description.ToString();
            colectedmoney.Text+=e.ColectedMoney.ToString();
            collectedmoney = e.ColectedMoney;
            neededmoney.Text+=e.NeededMoney.ToString();
            double ratio = e.ColectedMoney/e.NeededMoney;
            progress.Progress = ratio;    
        }
        public async void DonationAdd()
        {
            string result = await DisplayPromptAsync("DOTACJA", "Wpisz kwotę:", "wyślij", "anuluj", "kwota", 20, Keyboard.Numeric, "20");
            if (!String.IsNullOrEmpty(result))
            {
                Donation(result);
            }
        }
        
            public async void Donation(string result)
        {

            HttpClient client = new HttpClient();
            
            double money1 = double.Parse(result);
           
            double money3 = money1+collectedmoney ;
           
           var keys = new Dictionary<string, string> {
                { "id",id.Text},
              {"colectedmoney", money3.ToString() }
                 };
           var content = new FormUrlEncodedContent(keys);
            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/DonationIncrease.php", content);
            string http = await response.Content.ReadAsStringAsync();
            Navigation.PopAsync();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            
            DonationAdd();
        }

        private void buttonlogin(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            if (App.LoggedUser == null)
                Navigation.PushAsync(new LoginPage());
            else
                Navigation.PushAsync(new UpdatePage());
        }
        private void buttonDonation(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            Navigation.PushAsync(new AllDonationsPage());
        }

        private void buttonzdarzenia(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            Navigation.PushAsync(new AllDisastersPage());
        }

        private void buttonMapa(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            Navigation.PushAsync(new MainPage());
        }
    }
}