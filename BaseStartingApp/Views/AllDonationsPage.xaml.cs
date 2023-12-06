using BaseStartingApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseStartingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllDonationsPage : ContentPage
    {
        ObservableCollection<Donation> donation_colection = new ObservableCollection<Donation>();
        HttpClient client = new HttpClient();
        public async void Search()
        {
            donation_colection.Clear();
            string donation_data = await client.GetStringAsync("https://unpurged-knives.000webhostapp.com/ShowDonations.php");
            List<Donation> donation_list = JsonConvert.DeserializeObject<List<Donation>>(donation_data);

            foreach (Donation donation in donation_list)
            {
                if(donation.ColectedMoney<=donation.NeededMoney) donation_colection.Add(donation);

            }
            lista.IsRefreshing = false;
        }
        public AllDonationsPage()
        {
            InitializeComponent();
            lista.ItemsSource = donation_colection;
            Search();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateDonationPage());
        }
        private void lista_Refreshing(object sender, EventArgs e)
        {
            Search();

        }
        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Donation disaster = donation_colection[e.SelectedItemIndex];
            Navigation.PushAsync(new DonationPage(disaster));
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