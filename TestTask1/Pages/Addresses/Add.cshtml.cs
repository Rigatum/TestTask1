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

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            addCityViewModel.CityName = addCityViewModel.CityName.Trim();
            addStreetViewModel.StreetName = addStreetViewModel.StreetName.Trim();
            addHouseViewModel.HouseName = addHouseViewModel.HouseName.Trim();
            addFlatViewModel.FlatName = addFlatViewModel.FlatName.Trim();
            addOwnerViewModel.FIO = addOwnerViewModel.FIO.Trim();

            if (addHouseViewModel.FlatsNumber < 0 || addHouseViewModel.FlatsNumber > 100)
            {
                ViewData["Message"] = "Количество квартир в доме должно быть от 1 до 100";
                return Page();
            }
            else if(ModelState.IsValid)
                ViewData["Message"] = await _addressService.Insert(addCityViewModel, addStreetViewModel, 
                                                                addHouseViewModel, addFlatViewModel, 
                                                                addOwnerViewModel);
            else
                ViewData["Message"] = "Заполните все поля, кроме адреса. Количество квартир должно быть числовым значением";
            return Page();
        }
    }
}