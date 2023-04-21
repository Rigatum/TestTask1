using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dadata.Model;

namespace TestTask1.Contracts
{
    public interface IAddressService
    {
        Address GetCleanAddress(string dirtyAdress);
    }
}