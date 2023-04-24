using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Contracts;
namespace TestTask1.Models.ViewModels
{
    public class AddOwnerViewModel : IViewModel
    {
        public string FIO { get; set; }
    }
}