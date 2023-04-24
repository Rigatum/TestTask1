using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Models;
using TestTask1.Contracts;
using TestTask1.Data;
using TestTask1.Models.Domain;
using Microsoft.EntityFrameworkCore;
using TestTask1.Models.ViewModels;
namespace TestTask1.Services
{
    public class AddressService : IAddressService
    {
        private ApplicationContext _db { get; set; }
        public List<Address> Addresses { get; set; }
        public AddressService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<string> Insert(string addCityViewModelCityName, string addStreetViewModelStreetName, string addHouseViewModelHouseName,
                                        string addFlatViewModelFlatName, string addOwnerViewModelFIO)
        {
            var cityDomainModel = new City {CityName = addCityViewModelCityName};
            var streetDomainModel = new Street {StreetName = addStreetViewModelStreetName, City = cityDomainModel};
            var houseDomainModel = new House {HouseName = addHouseViewModelHouseName, Street = streetDomainModel};
            var flatDomainModel = new Flat {FlatName = addFlatViewModelFlatName, House = houseDomainModel};
            var ownerDomainModel = new Owner {FIO = addOwnerViewModelFIO, Flat = flatDomainModel};
            City? cityExist = null;
            Street? streetExist = null;
            House? houseExist = null;
            Flat? flatExist = null;
            Owner? ownerExist = null;

            if (String.IsNullOrWhiteSpace(addCityViewModelCityName) || String.IsNullOrWhiteSpace(addStreetViewModelStreetName)
            || String.IsNullOrWhiteSpace(addHouseViewModelHouseName) || String.IsNullOrWhiteSpace(addFlatViewModelFlatName)
            || String.IsNullOrWhiteSpace(addOwnerViewModelFIO))
                return "Заполните все поля";

            cityExist = await _db.Cities.FirstOrDefaultAsync(c => c.CityName == cityDomainModel.CityName);
            if (cityExist != null)
                streetExist = await _db.Streets.FirstOrDefaultAsync(s => s.StreetName == streetDomainModel.StreetName && s.CityID == cityExist.ID);
            if (streetExist != null)
                houseExist = await _db.Houses.FirstOrDefaultAsync(h => h.HouseName == houseDomainModel.HouseName && h.StreetID == streetExist.ID);
            if (houseExist != null)
                flatExist = await _db.Flats.FirstOrDefaultAsync(f => f.FlatName == flatDomainModel.FlatName && f.HouseID == houseExist.ID);
            if (flatExist != null)
                ownerExist = await _db.Owners.FirstOrDefaultAsync(o => o.FIO == ownerDomainModel.FIO && o.FlatID == flatExist.ID);
            
            if (cityExist == null) //Добавление  города, улицы, дома, квартиры и собственника
            {
                System.Console.WriteLine("Города нет");
                await _db.Cities.AddAsync(cityDomainModel);
                await _db.Streets.AddAsync(streetDomainModel);
                await _db.Houses.AddAsync(houseDomainModel);
                await _db.Flats.AddAsync(flatDomainModel);
                await _db.Owners.AddAsync(ownerDomainModel);
                await _db.SaveChangesAsync();
                return "Адрес был добавлен";
            }
            else if (cityExist != null && streetExist == null && houseExist == null && flatExist == null && ownerExist == null) // Добавление улицы, дома, квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                await _db.Streets.AddAsync(streetDomainModel);
                await _db.Houses.AddAsync(houseDomainModel);
                await _db.Flats.AddAsync(flatDomainModel);
                await _db.Owners.AddAsync(ownerDomainModel);
                await _db.SaveChangesAsync();
                return  "Улица, дом, квартира и собственник были добавлены к существующему городу";
            }
            else if (cityExist != null && streetExist != null && houseExist == null && flatExist == null && ownerExist == null) // Добавление дома, квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                await _db.Houses.AddAsync(houseDomainModel);
                await _db.Flats.AddAsync(flatDomainModel);
                await _db.Owners.AddAsync(ownerDomainModel);
                await _db.SaveChangesAsync();
                return  "Дом, квартира и собственник были добавлены к существующей улице";
            }
            else if (cityExist != null && streetExist != null &&  houseExist != null && flatExist == null && ownerExist == null) // Добавление квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                await _db.Flats.AddAsync(flatDomainModel);
                await _db.Owners.AddAsync(ownerDomainModel);
                await _db.SaveChangesAsync();
                return  "Квартира и собственник были добавлены к существующему дому";
            }
            else if (cityExist != null && streetExist != null &&  houseExist != null && flatExist != null && ownerExist == null) // Добавление собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                ownerDomainModel.Flat = flatExist;
                await _db.Owners.AddAsync(ownerDomainModel);
                await _db.SaveChangesAsync();
                return  "Собственник был добавлен к существующей квартире";
            }
            else
            {
                return  "Квартира с таким собственником уже существует";
            }
        }

        public List<Address> GetAddresses()
        {
            var addresses = from c in _db.Cities
                join s in _db.Streets on c.ID equals s.CityID
                join h in _db.Houses on s.ID equals h.StreetID
                join f in _db.Flats on h.ID equals f.HouseID
                join o in _db.Owners on f.ID equals o.FlatID
                select new Address{
                    CityName = c.CityName, ID = c.ID,
                    StreetName = s.StreetName, CityID = s.CityID,
                    HouseName = h.HouseName, StreetID = h.StreetID,
                    FlatName = f.FlatName, HouseID = f.HouseID,
                    FIO = o.FIO, FlatID = o.FlatID};
            return addresses.ToList();
        }
        public void ConvertToViewModel(int ID, int CityID, int StreetID, int HouseID, int FlatID, 
                                        out List<IViewModel> list)
        {
            var address = from c in _db.Cities
                join s in _db.Streets on c.ID equals s.CityID
                join h in _db.Houses on s.ID equals h.StreetID
                join f in _db.Flats on h.ID equals f.HouseID
                join o in _db.Owners on f.ID equals o.FlatID
                where c.ID == ID && s.CityID == CityID && 
                h.StreetID == StreetID && f.HouseID == HouseID
                && o.FlatID == FlatID
                select new{
                    CityName = c.CityName, ID = c.ID,
                    StreetName = s.StreetName, StreetID = s.ID,
                    HouseName = h.HouseName, HouseID = h.ID,
                    FlatName = f.FlatName, FlatID = f.ID,
                    FIO = o.FIO, OwnerID = o.ID};
            var listAddress = address.ToList();


            
            EditCityViewModel editCityViewModel = new EditCityViewModel() {ID = listAddress[0].ID, CityName = listAddress[0].CityName};
            EditStreetViewModel editStreetViewModel = new EditStreetViewModel() {ID = listAddress[0].StreetID, StreetName = listAddress[0].StreetName};
            EditHouseViewModel editHouseViewModel = new EditHouseViewModel() {ID = listAddress[0].HouseID, HouseName = listAddress[0].HouseName};
            EditFlatViewModel editFlatViewModel = new EditFlatViewModel() {ID = listAddress[0].FlatID, FlatName = listAddress[0].FlatName};
            EditOwnerViewModel editOwnerViewModel = new EditOwnerViewModel() {ID = listAddress[0].OwnerID, FIO = listAddress[0].FIO};

            list = new List<IViewModel>() {editCityViewModel, editStreetViewModel, editHouseViewModel, editFlatViewModel, editOwnerViewModel};
        }
        public async Task<string> Update(EditCityViewModel editCityViewModel, EditStreetViewModel editStreetViewModel, EditHouseViewModel editHouseViewModel,
                                        EditFlatViewModel editFlatViewModel, EditOwnerViewModel editOwnerViewModel)
        {
            var cityDomainModel = await _db.Cities.FindAsync(editCityViewModel.ID);
            var streetDomainModel = await _db.Streets.FindAsync(editStreetViewModel.ID);
            var houseDomainModel = await _db.Houses.FindAsync(editHouseViewModel.ID);
            var flatDomainModel = await _db.Flats.FindAsync(editFlatViewModel.ID);
            var ownerDomainModel = await _db.Owners.FindAsync(editOwnerViewModel.ID);
            City? cityExist = null;
            Street? streetExist = null;
            House? houseExist = null;
            Flat? flatExist = null;
            Owner? ownerExist = null;

            if (String.IsNullOrWhiteSpace(editCityViewModel.CityName) || String.IsNullOrWhiteSpace(editStreetViewModel.StreetName)
            || String.IsNullOrWhiteSpace(editHouseViewModel.HouseName) || String.IsNullOrWhiteSpace(editFlatViewModel.FlatName)
            || String.IsNullOrWhiteSpace(editOwnerViewModel.FIO))
                return "Заполните все поля";

            cityExist = await _db.Cities.FirstOrDefaultAsync(c => c.CityName == editCityViewModel.CityName);
            if (cityExist == null)
            {
                cityDomainModel.CityName = editCityViewModel.CityName;
                streetDomainModel.StreetName = editStreetViewModel.StreetName;
                houseDomainModel.HouseName = editHouseViewModel.HouseName;
                flatDomainModel.FlatName = editFlatViewModel.FlatName;
                ownerDomainModel.FIO = editOwnerViewModel.FIO;
                await _db.SaveChangesAsync();
                return "Адрес был обновлён";
            }

            streetExist = await _db.Streets.FirstOrDefaultAsync(s => s.StreetName == editStreetViewModel.StreetName && s.CityID == cityExist.ID);
            if (streetExist == null)
            {
                streetDomainModel.City = cityExist;
                streetDomainModel.StreetName = editStreetViewModel.StreetName;
                houseDomainModel.HouseName = editHouseViewModel.HouseName;
                flatDomainModel.FlatName = editFlatViewModel.FlatName;
                ownerDomainModel.FIO = editOwnerViewModel.FIO;
                await _db.SaveChangesAsync();
                return  "Улица была обновлена";
            }

            houseExist = await _db.Houses.FirstOrDefaultAsync(h => h.HouseName == editHouseViewModel.HouseName && h.StreetID == streetExist.ID);
            if (houseExist == null)
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                houseDomainModel.HouseName = editHouseViewModel.HouseName;
                flatDomainModel.FlatName = editFlatViewModel.FlatName;
                ownerDomainModel.FIO = editOwnerViewModel.FIO;
                await _db.SaveChangesAsync();
                return  "Номер дом был обновлён";
            }

            flatExist = await _db.Flats.FirstOrDefaultAsync(f => f.FlatName == editFlatViewModel.FlatName && f.HouseID == houseExist.ID);
            if (flatExist == null)
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                flatDomainModel.FlatName = editFlatViewModel.FlatName;
                ownerDomainModel.FIO = editOwnerViewModel.FIO;
                await _db.SaveChangesAsync();
                return  "Номер квартиры был обновлён";
            }

            ownerExist = await _db.Owners.FirstOrDefaultAsync(o => o.FIO == editOwnerViewModel.FIO && o.FlatID == flatExist.ID);
            if (ownerExist == null)
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                ownerDomainModel.Flat = flatExist;
                ownerDomainModel.FIO = editOwnerViewModel.FIO;
                await _db.SaveChangesAsync();
                return  "Собственник был обновлён";
            }
            else
            {
                return  "Квартира с таким собственником уже существует";
            }
        }

    }
}