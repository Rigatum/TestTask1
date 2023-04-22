using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models
{
    public class Flat
    {
        public int ID { get; set; }
        public string FlatName { get; set; }
        public string FlatType { get; set; }
        public int HouseID { get; set; }
        public House House { get; set; }
        public ICollection<Owner> Owners { get; set;}

        public override string ToString()
        {
            return $"{FlatType}. {FlatName}";
        }
    }
}