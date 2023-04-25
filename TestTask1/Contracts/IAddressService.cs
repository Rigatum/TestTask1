using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask1.Models.Domain;
using TestTask1.Models.ViewModels;
namespace TestTask1.Contracts
{
    public interface IAddressService
    {
        public Task<string> Insert(AddCityViewModel addCityViewModel, AddStreetViewModel addStreetViewModel, AddHouseViewModel addHouseViewModel,
                                        AddFlatViewModel addFlatViewModel, AddOwnerViewModel addOwnerViewModel);
        public List<Address> GetAddresses(string sortOrder);
        public void ConvertToViewModel(int ID, int CityID, int StreetID, int HouseID, int FlatID, 
                                        out List<IViewModel> list);
        public Task<string> Update(EditCityViewModel editCityViewModelCityName, EditStreetViewModel editStreetViewModelStreetName, EditHouseViewModel editHouseViewModelHouseName,
                                        EditFlatViewModel editFlatViewModelFlatName, EditOwnerViewModel editOwnerViewModelFIO);
        public Task<string> Delete(int CityID, int StreetID, int HouseID,
                                         int FlatID, int OwnerID);
        public List<SelectListItem> GetCities();
    }
}