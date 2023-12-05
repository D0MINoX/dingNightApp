using BaseStartingApp.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaseStartingApp.ViewModels
{
    public class UserInterface
    {
        public static async Task<ValidationData> usernameValidation(string username)
        {
            //Walidacja pola 'nazwa użytkownika' w formularzu rejestracji
            ValidationData data = new ValidationData() { correct = false };

            if (!String.IsNullOrEmpty(username))
            {
                //Pobranie listy użytkowników i sprawdzenie, czy dany użytkownik już istnieje
                var users = await App.Database.GetUsersAsync();
                Users user = users.Find(x => x.Username == username);

                if (user != null)
                    data.errorInfo = "Użytkownik o podanej nazwie już istnieje";
                else
                {
                    data.correct = true;
                }

            }
            else
            {
                data.errorInfo = "Nazwa użytkownika nie może być pusta";
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

        public static async Task RegisterUser(string username, string password, string confirmPassword)
        {
            //Sprawdzenie, czy wszystkie pola formularza rejestracji są wypełnione poprawnie
            bool usernameValidated = (await usernameValidation(username)).correct;
            bool passwordValidated = (await passwordValidation(password)).correct;
            bool passwordConfirmValidated = (await passwordConfirmValidation(password, confirmPassword)).correct;

            if (usernameValidated && passwordValidated && passwordConfirmValidated)
            {
                //zahaszowanie hasła użytkownika
                string hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

                //rejestracja nowego użytkownika i przekierowanie do strony logowania
                int result = await App.Database.SaveUserAsync(new Users() { Username = username, Password = hashedPassword });
                if (result > 0)
                    await App.Current.MainPage.DisplayAlert("Potwierdzenie", "Pomyślnie zajerestrowano nowego użytkownika", "Zaloguj się");
                AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Wprowadzone są błędne. Stosuj się do odpowiednich komunikatów. Spróbuj ponownie", "ZAMKNIJ");
            }
        }

        public async static Task LogInUserValidation(string username, string password)
        {
            //Sprawdzenie, czy wszystkie pola formularza logowanie nie są puste
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                //Pobranie listy użytkowników i sprawdzenie, czy dany użytkownik istnieje
                var users = await App.Database.GetUsersAsync();
                Users user = users.Find(x => x.Username == username);

                if (user == null)
                    await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Użytkownik o podanej nazwie nie istnieje. Spróbuj ponownie", "ZAMKNIJ");
                else
                {
                    //zahaszowanie hasła użytkownika
                    string hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

                    //sprawdzenie, czy hasz hasła i hasło użytkownika są identyczne
                    if (user.Password == hashedPassword)
                    {
                        //zalogowanie na konto użytkownika i przekierowanie do strony blogu
                        await App.Current.MainPage.DisplayAlert("Powiadomienie", "Pomyślnie zalogowano", "Przejdź do blogu");
                        App.LoggedUser = user;
                        AppNavigation.NavigateTo(new NavigationData { location = "LoggedPage" });
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Hasło nieprawidłowe. Spróbuj ponownie", "ZAMKNIJ");
                }

            }
        }
    }
}
