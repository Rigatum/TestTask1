using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Contracts;
namespace TestTask1.Models.ViewModels
{
    public class EditOwnerViewModel : IViewModel
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public int FlatID { get; set; }
    }
}