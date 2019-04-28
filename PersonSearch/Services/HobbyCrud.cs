using PersonSearch.Data.Context;
using PersonSearch.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PersonSearch.Services
{
    /// <summary>
    /// Create-Read-Update-Delete functionality around Hobby objects
    /// </summary>
    public class HobbyCrud
    {
        private readonly IPersonSearchContext _dbContext;

        public HobbyCrud(IPersonSearchContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Returns all available hobbies
        /// </summary>
        /// <returns></returns>
        public async Task<List<Hobby>> GetAll()
        {
            return await _dbContext.Hobbies.ToListAsync();
        }
    }
}