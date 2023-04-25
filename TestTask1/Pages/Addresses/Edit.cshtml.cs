using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask1.Contracts;
using TestTask1.Models.ViewModels;
namespace TestTask1.Pages.Addresses
{
    public class Edit : PageModel
    {
        [BindProperty]
        public EditCityViewModel editCityViewModel { get; set; }
        [BindProperty]
        public EditStreetViewModel editStreetViewModel { get; set; }
        [BindProperty]
        public EditHouseViewModel editHouseViewModel { get; set; }
        [BindProperty]
        public EditFlatViewModel editFlatViewModel { get; set; }
        [BindProperty]
        public EditOwnerViewModel editOwnerViewModel { get; set; }
        private readonly ILogger<Edit> _logger;
        private readonly IAddressService _addressSerivce;

        public Edit(ILogger<Edit> logger, IAddressService addressService)
        {
            _logger = logger;
            _addressSerivce = addressService;
        }

        public void OnGet(int ID, int CityID, int StreetID, int HouseID, int FlatID)
        {
            List<IViewModel> list;
            _addressSerivce.ConvertToViewModel(ID, CityID, StreetID, HouseID, FlatID, out list);
            
            editCityViewModel = (EditCityViewModel)list[0];
            editStreetViewModel = (EditStreetViewModel)list[1];
            editHouseViewModel = (EditHouseViewModel)list[2];
            editFlatViewModel = (EditFlatViewModel)list[3];
            editOwnerViewModel = (EditOwnerViewModel)list[4];
        }
        public async Task<IActionResult> OnPostUpdateAsync(int editCityViewModelID, int editStreetViewModelID, int editHouseViewModelID,
                                 int editFlatViewModelID, int editOwnerViewModelID)
        {
            editCityViewModel.ID = editCityViewModelID;
            editStreetViewModel.ID = editStreetViewModelID;
            editHouseViewModel.ID = editHouseViewModelID;
            editFlatViewModel.ID = editFlatViewModelID;
            editOwnerViewModel.ID = editOwnerViewModelID;
            editCityViewModel.CityName = editCityViewModel.CityName.Trim();
            editStreetViewModel.StreetName = editStreetViewModel.StreetName.Trim();
            editHouseViewModel.HouseName = editHouseViewModel.HouseName.Trim();
            editFlatViewModel.FlatName = editFlatViewModel.FlatName.Trim();
            editOwnerViewModel.FIO = editOwnerViewModel.FIO.Trim();
            ViewData["Message"] = await _addressSerivce.Update(editCityViewModel, editStreetViewModel, editHouseViewModel, editFlatViewModel, editOwnerViewModel);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int editCityViewModelID, int editStreetViewModelID, int editHouseViewModelID,
                                 int editFlatViewModelID, int editOwnerViewModelID)
        {
            var message = await _addressSerivce.Delete(editCityViewModelID, editStreetViewModelID, editHouseViewModelID,
                                                               editFlatViewModelID, editOwnerViewModelID);
            if (message == "Адрес удалён")
                return new RedirectToPageResult("/ManageObjects");
            ViewData["Message"] = message;
            return Page();
        }
    }
}