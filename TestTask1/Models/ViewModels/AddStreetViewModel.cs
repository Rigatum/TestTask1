using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Contracts;
using System.ComponentModel.DataAnnotations;
namespace TestTask1.Models.ViewModels
{
    public class AddStreetViewModel : IViewModel
    {
        [Required]
        public string StreetName { get; set; }
    }
}