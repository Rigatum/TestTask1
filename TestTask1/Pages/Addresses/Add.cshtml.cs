using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask1.Contracts;
using TestTask1.Models;
namespace TestTask1.Pages.Addresses
{
    public class Add : PageModel
    {
        [BindProperty]
        public string address { get; set; }
        public string token { get; set; }
        private readonly ILogger<Add> _logger;
        private readonly DadataClientSettings _dadataClientSettings;
        private readonly IConfiguration _configuration;

        public Add(ILogger<Add> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _configuration = Configuration;
            _dadataClientSettings = _configuration.GetSection("DadataClient").Get<DadataClientSettings>();
            token = _dadataClientSettings.token;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            
        }
    }
}