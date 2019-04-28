using System.Collections.Generic;

namespace PersonSearch.Data.Seed
{
    /// <summary>
    /// Interface for a provider of test data for a single type of db entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ITestDataSeed<T>
    {
        /// <summary>
        /// Returns all test data to be stored for entity T
        /// </summary>
        /// <returns></returns>
        IList<T> Get();
    }
}