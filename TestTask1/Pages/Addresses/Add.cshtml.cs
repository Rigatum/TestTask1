using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask1.Contracts;
using TestTask1.Models.Domain;
using TestTask1.Models.ViewModels;
using TestTask1.Pages;
using TestTask1.Data;
using TestTask1.Contracts;
namespace TestTask1.Pages.Addresses
{
    public class Add : PageModel
    {
        [BindProperty]
        public AddCityViewModel addCityViewModel { get; set; }
        [BindProperty]
        public AddFlatViewModel addFlatViewModel { get; set; }
        [BindProperty]
        public AddHouseViewModel addHouseViewModel { get; set; }
        [BindProperty]
        public AddOwnerViewModel addOwnerViewModel { get; set; }
        [BindProperty]
        public AddStreetViewModel addStreetViewModel { get; set; }
        private readonly ILogger<Add> _logger;
        private readonly ApplicationContext _db;
        private readonly IAddressService _addressService;

        public Add(ILogger<Add> logger, ApplicationContext db, IAddressService addressService)
        {
            _logger = logger;
            _db = db;
            _addressService = addressService;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            ViewData["Message"] = await _addressService.Insert(addCityViewModel.CityName, addStreetViewModel.StreetName, 
                                                                addHouseViewModel.HouseName, addFlatViewModel.FlatName, 
                                                                addOwnerViewModel.FIO);
            return Page();
        }
    }
}