using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BaseStartingApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BaseStartingApp.Views;

namespace BaseStartingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllDisastersPage : ContentPage
	{
        ObservableCollection<Disaster> disaster_colection = new ObservableCollection<Disaster>();
        HttpClient client = new HttpClient();
        public async Task Search()
        {
            Color butcolor = Color.FromRgb(0, 204, 153);
            Color tekstkolor = Color.White;
            disaster_colection.Clear();
            string disasters_data = await client.GetStringAsync("https://unpurged-knives.000webhostapp.com/ShowDisaster.php");
			List<Disaster> disaster_list = JsonConvert.DeserializeObject<List<Disaster>>(disasters_data);
			
			foreach(Disaster disaster in disaster_list)
			{ 
				disaster_colection.Add(disaster);
				
            }
            lista.IsRefreshing = false;

            if (App.LoggedUser != null)
            {

                button.IsVisible = true;
              
              
            
            }
        }
        public AllDisastersPage()
		{
			
			InitializeComponent ();

            lista.ItemsSource = disaster_colection;
            Search();
        }

        private void Button_Clicked()
        {
            Navigation.PushAsync(new AddDisasterPage());
        }

        private void lista_Refreshing(object sender, EventArgs e)
        {
            Search();
           
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

        private void button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddDisasterPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string id = ((StackLayout)sender).Children.OfType<Label>().FirstOrDefault().Text;
            Disaster disaster = disaster_colection.First(x => x.Id == Int32.Parse(id));
            Navigation.PushAsync(new DisasterPage(disaster));
        }
    }
}