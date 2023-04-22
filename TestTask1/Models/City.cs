using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models
{
    public class City
    {
        public int ID { get; set; }
        public string CityName { get; set; }
        public ICollection<Street> Streets { get; set; }
        
        public override string ToString()
        {
            return CityName;
        }
    }
}