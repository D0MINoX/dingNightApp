using BaseStartingApp.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BaseStartingApp.ViewModels;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net.Http;

namespace BaseStartingApp.ViewModels
{
    public class UserInterface
    {
        public static async Task<ValidationData> emailValidation(string email)
        {
            //Walidacja pola 'email' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            if (!String.IsNullOrEmpty(email))
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(email))
                {
                    //Pobranie listy użytkowników i sprawdzenie, czy dany użytkownik już istnieje
                    var users = await Database.GetUsersAsync();
                    Users user = users.Find(x => x.email == email);

                    if (user != null)
                        data.errorInfo = "Użytkownik o podanym emailu już istnieje";
                    else
                    {
                        data.correct = true;
                    }
                } else
                {
                    data.errorInfo = "Podaj prawidłowy email";
                }

            }
            else
            {
                data.errorInfo = "Email nie może być pusty";
            }

            return data;
        }

        public static async Task<ValidationData> emailUpdateValidation(string email)
        {
            //Walidacja pola 'email' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            if (!String.IsNullOrEmpty(email))
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(email))
                {
                    data.correct = true;
                }
                else
                {
                    data.errorInfo = "Podaj prawidłowy email";
                }

            }
            else
            {
                data.errorInfo = "Email nie może być pusty";
            }

            return data;
        }

        public static async Task<ValidationData> nameValidation(string name)
        {
            //Walidacja pola 'imie' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            //Sprawdzenie, czy pole 'hasło' nie jest puste
            if (!string.IsNullOrEmpty(name))
                data.correct = true;
            else
                data.errorInfo = "Imie nie może być puste";

            return data;
        }

        public static async Task<ValidationData> surnameValidation(string surname)
        {
            //Walidacja pola 'nazwisko' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            //Sprawdzenie, czy pole 'nazwisko' nie jest puste
            if (!string.IsNullOrEmpty(surname))
                data.correct = true;
            else
                data.errorInfo = "Nazwisko nie może być puste";

            return data;
        }

        public static async Task<ValidationData> phoneValidation(string phone)
        {
            //Walidacja pola 'nazwa użytkownika' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            if (!String.IsNullOrEmpty(phone))
            {
                string pattern = @"^\d{9}$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(phone))
                {
                    data.correct = true;
                }
                else
                {
                    data.errorInfo = "Podaj prawidłowy nr telefonu";
                }

            }
            else
            {
                data.errorInfo = "Nr telefonu nie może być pusty";
            }

            return data;
        }

        public static async Task<ValidationData> passwordValidation(string password)
        {
            //Walidacja pola 'hasło' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            //Sprawdzenie, czy pole 'hasło' nie jest puste
            if (!string.IsNullOrEmpty(password))
                data.correct = true;
            else
                data.errorInfo = "Hasło nie może być puste";

            return data;
        }

        public static async Task<ValidationData> passwordConfirmValidation(string password, string confirmPassword)
        {
            //Walidacja pola 'powtórz hasło' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            //Sprawdzenie, czy pola 'hasło' i 'powtórz hasło' są identyczne
            if (!string.IsNullOrEmpty(confirmPassword))
            {
                if (confirmPassword != password)
                    data.errorInfo = "Podane hasła nie są identyczne";
                else
                {
                    data.correct = true;
                }
            }
            else
            {
                data.errorInfo = "Powtórz hasło";
            }

            return data;
        }

        public static async Task RegisterUser(string email, string name, string surname, string phone, string password, string confirmPassword)
        {
            //Sprawdzenie, czy wszystkie pola formularza rejestracji są wypełnione poprawnie
            List<bool> validation = new List<bool>
            {
                (await emailValidation(email)).correct,
                (await nameValidation(name)).correct,
                (await surnameValidation(surname)).correct,
                (await phoneValidation(phone)).correct,
                (await passwordValidation(password)).correct,
                (await passwordConfirmValidation(password, confirmPassword)).correct
            };


            if (validation.All(x => x == true))
            {
                //zahaszowanie hasła użytkownika
                string hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

                //rejestracja nowego użytkownika i przekierowanie do strony logowania
                Users user = new Users()
                {
                    email = email,
                    name = name,
                    surname = surname,
                    phone = phone,
                    password = hashedPassword
                    //token = 

                };
                await Database.SaveUserAsync(user);
                //if (result > 0)
                    await App.Current.MainPage.DisplayAlert("Potwierdzenie", "Pomyślnie zajerestrowano nowego użytkownika", "Zaloguj się");
                AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Wprowadzone są błędne. Stosuj się do odpowiednich komunikatów. Spróbuj ponownie", "ZAMKNIJ");
            }
        }

        public async static Task<bool> LogInUserValidation(string email, string password)
        {
            //Sprawdzenie, czy wszystkie pola formularza logowanie nie są puste
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                //Pobranie listy użytkowników i sprawdzenie, czy dany użytkownik istnieje
                var users = await Database.GetUsersAsync();
                Users user = users.Find(x => x.email == email);

                if (user == null)
                {
                    await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Użytkownik o podanym emailu nie istnieje. Spróbuj ponownie", "ZAMKNIJ");
                    return false;
                }
                else
                {
                    //zahaszowanie hasła użytkownika
                    string hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

                    //sprawdzenie, czy hasz hasła i hasło użytkownika są identyczne
                    if (user.password == hashedPassword)
                    {
                        //zalogowanie na konto użytkownika i przekierowanie do strony blogu
                        await App.Current.MainPage.DisplayAlert("Powiadomienie", "Pomyślnie zalogowano", "Przejdź do blogu");
                        App.LoggedUser = user;
                        return true;
                        //AppNavigation.NavigateTo(new NavigationData { location = "MainPage" });
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Hasło nieprawidłowe. Spróbuj ponownie", "ZAMKNIJ");
                        return false;
                    }
                        
                }

            } else return false;
        }

        /*HttpClient client = new HttpClient();
        public static async void mapaa(Disaster data)
        {
            var dane = new Dictionary<string, string>{
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
            
        }*/

        public static async Task<bool> UpdateUser(string email, string name, string surname, string phone, string password, string confirmPassword)
        {
            //Sprawdzenie, czy wszystkie pola formularza rejestracji są wypełnione poprawnie
            List<bool> validation = new List<bool>
            {
                (await emailUpdateValidation(email)).correct,
                (await nameValidation(name)).correct,
                (await surnameValidation(surname)).correct,
                (await phoneValidation(phone)).correct,
                (await passwordValidation(password)).correct,
                (await passwordConfirmValidation(password, confirmPassword)).correct
            };


            if (validation.All(x => x == true))
            {
                //zahaszowanie hasła użytkownika
                

                //rejestracja nowego użytkownika i przekierowanie do strony logowania
                Users user = new Users()
                {
                    id = App.LoggedUser.id,
                    email = email,
                    name = name,
                    surname = surname,
                    phone = phone,
                    password = password
                    //token = 

                };
                await Database.UpdateUserAsync(user);
                //if (result > 0)
                await App.Current.MainPage.DisplayAlert("Potwierdzenie", "Pomyślnie zaktualizowano dane użytkownika", "OK");
                App.LoggedUser = user;
                return true;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Wprowadzone są błędne. Stosuj się do odpowiednich komunikatów. Spróbuj ponownie", "ZAMKNIJ");
                return false;
            }

            return true;
        }
    }
}
