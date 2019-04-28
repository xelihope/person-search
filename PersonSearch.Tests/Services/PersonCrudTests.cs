using Moq;
using System.Linq;
using NUnit.Framework;
using PersonSearch.Data.Context;
using PersonSearch.Data.Entities;
using PersonSearch.Services;
using System.Collections.Generic;

namespace PersonSearch.Tests.Services
{
    [TestFixture]
    public class PersonCrudTests
    {
        private static IList<TestCaseData> _getTestCases = new List<TestCaseData> {
            new TestCaseData(2, 1, "", new List<string> { "Leroy", "Zelda" }).SetName("Simple paging no filter"),
            new TestCaseData(100, 0, "", new List<string> { "Able", "Alice", "Leroy", "Zelda", "Zoo" })
                .SetName("Get all no filter"),
            new TestCaseData(4, 4, "", new List<string>()).SetName("Empty for invalid page"),
            new TestCaseData(2, 0, "myfilter", new List<string>()).SetName("Empty for no filter match"),
            new TestCaseData(2, 0, "in", new List<string> { "Leroy", "Zelda" }).SetName("Filter matches on last name"),
            new TestCaseData(2, 0, "Ze", new List<string> { "Zelda", "Zoo" }).SetName("Filter matches on first and last name"),
            new TestCaseData(2, 0, "Leroy Jenkins", new List<string> { "Leroy" }).SetName("Filter matches on full name"),
            new TestCaseData(0, 0, "Leroy Jenkins", new List<string>()).SetName("0 page size returns none")
        };

        [TestCaseSource(nameof(_getTestCases))]
        public void Get_ReturnsPagedOrderedAndFilteredByName(int pageSize, int page, string nameFilter, 
            IList<string> expectedFirstNames)
        {
            // Arrange
            var dbContextMock = new Mock<IPersonSearchContext>();
            var people = new List<Person> {
                new Person { PersonId = 1, FirstName = "Able", LastName = "Gable" },
                new Person { PersonId = 2, FirstName = "Zelda", LastName = "Link" },
                new Person { PersonId = 3, FirstName = "Leroy", LastName = "Jenkins" },
                new Person { PersonId = 4, FirstName = "Zoo", LastName = "Zebra" },
                new Person { PersonId = 5, FirstName = "Alice", LastName = "Wonderland" },
            }.AsQueryable();
            var dbSetMock = EntityFrameworkHelper.GetMockDbSet(people);
            dbContextMock.Setup(x => x.People).Returns(dbSetMock.Object);

            // Act
            var results = new PersonCrud(dbContextMock.Object).Get(pageSize, page, nameFilter)
                .Result;

            // Assert
            CollectionAssert.AreEqual(expectedFirstNames, results.Select(x => x.FirstName));
        }

        private static IList<TestCaseData> _getCountTestCases = new List<TestCaseData> {
            new TestCaseData("", 5).SetName("No filter returns all"),
            new TestCaseData(null, 5).SetName("Null filter returns all"),
            new TestCaseData("myFilter", 0).SetName("No matching elements returns 0"),
            new TestCaseData("a", 4).SetName("Filter matches first and last name"),
            new TestCaseData("Leroy Jenkins", 1).SetName("Filter matches full name")
        };

        [TestCaseSource(nameof(_getCountTestCases))]
        public void GetCount_ReturnsPersonCountFilteredByName(string nameFilter, int expected)
        {
            // Arrange
            var dbContextMock = new Mock<IPersonSearchContext>();
            var people = new List<Person> {
                new Person { PersonId = 1, FirstName = "Able", LastName = "Gable" },
                new Person { PersonId = 2, FirstName = "Zelda", LastName = "Link" },
                new Person { PersonId = 3, FirstName = "Leroy", LastName = "Jenkins" },
                new Person { PersonId = 4, FirstName = "Zoo", LastName = "Zebra" },
                new Person { PersonId = 5, FirstName = "Alice", LastName = "Wonderland" },
            }.AsQueryable();
            var dbSetMock = EntityFrameworkHelper.GetMockDbSet(people);
            dbContextMock.Setup(x => x.People).Returns(dbSetMock.Object);

            // Act
            var result = new PersonCrud(dbContextMock.Object).GetCount(nameFilter)
                .Result;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Create_SimplePerson_SavesNewPerson()
        {
            // Arrange
            var dbContextMock = new Mock<IPersonSearchContext>();
            var people = EntityFrameworkHelper.GetMockDbSet(new List<Person>().AsQueryable());
            var hobbies = EntityFrameworkHelper.GetMockDbSet(new List<Hobby> {
                new Hobby { HobbyId = 1, },
                new Hobby { HobbyId = 2, },
                new Hobby { HobbyId = 3, },
            }.AsQueryable());
            dbContextMock.Setup(x => x.People).Returns(people.Object);
            dbContextMock.Setup(x => x.Hobbies).Returns(hobbies.Object);

            var personData = new Mock<IPersonData>();
            var addressData = new Mock<IAddressData>().Object;
            personData.Setup(d => d.Address).Returns(addressData);
            personData.Setup(d => d.Hobbies).Returns(new Dictionary<int, bool> {
                { 1, true },
                { 2, true },
                { 3, false }
            });

            // Act
            var result = new PersonCrud(dbContextMock.Object).Create(personData.Object);

            // Assert
            dbContextMock.Verify(d => d.SaveChangesAsync(), Times.Once);
            people.Verify(x => x.Add(It.Is<Person>(p => p.Hobbies.Count() == 2 && p.Address != null)), 
                Times.Once);
        }

        [Test]
        public void Create_DoesNotThrowOnNulls()
        {
            // Arrange
            var dbContextMock = new Mock<IPersonSearchContext>();
            var people = EntityFrameworkHelper.GetMockDbSet(new List<Person>().AsQueryable());
            var hobbies = EntityFrameworkHelper.GetMockDbSet(new List<Hobby>().AsQueryable());
            dbContextMock.Setup(x => x.People).Returns(people.Object);
            dbContextMock.Setup(x => x.Hobbies).Returns(hobbies.Object);

            var personData = new Mock<IPersonData>();
            personData.Setup(d => d.Address).Returns((IAddressData)null);
            personData.Setup(d => d.Hobbies).Returns((Dictionary<int, bool>)null);

            // Act
            var result = new PersonCrud(dbContextMock.Object).Create(personData.Object);

            // Assert
            dbContextMock.Verify(d => d.SaveChangesAsync(), Times.Once);
            people.Verify(x => x.Add(It.Is<Person>(p => p.Hobbies.Count() == 0 && p.Address == null)), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("TestPicture")]
        public void UpdatePicture_UpdatesPicture(string fileName)
        {
            // Arrange
            var dbContextMock = new Mock<IPersonSearchContext>();
            var people = EntityFrameworkHelper.GetMockDbSet(new List<Person> {
                new Person { PersonId = 1 }
            }.AsQueryable());
            dbContextMock.Setup(x => x.People).Returns(people.Object);
            Person person = new Person();
            people.Setup(p => p.Attach(It.IsAny<Person>())).Callback((Person p) => person = p);

            // Act
            var result = new PersonCrud(dbContextMock.Object).UpdatePicture(1, fileName);

            // Assert
            dbContextMock.Verify(d => d.SaveChangesAsync(), Times.Once);
            people.Verify(x => x.Attach(It.IsAny<Person>()), Times.Once);
            Assert.AreEqual(fileName, person.PictureFile);
            Assert.AreEqual(1, person.PersonId);
        }
    }
}
