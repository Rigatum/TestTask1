using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models
{
    public class Street
    {
        public int ID { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string StreetTypeFull { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }

        public override string ToString()
        {
            return $"{StreetType}. {StreetName}";
        }
    }
}