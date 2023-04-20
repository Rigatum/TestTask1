using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models
{
    public class FlatOwner
    {
        public int FlatID { get; set; }
        public Flat Flat { get; set; }
        public int OwnerID { get; set; }
        public Owner Owner { get; set; }
    }
}