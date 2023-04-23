using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask1.Data;
using TestTask1.Models.Domain;
using Microsoft.EntityFrameworkCore;
using TestTask1.Contracts;
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

    public void OnGet()
    {
        Addresses = _addressService.GetAddresses();
    }
}
