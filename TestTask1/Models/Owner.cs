using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Models
{
    public class Owner
    {
        public int ID { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string? OwnerFatherName { get; set; }
        public ICollection<Flat> Flat { get; set;}

        public override string ToString()
        {
            return $"{OwnerLastName} {OwnerFirstName} {OwnerFatherName}";
        }   
    }
}