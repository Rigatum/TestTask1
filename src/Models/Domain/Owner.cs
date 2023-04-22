using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models.Domain
{
    public class Owner
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public int FlatID { get; set; }
        public Flat Flat { get; set; }
        
        public override string ToString()
        {
            return FIO;
        }   
    }
}