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
    public partial class CreateDonationPage : ContentPage
    {
        public CreateDonationPage()
        {
            InitializeComponent();
        }
        HttpClient client = new HttpClient();
        public async void AddDonation(Donation data)
        {

            var dane = new Dictionary<string, string>{
                {"name",data.Name},
                {"description",data.Description},
                {"colectedmoney",data.ColectedMoney.ToString()},
                {"neededmoney",data.NeededMoney.ToString()},
            };

            var content = new FormUrlEncodedContent(dane);

            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/DonationAdd.php", content);
            string http = await response.Content.ReadAsStringAsync();
            DisplayAlert("ALERT", http, "OK");
            Navigation.PopAsync();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

            if (int.TryParse(neededmoney.Text, out var number))
            {
                Donation donation = new Donation(name.Text,description.Text,0.0,number);
                AddDonation(donation);
            }
            else
            {
                DisplayAlert("BŁĄD", "błędne dane", "OK");
            }


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