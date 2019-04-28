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
    public class HobbyCrudTests
    {
        [Test]
        public void GetAll_ReturnsAllHobbies()
        {
            // Arrange
            var dbContextMock = new Mock<IPersonSearchContext>();
            var hobbies = new List<Hobby> {
                new Hobby { HobbyId = 1 },
                new Hobby { HobbyId = 2 }
            }.AsQueryable();
            var dbSetMock = EntityFrameworkHelper.GetMockDbSet(hobbies);
            dbContextMock.Setup(x => x.Hobbies).Returns(dbSetMock.Object);

            // Act
            var results = new HobbyCrud(dbContextMock.Object).GetAll()
                .Result;

            // Assert
            CollectionAssert.AreEqual(hobbies, results);
        }
    }
}
