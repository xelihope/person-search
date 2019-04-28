using PersonSearch.Data.Context;
using PersonSearch.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PersonSearch.Services
{
    /// <summary>
    /// Create-Read-Update-Delete functionality around Person objects and their common foreign children
    /// </summary>
    public class PersonCrud
    {
        private readonly IPersonSearchContext _dbContext;

        public PersonCrud(IPersonSearchContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Returns all Person objects paged by the input page size and page number,
        /// filtered by whether or not the first or last name contains the name filter string.
        /// The output is paged by a first name ordering.
        /// </summary>
        /// <param name="pageSize">Count of Person objects to return</param>
        /// <param name="page">Page number of Person objects to return</param>
        /// <param name="nameFilter">String to filter Person objects by first or last name containment</param>
        /// <returns></returns>
        public async Task<List<Person>> Get(int pageSize, int page, string nameFilter)
        {
            var people = FilterByName(_dbContext.People.Include(nameof(Person.Address)).Include(nameof(Person.Hobbies)), nameFilter);
            return await people.OrderBy(p => p.FirstName)
                .Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        /// <summary>
        /// Returns the count of Person objects that satisfy the filter criteria.
        /// </summary>
        /// <param name="nameFilter">String to filter Person objects by first or last name containment</param>
        /// <returns></returns>
        public async Task<int> GetCount(string nameFilter)
        {
            var people = FilterByName(_dbContext.People, nameFilter);
            return await people.CountAsync();
        }

        private IQueryable<Person> FilterByName(IQueryable<Person> query, string nameFilter)
        {
            if (nameFilter != null) {
                return query.Where(p => p.FirstName.Contains(nameFilter) 
                    || p.LastName.Contains(nameFilter)
                    || p.FirstName + " " + p.LastName == nameFilter);
            }
            return query;
        }

        /// <summary>
        /// Creates a new Person object using input data
        /// </summary>
        /// <param name="data">Person related data</param>
        /// <returns>The newly created Person object</returns>
        public async Task<int> Create(IPersonData data)
        {
            var newPerson = new Person {
                FirstName = data.FirstName,
                LastName = data.LastName,
                BirthDate = data.BirthDate
            };
            if (data.Address != null) {
                newPerson.Address = new Address {
                    Street1 = data.Address?.Street1,
                    Street2 = data.Address?.Street2,
                    City = data.Address.City,
                    State = data.Address.State,
                    ZipCode = data.Address.ZipCode
                };
            }

            if (data.Hobbies != null && data.Hobbies.Any()) {
                var hobbies = data.Hobbies.Where(kvp => kvp.Value).Select(kvp => new Hobby { HobbyId = kvp.Key }).ToList();
                foreach (var hobby in hobbies) {
                    _dbContext.Hobbies.Attach(hobby);
                }
                newPerson.Hobbies = hobbies;
            }

            _dbContext.People.Add(newPerson);

            await _dbContext.SaveChangesAsync();

            return newPerson.PersonId;
        }

        /// <summary>
        /// Updates the avatar picture for the person with the given ID to the input file.
        /// </summary>
        /// <param name="id">PersonId</param>
        /// <param name="filePath">Path to new picture</param>
        /// <returns></returns>
        public async Task<int> UpdatePicture(int id, string fileName)
        {
            var person = new Person { PersonId = id };
            _dbContext.People.Attach(person);

            person.PictureFile = fileName;
            return await _dbContext.SaveChangesAsync();
        }
    }
}