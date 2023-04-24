using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Models.Domain;
using TestTask1.Models.ViewModels;
namespace TestTask1.Contracts
{
    public interface IAddressService
    {
        public Task<string> Insert(string addCityViewModelCityName, string addStreetViewModelStreetName, string addHouseViewModelHouseName,
                                   string addFlatViewModelFlatName, string addOwnerViewModelFIO);
        public List<Address> GetAddresses();
        public void ConvertToViewModel(int ID, int CityID, int StreetID, int HouseID, int FlatID, 
                                        out List<IViewModel> list);
        public Task<string> Update(EditCityViewModel editCityViewModelCityName, EditStreetViewModel editStreetViewModelStreetName, EditHouseViewModel editHouseViewModelHouseName,
                                        EditFlatViewModel editFlatViewModelFlatName, EditOwnerViewModel editOwnerViewModelFIO);
        public Task<string> Delete(int CityID, int StreetID, int HouseID,
                                         int FlatID, int OwnerID);
    }
}