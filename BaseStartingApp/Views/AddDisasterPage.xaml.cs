using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using BaseStartingApp.Views;
using Xamarin.Forms.Maps;
namespace BaseStartingApp
{
    public partial class AddDisasterPage : ContentPage
    {
        public static Position polozenie; 
        public AddDisasterPage()
        {
            InitializeComponent();
        }
        HttpClient client = new HttpClient();
        public async void AddDisaster(Disaster data)
        {

            var dane = new Dictionary<string, string> {
                {"name",data.Name},
                {"description",data.Description},
                {"location",data.Location},
                {"volunteers_number",data.volunteers_number.ToString()},
                {"volunteers_number_needed",data.volunteers_number_needed.ToString()},
                {"user",data.user.ToString()}

            };
            

            var content = new FormUrlEncodedContent(dane);

            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/DisastersAdd.php", content);
            string http = await response.Content.ReadAsStringAsync();
            DisplayAlert("ALERT", http, "OK");
            Navigation.PopAsync();
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

        private void buttonMapa(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            Navigation.PushAsync(new MainPage());
        }

        private async void buttonzdarzenia(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            Navigation.PushAsync(new AllDisastersPage());


        }

        private void Buttond_Clicked(object sender, EventArgs e)
        {
            Disaster disaster;
            if (int.TryParse(volunteers_number_needed.Text, out var number))
            {
                if (polozenie != null)
                {
                    disaster = new Disaster(name.Text, description.Text, polozenie.Latitude.ToString() +","+ polozenie.Longitude.ToString(), 0, number, App.LoggedUser.email);
                
                }
                else
                {
                    disaster = new Disaster(name.Text, description.Text, "null", 0, number, App.LoggedUser.email);
                };
                AddDisaster(disaster);
            }
            else
            {
                DisplayAlert("BŁĄD", "błędne dane", "OK");
            }


        }
    }
}
