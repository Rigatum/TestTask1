using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask1.Contracts;

namespace TestTask1.Pages.Addresses
{
public class GameDataModel
{
    public string Name { get; set; }
    public string ID { get; set; }

    public List<GameDataModel> GetData()
    {
        List<GameDataModel> AutoCompleteData = new List<GameDataModel>()
        {
                new GameDataModel() { ID= "Game1", Name= "Badminton" },
                new GameDataModel() { ID= "Game2", Name= "Basketball" },
                new GameDataModel() { ID= "Game3", Name= "Cricket" },
                new GameDataModel() { ID= "Game4", Name= "Football" },
                new GameDataModel() { ID= "Game5", Name= "Golf" },
                new GameDataModel() { ID= "Game6", Name= "Hockey" },
                new GameDataModel() { ID= "Game7", Name= "Tennis"}
        };
        return AutoCompleteData;
    }
}
    public class Add : PageModel
    {
        [BindProperty]
        public string dirtyAddress { get; set; }
        private readonly ILogger<Add> _logger;
        private readonly IAddressService _addressService;

        public Add(ILogger<Add> logger, IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var test = _addressService.GetCleanAddress(dirtyAddress);
            System.Console.WriteLine(test.city);
        }
    }
}