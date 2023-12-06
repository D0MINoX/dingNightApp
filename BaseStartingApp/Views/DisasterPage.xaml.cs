using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace BaseStartingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DisasterPage : ContentPage
	{
		public DisasterPage(Disaster e)
		{

			InitializeComponent ();
			id.Text = e.Id.ToString();
			title.Text = e.Name;
			description.Text = e.Description;
			location.Text = e.Location;
			volunteers_number.Text = e.volunteers_number.ToString();
			volunteers_number_needed.Text=e.volunteers_number_needed.ToString();
			user.Text = e.user.ToString();

			if(App.LoggedUser != null)
			{
				if(e.user == App.LoggedUser.email.ToString())
				{
					delete.IsVisible = true;
				}
			}
		}
		public async void IncreaseDisaster()
		{
			HttpClient client = new HttpClient ();
			var keys = new Dictionary<string, string> {
				{ "id",id.Text},
				{"volunteers_number",volunteers_number.Text }
				};
            var content = new FormUrlEncodedContent(keys);
            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/DisastersIncrease.php", content);
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
           IncreaseDisaster();
            DisplayAlert("DZIĘKUJEMY", "Dziękujemy za zgłoszenie, skontaktujemy się z tobą telefonicznie", "OK");

        }
		HttpClient client = new HttpClient();

        public async void DeleteDisaster()
        {
            var keys = new Dictionary<string, string> {
        { "id",id.Text}
		};
            var content = new FormUrlEncodedContent(keys);
            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/DeleteDisaster.php", content);
        
            string http = await response.Content.ReadAsStringAsync();
            DisplayAlert("ALERT", http, "OK");
            Navigation.PopAsync();
        }

        private void delete_Clicked(object sender, EventArgs e)
        {
			DeleteDisaster();
        }
    }
}