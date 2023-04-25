using TestTask1.Contracts;
using TestTask1.Data;
using TestTask1.Models.Domain;
using Microsoft.EntityFrameworkCore;
using TestTask1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<string> Insert(AddCityViewModel addCityViewModel, AddStreetViewModel addStreetViewModel, AddHouseViewModel addHouseViewModel,
                                        AddFlatViewModel addFlatViewModel, AddOwnerViewModel addOwnerViewModel)
        {
            var cityDomainModel = new City {CityName = addCityViewModel.CityName};
            var streetDomainModel = new Street {StreetName = addStreetViewModel.StreetName, City = cityDomainModel};
            var houseDomainModel = new House {HouseName = addHouseViewModel.HouseName, Street = streetDomainModel, FlatsNumber = addHouseViewModel.FlatsNumber};
            var flatDomainModel = new Flat {FlatName = addFlatViewModel.FlatName, House = houseDomainModel};
            var ownerDomainModel = new Owner {FIO = addOwnerViewModel.FIO, Flat = flatDomainModel};
            City? cityExist = null;
            Street? streetExist = null;
            House? houseExist = null;
            Flat? flatExist = null;
            Owner? ownerExist = null;

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
                if (houseDomainModel.FlatsNumber == null)
                {
                    return  "При добавлении нового дома, количество квартир должно быть заполнено";
                }
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
                if (houseDomainModel.FlatsNumber == null)
                {
                    return  "При добавлении нового дома, количество квартир должно быть заполнено";
                }
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
                if (houseDomainModel.FlatsNumber == null)
                {
                    return  "При добавлении нового дома, количество квартир должно быть заполнено";
                }
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
                return  "Квартира с таким собственником уже существует";
        }

        public List<Address> GetAddresses(string sortOrder)
        {
            var addresses = from c in _db.Cities
            join s in _db.Streets on c.ID equals s.CityID
            join h in _db.Houses on s.ID equals h.StreetID
            join f in _db.Flats on h.ID equals f.HouseID
            join o in _db.Owners on f.ID equals o.FlatID
            select new Address{
                CityName = c.CityName, ID = c.ID,
                StreetName = s.StreetName, CityID = s.CityID,
                HouseName = h.HouseName, StreetID = h.StreetID, FlatsNumber = h.FlatsNumber,
                FlatName = f.FlatName, HouseID = f.HouseID,
                FIO = o.FIO, FlatID = o.FlatID};

            switch (sortOrder)
            {
                case "city_desc":
                    return addresses.OrderByDescending(x => x.CityName).ToList();
                case "street_desc":
                    return addresses.OrderByDescending(x => x.StreetName).ToList();
                case "house_desc":
                    return addresses.OrderByDescending(x => x.HouseName).ToList();
                case "flat_number_desc":
                    return addresses.OrderByDescending(x => x.FlatsNumber).ToList();
                case "flat_desc":
                    return addresses.OrderByDescending(x => x.FlatName).ToList();
                case "owner_desc":
                    return addresses.OrderByDescending(x => x.FIO).ToList();
                case "street_asc":
                    return addresses.OrderBy(x => x.StreetName).ToList();
                case "house_asc":
                    return addresses.OrderBy(x => x.HouseName).ToList();
                case "flat_number_asc":
                    return addresses.OrderBy(x => x.FlatsNumber).ToList();
                case "flat_asc":
                    return addresses.OrderBy(x => x.FlatName).ToList();
                case "owner_asc":
                    return addresses.OrderBy(x => x.FIO).ToList();
                default:
                    return addresses.OrderBy(x => x.CityName).ToList();
            }
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
                    HouseName = h.HouseName, HouseID = h.ID, FlatsNumber = h.FlatsNumber,
                    FlatName = f.FlatName, FlatID = f.ID,
                    FIO = o.FIO, OwnerID = o.ID};
            var listAddress = address.ToList();


            
            EditCityViewModel editCityViewModel = new EditCityViewModel() {ID = listAddress[0].ID, CityName = listAddress[0].CityName};
            EditStreetViewModel editStreetViewModel = new EditStreetViewModel() {ID = listAddress[0].StreetID, StreetName = listAddress[0].StreetName};
            EditHouseViewModel editHouseViewModel = new EditHouseViewModel() {ID = listAddress[0].HouseID, HouseName = listAddress[0].HouseName, FlatsNumber = listAddress[0].FlatsNumber};
            EditFlatViewModel editFlatViewModel = new EditFlatViewModel() {ID = listAddress[0].FlatID, FlatName = listAddress[0].FlatName};
            EditOwnerViewModel editOwnerViewModel = new EditOwnerViewModel() {ID = listAddress[0].OwnerID, FIO = listAddress[0].FIO};

            list = new List<IViewModel>() {editCityViewModel, editStreetViewModel, editHouseViewModel, editFlatViewModel, editOwnerViewModel};
        }
        public async Task<string> Update(EditCityViewModel editCityViewModel, EditStreetViewModel editStreetViewModel, EditHouseViewModel editHouseViewModel,
                                        EditFlatViewModel editFlatViewModel, EditOwnerViewModel editOwnerViewModel)
        {
            if (editHouseViewModel.FlatsNumber < 1 || editHouseViewModel.FlatsNumber > 100 || !editHouseViewModel.FlatsNumber.HasValue)
                return  "Введите количество квартир от 1 до 100";
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
                houseDomainModel.FlatsNumber = editHouseViewModel.FlatsNumber;
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
                houseDomainModel.FlatsNumber = editHouseViewModel.FlatsNumber;
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
                houseDomainModel.FlatsNumber = editHouseViewModel.FlatsNumber;
                flatDomainModel.FlatName = editFlatViewModel.FlatName;
                ownerDomainModel.FIO = editOwnerViewModel.FIO;
                await _db.SaveChangesAsync();
                return  "Номер дома был обновлён";
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
            else if (houseExist.FlatsNumber != editHouseViewModel.FlatsNumber)
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                houseDomainModel.FlatsNumber = editHouseViewModel.FlatsNumber;
                flatDomainModel.House = houseExist;
                ownerDomainModel.Flat = flatExist;
                ownerDomainModel.FIO = editOwnerViewModel.FIO;
                await _db.SaveChangesAsync();
                return  "Количество квартир в доме было обновлено";
            }
            else
                return  "Такой адрес уже имеется";
        }
        public async Task<string> Delete(int CityID, int StreetID, int HouseID,
                                            int FlatID, int OwnerID)
        {
            var existingCity = await _db.Cities.FindAsync(CityID);
            var existingStreet = await _db.Streets.FindAsync(StreetID);
            var existingHouse = await _db.Houses.FindAsync(HouseID);
            var existingFlat = await _db.Flats.FindAsync(FlatID);
            var existingOwner = await _db.Owners.FindAsync(OwnerID);

            if (existingCity != null && existingStreet != null && existingHouse != null
                && existingFlat != null && existingOwner != null)
            {
                _db.Owners.Remove(existingOwner);
                await _db.SaveChangesAsync();
                if (!_db.Owners.Any(o=>o.FlatID == existingOwner.FlatID))
                    _db.Flats.Remove(existingFlat);
                else
                {
                    await _db.SaveChangesAsync();
                    return "Адрес удалён";
                }
                await _db.SaveChangesAsync();
                if (!_db.Flats.Any(f=>f.HouseID == existingFlat.HouseID))
                    _db.Houses.Remove(existingHouse);
                else
                {
                    await _db.SaveChangesAsync();
                    return "Адрес удалён";
                }
                await _db.SaveChangesAsync();
                if (!_db.Houses.Any(h=>h.StreetID == existingHouse.StreetID))
                    _db.Streets.Remove(existingStreet);
                else
                {
                    await _db.SaveChangesAsync();
                    return "Адрес удалён";
                }
                await _db.SaveChangesAsync();
                if (!_db.Streets.Any(s=>s.CityID == existingStreet.CityID))
                    _db.Cities.Remove(existingCity);

                await _db.SaveChangesAsync();
                return "Адрес удалён";
            }
            else
                return "Адрес не найден";
        }
        public List<SelectListItem> GetCities()
        {
            return _db.Cities.Select(c => new SelectListItem() {Text = c.CityName, Value = c.ID.ToString()}).ToList();
        }
    }
}