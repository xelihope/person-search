using PersonSearch.Data.Entities;
using PersonSearch.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonSearch.Data.Seed
{
    /// <summary>
    /// Hobby test data
    /// </summary>
    public class HobbySeed : ITestDataSeed<Hobby>
    {
        public IList<Hobby> Get()
        {
            return Enum.GetValues(typeof(HobbyType)).Cast<HobbyType>()
                .Select(x => new Hobby { HobbyType = x }).ToList();
        }
    }
}