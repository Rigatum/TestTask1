using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models
{
    public class Owner
    {
        public int Guid { get; set; }
        public string FIO { get; set; }
        public ICollection<Flat> Flats { get; set;}

        public override string ToString()
        {
            return FIO;
        }   
    }
}