using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartingApp
{
    public class Donation
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double ColectedMoney {  get; set; }
        public double NeededMoney { get; set; }
        public Donation(string N,string D,double C,double NM) {
       
            Name = N;
            Description = D;
            ColectedMoney = C;
            NeededMoney = NM;
        }
    }
}
