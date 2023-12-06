using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using BaseStartingApp.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace BaseStartingApp.ViewModels
{
    public class Database
    {
        private static HttpClient client = new HttpClient();
        public static bool ConnectionCheck()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                return true;
            else
                return false;
        }

        public static async Task<List<Disaster>> GetDisastersAsync()
        {
            string disasters_data = await client.GetStringAsync("https://unpurged-knives.000webhostapp.com/ShowDisaster.php");
            List<Disaster> disasters_list = JsonConvert.DeserializeObject<List<Disaster>>(disasters_data);

            return disasters_list;
        }

        public static async Task<List<Users>> GetUsersAsync()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>() { { "table", "users" } };
            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/test.php", new FormUrlEncodedContent(dict));
            string json = await response.Content.ReadAsStringAsync();
            List<Users> users = JsonConvert.DeserializeObject<List<Users>>(json);

            return users;
        }

        public static async Task SaveUserAsync(Users user)
        {
            
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var prop in user.GetType().GetProperties()) { 
                dict.Add(prop.Name, prop.GetValue(user).ToString());
            Console.WriteLine($"{prop.Name} {prop.GetValue(user).ToString()}");
            }

            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/test.php", new FormUrlEncodedContent(dict));
        }

        public static async Task UpdateUserAsync(Users user)
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var prop in user.GetType().GetProperties())
            {
                dict.Add(prop.Name, prop.GetValue(user).ToString());
                Console.WriteLine($"{prop.Name} {prop.GetValue(user).ToString()}");
            }

            HttpResponseMessage response = await client.PostAsync("https://unpurged-knives.000webhostapp.com/UpdateUser.php", new FormUrlEncodedContent(dict));
            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
        }

    }
}
