using PersonSearch.Data.Entities;
using System;
using System.Collections.Generic;

namespace PersonSearch.Data.Seed
{
    /// <summary>
    /// Person test data
    /// </summary>
    public class PersonSeed : ITestDataSeed<Person>
    {
        public IList<Person> Get()
        {
            return new List<Person> {
                new Person { PersonId = 1, FirstName = "Elizabeth", LastName = "Gorden", BirthDate = new DateTime(1940, 7, 24), PictureFile = "user1.png" },
                new Person { PersonId = 2, FirstName = "Aaron", LastName = "Van de Merkt", BirthDate = new DateTime(1950, 3, 5), PictureFile = "user2.png" },
                new Person { PersonId = 3, FirstName = "Eric", LastName = "Bailey", BirthDate = new DateTime(1988, 10, 22), PictureFile = "" },
                new Person { PersonId = 4, FirstName = "Larry", LastName = "Smith", BirthDate = new DateTime(1999, 7, 10), PictureFile = "user3.png" },
                new Person { PersonId = 5, FirstName = "Bill", LastName = "Bob", BirthDate = new DateTime(2000, 9, 9), PictureFile = "" },
                new Person { PersonId = 6, FirstName = "Liza", LastName = "Smith", BirthDate = new DateTime(1978, 1, 1), PictureFile = "user4.gif" },
                new Person { PersonId = 7, FirstName = "Gert", LastName = "Gole", BirthDate = new DateTime(1955, 6, 24), PictureFile = "user5.jpg" },
                new Person { PersonId = 8, FirstName = "Billy", LastName = "Bobby", BirthDate = new DateTime(2010, 3, 28), PictureFile = "user6.jpg" },
                new Person { PersonId = 9, FirstName = "Ahmed", LastName = "Saharaz", BirthDate = new DateTime(1975, 12, 30), PictureFile = "user7.jpg" },
                new Person { PersonId = 10, FirstName = "Jaina", LastName = "Proudmoore", BirthDate = new DateTime(1985, 2, 2), PictureFile = "user8.jpg" },
                new Person { PersonId = 11, FirstName = "Mario", LastName = "Mario", BirthDate = new DateTime(1977, 8, 18), PictureFile = "" },
                new Person { PersonId = 12, FirstName = "Anduin", LastName = "Wrynn", BirthDate = new DateTime(1996, 7, 24), PictureFile = "" },
                new Person { PersonId = 13, FirstName = "Steve", LastName = "Irwin", BirthDate = new DateTime(1900, 2, 28), PictureFile = "" },
                new Person { PersonId = 14, FirstName = "Names", LastName = "Ugh", BirthDate = new DateTime(2005, 11, 18), PictureFile = "" },
                new Person { PersonId = 15, FirstName = "Rosie", LastName = "Gorden", BirthDate = new DateTime(2005, 3, 3), PictureFile = "" },
                new Person { PersonId = 16, FirstName = "Master", LastName = "Chief", BirthDate = new DateTime(1970, 12, 31), PictureFile = "" },
                new Person { PersonId = 17, FirstName = "Peach", LastName = "Toadstool", BirthDate = new DateTime(1995, 9, 10), PictureFile = "user9.jpg" },
                new Person { PersonId = 18, FirstName = "Alfonse", LastName = "Askr", BirthDate = new DateTime(1998, 10, 02), PictureFile = "" },
                new Person { PersonId = 19, FirstName = "John", LastName = "Jones", BirthDate = new DateTime(1940, 7, 24), PictureFile = "" },
                new Person { PersonId = 20, FirstName = "Ash", LastName = "Ketchum", BirthDate = new DateTime(2007, 10, 20), PictureFile = "" },
            };
        }
    }
}