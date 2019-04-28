using PersonSearch.Data.Context;
using PersonSearch.Data.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersonSearch.Data.Seed
{
    /// <summary>
    /// Initializer of Person Search test data
    /// </summary>
    public class PersonSearchDbInitializer : DropCreateDatabaseAlways<PersonSearchContext>
    {
        protected override void Seed(PersonSearchContext context)
        {
            var people = new PersonSeed().Get();
            var addresses = new AddressSeed().Get();
            var hobbies = new HobbySeed().Get();

            // Set relationships
            var peopleHobbies = new Dictionary<int, IList<HobbyType>> {
                { 1, new List<HobbyType> { HobbyType.VideoGames, HobbyType.Programming, HobbyType.Art } },
                { 2, new List<HobbyType> { HobbyType.VideoGames, HobbyType.Television, HobbyType.Writing } },
            };
            var addressDict = addresses.ToDictionary(a => a.AddressId, a => a);
            var peopleAddress = new Dictionary<int, int> {
                { 1, 1 },
                { 2, 2 }
            };
            foreach (var person in people)
            {
                if (peopleHobbies.ContainsKey(person.PersonId))
                {
                    person.Hobbies = hobbies.Select(h => h)
                        .Where(h => peopleHobbies[person.PersonId].Contains(h.HobbyType)).ToList();
                }
                if (peopleAddress.ContainsKey(person.PersonId))
                {
                    person.Address = addressDict[peopleAddress[person.PersonId]];
                }
            }

            // Populate tables
            context.Hobbies.AddRange(hobbies);
            context.People.AddRange(people);

            context.SaveChanges();
        }
    }
}