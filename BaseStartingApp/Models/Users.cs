using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartingApp.Models
{
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        //public string token { get; set; }
    }
}
