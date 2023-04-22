using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask1.Models;
using TestTask1.Contracts;
using TestTask1.Data;
using TestTask1.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace TestTask1.Services
{
    public class AddressService : IAddressService
    {
        private ApplicationContext _db { get; set; }
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
            City cityExist = null;
            Street streetExist = null;
            House houseExist = null;
            Flat flatExist = null;
            Owner ownerExist = null;

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
                _db.Cities.AddAsync(cityDomainModel);
                _db.Streets.AddAsync(streetDomainModel);
                _db.Houses.AddAsync(houseDomainModel);
                _db.Flats.AddAsync(flatDomainModel);
                _db.Owners.AddAsync(ownerDomainModel);
                _db.SaveChangesAsync();
                return "Адрес был добавлен";
            }
            else if (cityExist != null && streetExist == null && houseExist == null && flatExist == null && ownerExist == null) // Добавление улицы, дома, квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                _db.Streets.AddAsync(streetDomainModel);
                _db.Houses.AddAsync(houseDomainModel);
                _db.Flats.AddAsync(flatDomainModel);
                _db.Owners.AddAsync(ownerDomainModel);
                _db.SaveChangesAsync();
                return  "Улица, дом, квартира и собственник были добавлены к существующему городу";
            }
            else if (cityExist != null && streetExist != null && houseExist == null && flatExist == null && ownerExist == null) // Добавление дома, квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                _db.Houses.AddAsync(houseDomainModel);
                _db.Flats.AddAsync(flatDomainModel);
                _db.Owners.AddAsync(ownerDomainModel);
                _db.SaveChangesAsync();
                return  "Дом, квартира и собственник были добавлены к существующей улице";
            }
            else if (cityExist != null && streetExist != null &&  houseExist != null && flatExist == null && ownerExist == null) // Добавление квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                _db.Flats.AddAsync(flatDomainModel);
                _db.Owners.AddAsync(ownerDomainModel);
                _db.SaveChangesAsync();
                return  "Квартира и собственник были добавлены к существующему дому";
            }
            else if (cityExist != null && streetExist != null &&  houseExist != null && flatExist != null && ownerExist == null) // Добавление собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                ownerDomainModel.Flat = flatExist;
                _db.Owners.AddAsync(ownerDomainModel);
                _db.SaveChangesAsync();
                return  "Собственник был добавлен к существующей квартире";
            }
            else
            {
                return  "Квартира с таким собственником уже существует";
            }
        }
    }
}