using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask1.Data;
using TestTask1.Models.Domain;
using TestTask1.Contracts;
namespace TestTask1.Pages;

public class ManageObjectsModel : PageModel
{
    private readonly ILogger<ManageObjectsModel> _logger;
    private readonly IAddressService _addressService;
    public List<Address> Addresses { get; set; }
    public ManageObjectsModel(ILogger<ManageObjectsModel> logger, IAddressService addressService)
    {
        _logger = logger;
        _addressService = addressService;
    }
    public string CitySort { get; set; }
    public string StreetSort { get; set; }
    public string HouseSort { get; set; }
    public string FlatNumberSort { get; set; }
    public string FlatSort { get; set; }
    public string OwnerSort { get; set; }
    public string CurrentSort { get; set; }
    public void OnGet(string sortOrder)
    {
        CitySort = sortOrder=="city_desc" ? "city_asc" : "city_desc";
        StreetSort = sortOrder=="street_desc" ? "street_asc" : "street_desc";
        HouseSort = sortOrder=="house_desc" ? "house_asc" : "house_desc";
        FlatNumberSort = sortOrder=="flat_number_desc" ? "flat_number_asc" : "flat_number_desc";
        FlatSort = sortOrder=="flat_desc" ? "flat_asc" : "flat_desc";
        OwnerSort = sortOrder=="owner_desc" ? "owner_asc" : "owner_desc";

        Addresses = _addressService.GetAddresses(sortOrder);
    }
}

