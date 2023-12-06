using BaseStartingApp.Models;
using BaseStartingApp.ViewModels;
using BaseStartingApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BaseStartingApp
{
    public partial class MainPage : ContentPage
    {
        private async Task<Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }

                return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return null;
        }
        private async Task SetInitialLocation()
        {
            try
            {
                // Pobranie aktualnej lokalizacji użytkownika
                var location = await GetCurrentLocation();

                // Ustawienie początkowej lokalizacji na mapie
                if (location != null)
                {
                    myMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Position(location.Latitude, location.Longitude),
                        Distance.FromMiles(2))); // Ustaw promień widzenia mapy, tutaj na 1 milę
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów związanych z lokalizacją
            }
        }
        
        private async Task GeneratePins(Position polozenie, string labelText)
        {
            var geocoder = new Geocoder();
            //var polozenie = new Position(lokalizacja.Latitude, lokalizacja.Longitude); // Przykładowe współrzędne (San Francisco)

            // Uzyskaj listę adresów dla danej lokalizacji
            var addresses = await geocoder.GetAddressesForPositionAsync(polozenie);

            // Wybierz pierwszy adres z listy (możesz dostosować, w zależności od potrzeb)
            string firstAddress = addresses.FirstOrDefault();

            Pin pinTokyo = new Pin()
            {
                Type = PinType.Place,
                Label = labelText ?? "Nowe zdarzenie",
                Address = firstAddress,
                Position = polozenie
            };

            myMap.Pins.Add(pinTokyo);
            //myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinTokyo.Position, Distance.FromMeters(1400)));
        }
        public MainPage()
        {
            InitializeComponent();

            // Inicjalizacja mapy
            myMap.MapType = MapType.Street;
            myMap.HasZoomEnabled = true;

            // Ustawienie początkowej lokalizacji na mapie
            SetInitialLocation();

            Task.Run(async () => {
                //await SetInitialLocation();
                List<Disaster> disastersList = await Database.GetDisastersAsync();
                foreach (Disaster disaster in disastersList)
                {
                    if (disaster.Location.Contains(','))
                    {
                        double latitude = double.Parse(disaster.Location.Substring(0, disaster.Location.IndexOf(',')));
                        double longitude = double.Parse(disaster.Location.Substring(disaster.Location.IndexOf(',') + 1));
                        //string latitude = disaster.Location.Substring(0, disaster.Location.IndexOf(','));
                        //string longitude = disaster.Location.Substring(disaster.Location.IndexOf(',') + 1);

                        Console.WriteLine($"Latitude: {latitude}, Longitude: {longitude}");
                        await GeneratePins(new Position(latitude, longitude), disaster.Name);
                    }

                }
            }).Wait();



            if (App.LoggedUser == null) { 
                button.IsVisible = false;

            }
            else
            {
                button.IsVisible = true;
            }
        }

        private async void Buttond_Clicked(object sender, EventArgs e)
        {
            Location lokalizacja = await GetCurrentLocation();
            await GeneratePins(new Position(lokalizacja.Latitude, lokalizacja.Longitude), null);
            AddDisasterPage.polozenie = new Position(lokalizacja.Latitude, lokalizacja.Longitude);
            Navigation.PushAsync(new AddDisasterPage());

        }

        private void buttonlogin(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            if(App.LoggedUser == null)
                Navigation.PushAsync(new LoginPage());
            else
                Navigation.PushAsync(new UpdatePage());
        }
        private void buttonDonation(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            Navigation.PushAsync(new AllDonationsPage());
        }

        private async void buttonzdarzenia(object sender, EventArgs e)
        {
            //AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            Navigation.PushAsync(new AllDisastersPage());


        }
    }
}
