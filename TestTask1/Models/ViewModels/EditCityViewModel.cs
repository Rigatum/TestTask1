using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Contracts;
namespace TestTask1.Models.ViewModels
{
    public class EditCityViewModel : IViewModel
    {
        public int ID { get; set; }
        public string CityName { get; set; }
    }
}