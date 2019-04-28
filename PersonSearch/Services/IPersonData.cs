using PersonSearch.Data.Enums;
using System;
using System.Collections.Generic;

namespace PersonSearch.Services
{
    /// <summary>
    /// Interface for all updateable properties for a Person
    /// </summary>
    public interface IPersonData
    {
        string FirstName { get; }
        string LastName { get; }
        DateTime BirthDate { get; }
        IAddressData Address { get; }
        Dictionary<int, bool> Hobbies { get; }
    }
}
