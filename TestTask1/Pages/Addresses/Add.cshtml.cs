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

        public Add(ILogger<Add> logger, ApplicationContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var cityDomainModel = new City {CityName = addCityViewModel.CityName};
            var streetDomainModel = new Street {StreetName = addStreetViewModel.StreetName, City = cityDomainModel};
            var houseDomainModel = new House {HouseName = addHouseViewModel.HouseName, Street = streetDomainModel};
            var flatDomainModel = new Flat {FlatName = addFlatViewModel.FlatName, House = houseDomainModel};
            var ownerDomainModel = new Owner {FIO = addOwnerViewModel.FIO, Flat = flatDomainModel};
            City cityExist = null;
            Street streetExist = null;
            House houseExist = null;
            Flat flatExist = null;
            Owner ownerExist = null;

            cityExist = _db.Cities.FirstOrDefault(c => c.CityName == cityDomainModel.CityName);
            if (cityExist != null)
                streetExist = _db.Streets.FirstOrDefault(s => s.StreetName == streetDomainModel.StreetName && s.CityID == cityExist.ID);
            if (streetExist != null)
                houseExist = _db.Houses.FirstOrDefault(h => h.HouseName == houseDomainModel.HouseName && h.StreetID == streetExist.ID);
            if (houseExist != null)
                flatExist = _db.Flats.FirstOrDefault(f => f.FlatName == flatDomainModel.FlatName && f.HouseID == houseExist.ID);
            if (flatExist != null)
                ownerExist = _db.Owners.FirstOrDefault(o => o.FIO == ownerDomainModel.FIO && o.FlatID == flatExist.ID);
            
            if (cityExist == null) //Добавление  города, улицы, дома, квартиры и собственника
            {
                System.Console.WriteLine("Города нет");
                _db.Cities.Add(cityDomainModel);
                _db.Streets.Add(streetDomainModel);
                _db.Houses.Add(houseDomainModel);
                _db.Flats.Add(flatDomainModel);
                _db.Owners.Add(ownerDomainModel);
                _db.SaveChanges();
                ViewData["Message"] = "Адрес был добавлен";
            }
            else if (cityExist != null && streetExist == null && houseExist == null && flatExist == null && ownerExist == null) // Добавление улицы, дома, квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                _db.Streets.Add(streetDomainModel);
                _db.Houses.Add(houseDomainModel);
                _db.Flats.Add(flatDomainModel);
                _db.Owners.Add(ownerDomainModel);
                _db.SaveChanges();
                ViewData["Message"] = "Улица, дом, квартира и собственник были добавлены к существующему городу";
            }
            else if (cityExist != null && streetExist != null && houseExist == null && flatExist == null && ownerExist == null) // Добавление дома, квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                _db.Houses.Add(houseDomainModel);
                _db.Flats.Add(flatDomainModel);
                _db.Owners.Add(ownerDomainModel);
                _db.SaveChanges();
                ViewData["Message"] = "Дом, квартира и собственник были добавлены к существующей улице";
            }
            else if (cityExist != null && streetExist != null &&  houseExist != null && flatExist == null && ownerExist == null) // Добавление квартиры и собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                _db.Flats.Add(flatDomainModel);
                _db.Owners.Add(ownerDomainModel);
                _db.SaveChanges();
                ViewData["Message"] = "Квартира и собственник были добавлены к существующему дому";
            }
            else if (cityExist != null && streetExist != null &&  houseExist != null && flatExist != null && ownerExist == null) // Добавление собственника
            {
                streetDomainModel.City = cityExist;
                houseDomainModel.Street = streetExist;
                flatDomainModel.House = houseExist;
                ownerDomainModel.Flat = flatExist;
                _db.Owners.Add(ownerDomainModel);
                _db.SaveChanges();
                ViewData["Message"] = "Собственник был добавлен к существующей квартире";
            }
            else
            {
                ViewData["Message"] = "Квартира с таким собственником уже существует";
            }
        }
    }
}