using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask1.Contracts;

namespace TestTask1.Pages.Addresses
{
    public class Add : PageModel
    {
        [BindProperty]
        public string dirtyAddress { get; set; }
        private readonly ILogger<Add> _logger;
        private readonly IAddressService _addressService;

        public Add(ILogger<Add> logger, IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var test = _addressService.GetCleanAddress(dirtyAddress);
            System.Console.WriteLine(test.city);
        }
    }
}