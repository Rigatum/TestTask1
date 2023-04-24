using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models.Domain
{
    public class House
    {
        public int ID { get; set; }
        public string HouseName { get; set; }
        public byte FlatsNumber { get; set; }
        public int StreetID { get; set; }
        public Street Street { get; set; }
        public ICollection<Flat> Flats { get; set;}

        public override string ToString()
        {
            return HouseName;
        }
    }
}