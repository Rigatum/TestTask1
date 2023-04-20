using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models
{
    public class House
    {
        public int ID { get; set; }
        public string HouseName { get; set; }
        public string HouseType { get; set; }
        public string HouseTypeFull { get; set; }
        public int StreetID { get; set; }
        public City Street { get; set; }
        public ICollection<Flat> Flats { get; set;}

        public override string ToString()
        {
            return $"{HouseType}. {HouseName}";
        }
    }
}