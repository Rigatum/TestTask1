using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Contracts;
namespace TestTask1.Models.ViewModels
{
    public class EditStreetViewModel : IViewModel
    {
        public int ID { get; set; }
        public string StreetName { get; set; }
        public int CityID { get; set; }
    }
}