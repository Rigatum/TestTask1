using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask1.Data;
using TestTask1.Models.Domain;
using Microsoft.EntityFrameworkCore;
using TestTask1.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestTask1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IAddressService _addressService;
    public List<Address> Addresses { get; set; }
    public IndexModel(ILogger<IndexModel> logger, IAddressService addressService)
    {
        _logger = logger;
        _addressService = addressService;
    }
    public string CitySort { get; set; }
    public string StreetSort { get; set; }
    public string HouseSort { get; set; }
    public string FlatNumberSort { get; set; }
    public List<SelectListItem> Cities { get; set; }
    public void OnGet(string sortOrder, string searchString)
    {
        Cities = _addressService.GetCities();
        if (searchString != null)
            Addresses = _addressService.FindAddressByCity(searchString);
        else 
        {
            CitySort = sortOrder=="city_desc" ? "city_asc" : "city_desc";
            StreetSort = sortOrder=="street_desc" ? "street_asc" : "street_desc";
            HouseSort = sortOrder=="house_desc" ? "house_asc" : "house_desc";
            FlatNumberSort = sortOrder=="flat_number_desc" ? "flat_number_asc" : "flat_number_desc";
            Addresses = _addressService.GetAddresses(sortOrder);
        }
    }
    public void OnGetByHouse()
    {
        Cities = _addressService.GetCities();
        string s = (string)RouteData.Values["id"];
        Addresses = _addressService.FindAddressByHouse(int.Parse(s));
    }
}
