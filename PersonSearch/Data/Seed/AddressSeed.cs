using PersonSearch.Data.Entities;
using PersonSearch.Data.Enums;
using System.Collections.Generic;

namespace PersonSearch.Data.Seed
{
    /// <summary>
    /// Address test data
    /// </summary>
    public class AddressSeed : ITestDataSeed<Address>
    {
        public IList<Address> Get()
        {
            return new List<Address> {
                new Address { AddressId = 1, Street1 = "4431 N Winchester Ave", Street2 = "Apt 2B", City = "Chicago", State = State.Illinois, ZipCode = 60640 },
                new Address { AddressId = 2, Street1 = "349 Mary Lane", Street2 = "", City = "Crystal Lake", State = State.Illinois, ZipCode = 60014 }
            };
        }
    }
}