using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask1.Contracts;
using TestTask1.Models;
using TestTask1.Pages;
using TestTask1.Data;
namespace TestTask1.Pages.Addresses
{
    public class Add : PageModel
    {
        [BindProperty]
        public string address { get; set; }
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
            
        }
    }
}