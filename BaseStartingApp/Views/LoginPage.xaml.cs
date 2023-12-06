using BaseStartingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseStartingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseStartingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
            Color butcolor = Color.FromRgb(0, 204, 153);
            Color tekstkolor = Color.White;
            Button myButton = new Button
            {
                Text = "Utwórz konto",
                Padding = 5,
                BackgroundColor = butcolor,
                TextColor = tekstkolor,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = 10,
                Command = new Command<NavigationData>(AppNavigation.NavigateTo),
                CommandParameter = new NavigationData { location = "RegisterPage" }
            };

            gridlog.Children.Add(myButton,0,3);
            Button myButton2 = new Button
            {
                Text = "Wróć na stronę główną",
                Padding = 5,
                BackgroundColor = Color.Transparent,
                TextColor = butcolor,
                FontAttributes = FontAttributes.Italic,
                Command = new Command(move)
                //CommandParameter = new NavigationData { location = "MainPage" }
               
            };

            gridlog.Children.Add(myButton2, 0, 4);

            //Button myButton3 = new Button
            //{
            //    Text = "editpage",
            //    Padding = 5,
            //    BackgroundColor = Color.Transparent,
            //    TextColor = butcolor,
            //    FontAttributes = FontAttributes.Italic,
            //    Command = new Command(move)
            //    //CommandParameter = new NavigationData { location = "MainPage" }

            //};

            //gridlog.Children.Add(myButton2, 0, 5);



        }
        private void move()
        {
            //App.Current.MainPage = new MainPage();
            Navigation.PopAsync();
        }
        //private async void move2(object sender, EventArgs e)
        //{
        //    // Przejście na inną stronę za pomocą nawigacji
        //    await Navigation.PushAsync(new Edition());
        //    // await Navigation.PushModalAsync(new YourNextPage()); // W przypadku używania nawigacji modalnej
        //}
        private async void Button_Clicked(object sender, EventArgs e)
       {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            bool result = await ViewModels.UserInterface.LogInUserValidation(username,password);
            if (result) 
                Navigation.PopAsync();
        }

    }
}