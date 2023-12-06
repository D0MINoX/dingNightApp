using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BaseStartingApp.Models;
using BaseStartingApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BaseStartingApp.ViewModels
{
    class AppNavigation
    {
        //Funkcja służąca do nawigacji po naszej aplikacji
        static public async void NavigateTo(NavigationData data)
        {
            if (!string.IsNullOrEmpty(data.location))
            {
                //nazwa naszego projektu
                string projectName = Assembly.GetExecutingAssembly().GetName().Name;
                //string viewName = data.location.Contains("/") ? data.location.Substring(0, data.location.IndexOf('/')) : data.location;
                string viewName = data.location;
                Console.WriteLine("\n\n" + $"{projectName}.Views.{viewName}" + "\n\n");
                //pobranie typu widoku, do którego chcemy się przenieść
                Type type = Type.GetType($"{projectName}.Views.{viewName}");

                //zmiana widoku
                List<string> list = new List<string>() { "UserDataPage" };

                //if(viewName != data.location)
                //    App.Current.MainPage = (Page)Activator.CreateInstance(type, data.location.Substring(data.location.IndexOf('/')+1));
                
                /*if (list.Contains(data.location))
                    App.Current.MainPage = (Page)Activator.CreateInstance(type, data.loggedUser);
                else*/
                App.Current.MainPage = new NavigationPage((Page)Activator.CreateInstance(type));
                //App.Current.MainPage = Navigation.PushAsync(new MainPage());
            }
                
        }
    }
}
