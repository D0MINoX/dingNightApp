using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BaseStartingApp
{
    public class Disaster
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int volunteers_number { get; set; }
        public int volunteers_number_needed { get; set; }
        public string user { get; set; }

        public Disaster(string N,string D,string L,int VN,int VNN,string U) {
            Name = N;
            Description = D;
            Location = L;
            volunteers_number = VN;
            volunteers_number_needed = VNN;
            user = U;
        }
    }
}
