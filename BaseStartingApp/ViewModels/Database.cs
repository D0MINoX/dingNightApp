using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using BaseStartingApp.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BaseStartingApp.ViewModels
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            //nazwiązanie połączenia z bazą danych
            _database = new SQLiteAsyncConnection(dbPath);
        }

        //Inicjalizacja bazy danych tabelami i podstawowymi rekordami
        public async Task InitializeAsync()
        {
            //await _database.DropTableAsync<Users>();
            await _database.CreateTableAsync<Users>();

            var users = await App.Database.GetUsersAsync();
            Users user = users.Find(x => x.Username == "Admin");

            if(user == null)
            {
                string hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("123")));
                await _database.InsertAsync(new Users() { Username = "Admin", Password = hashedPassword });
            }

        }

        public Task<List<Users>> GetUsersAsync()
        {
            //Zwraca wszystkich użytkowników w formie listy
            return _database.Table<Users>().ToListAsync();
        }

        public Task<int> SaveUserAsync(Users user)
        {
            //Dodanie nowego użytkownika do tabeli
            return _database.InsertAsync(user);
        }

    }
}
