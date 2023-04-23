using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models.Domain
{
    public class Address
    {
        public string CityName { get; set; }
        public int ID { get; set; }
        public string StreetName { get; set; }
        public int CityID { get; set; }
        public string HouseName { get; set; }
        public int StreetID { get; set; }
        public string FlatName { get; set; }
        public int HouseID { get; set; }
        public string FIO { get; set; }
        public int FlatID { get; set; }
    }
}