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
    public void OnGet()
    {
        Addresses = _addressService.GetAddresses();
    }
}

