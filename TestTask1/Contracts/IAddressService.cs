using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask1.Contracts
{
    public interface IAddressService
    {
        public Task<string> Insert(string addCityViewModelCityName, string addStreetViewModelStreetName, string addHouseViewModelHouseName,
                                string addFlatViewModelFlatName, string addOwnerViewModelFIO);
    }
}