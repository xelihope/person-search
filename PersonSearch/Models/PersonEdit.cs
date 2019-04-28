using PersonSearch.Data.Enums;
using PersonSearch.Services;
using System;
using System.Collections.Generic;
using System.Web.ModelBinding;

namespace PersonSearch.Models
{
    public class PersonEdit : IPersonData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Address Address { get; set; }

        public Dictionary<int, bool> Hobbies { get; set; }

        [BindNever]
        IAddressData IPersonData.Address => Address;
    }
}