using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dadata;
using Dadata.Model;
using TestTask1.Models;
using TestTask1.Models.SecretKeys;
using TestTask1.Contracts;
namespace TestTask1.Services
{
    public class AddressService : IAddressService
    {
        private readonly IConfiguration _configuration;
        private readonly DadataClientSettings _dadataClientSettings;
        private readonly CleanClient _cleanClient;
        public AddressService(IConfiguration Configuration)
        {
            _configuration = Configuration;
            _dadataClientSettings = _configuration.GetSection("DadataClient").Get<DadataClientSettings>();
            _cleanClient = new CleanClient(_dadataClientSettings.token, _dadataClientSettings.secret);
        }

        public Address GetCleanAddress(string dirtyAdress)
        {
            return _cleanClient.Clean<Address>(dirtyAdress);
        }
    }
}