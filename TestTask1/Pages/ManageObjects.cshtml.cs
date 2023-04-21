using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask1.Services;
using Dadata.Model;
using TestTask1.Contracts;
namespace TestTask1.Pages;

public class ManageObjectsModel : PageModel
{
    private IAddressService _addressService; 
    private readonly ILogger<ManageObjectsModel> _logger;
    public Address address;
    

    public ManageObjectsModel(ILogger<ManageObjectsModel> logger, IAddressService addressService)
    {
        _logger = logger;
        _addressService = addressService;
    }

    public void OnGet()
    {
        address = _addressService.GetCleanAddress("");
    }
}

