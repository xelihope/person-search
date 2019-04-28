using Moq;
using NUnit.Framework;
using PersonSearch.Data.Enums;
using PersonSearch.Services;
using System;
using System.Collections.Generic;

namespace PersonSearch.Tests.Services
{
    [TestFixture]
    public class PersonValidatorTests
    {
        /// <summary>
        /// First Name is always required for a Person
        /// </summary>
        /// <param name="firstName"></param>
        [TestCase(null)]
        [TestCase("   \n  ")]
        public void GetErrors_ErrorForMissingFirstName(string firstName)
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.FirstName).Returns(firstName);

            // Act
            var errors = new PersonValidator().GetErrors(personMock.Object);

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { Resources.Person.FirstNameRequiredError }, errors);
        }

        /// <summary>
        /// Last Name is always required for a Person
        /// </summary>
        /// <param name="lastName"></param>
        [TestCase(null)]
        [TestCase("   \n  ")]
        public void GetErrors_ErrorForMissingLastName(string lastName)
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.LastName).Returns(lastName);

            // Act
            var errors = new PersonValidator().GetErrors(personMock.Object);

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { Resources.Person.LastNameRequiredError }, errors);
        }

        /// <summary>
        /// Default value for dates of minimum allowed is invalid for a Person's Birth Date
        /// and Birth Date is required
        /// </summary>
        [Test]
        public void GetErrors_ErrorForBadBirthDate()
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.BirthDate).Returns(DateTime.MinValue);

            // Act
            var errors = new PersonValidator().GetErrors(personMock.Object);

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { Resources.Person.BirthDateRequiredError }, errors);
        }

        /// <summary>
        /// City is required when an address is provided for a Person
        /// </summary>
        /// <param name="lastName"></param>
        [TestCase(null)]
        [TestCase("   \n  ")]
        public void GetErrors_ErrorForMissingCity(string city)
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.Address.City).Returns(city);

            // Act
            var validator = new PersonValidator();
            var errors = validator.GetErrors(personMock.Object);

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { Resources.Person.CityRequiredError }, errors);
        }

        /// <summary>
        /// State is required when an address is provided for a Person
        /// </summary>
        /// <param name="state"></param>
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(500)]
        public void GetErrors_ErrorForMissingState(State state)
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.Address.State).Returns(state);

            // Act
            var validator = new PersonValidator();
            var errors = validator.GetErrors(personMock.Object);

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { Resources.Person.StateRequiredError }, errors);
        }

        /// <summary>
        /// Zip Code is required when an address is provided for a Person.
        /// Zip Codes are numbers with 5-digits.
        /// </summary>
        /// <param name="state"></param>
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(9999)]
        [TestCase(100000)]
        public void GetErrors_ErrorForBadZipCode(int zipCode)
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.Address.ZipCode).Returns(zipCode);

            // Act
            var validator = new PersonValidator();
            var errors = validator.GetErrors(personMock.Object);

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { Resources.Person.ZipCodeRequiredAndNumericError }, errors);
        }

        /// <summary>
        /// Address is not required for a Person
        /// </summary>
        [Test]
        public void GetErrors_ValidWithNullAddress()
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.Address).Returns((IAddressData)null);

            // Act
            var validator = new PersonValidator();
            var errors = validator.GetErrors(personMock.Object);

            // Assert
            Assert.IsEmpty(errors);
        }

        /// <summary>
        /// Generic valid data
        /// </summary>
        [Test]
        public void GetErrors_Valid()
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.Hobbies).Returns(new Dictionary<int, bool> { { 1, true }, { 2, false } });

            // Act
            var validator = new PersonValidator();
            var errors = validator.GetErrors(personMock.Object);

            // Assert
            Assert.IsEmpty(errors);
        }

        /// <summary>
        /// Validator should return all errors for usage by user/requester
        /// </summary>
        [Test]
        public void GetErrors_ReturnsMultipleErrors()
        {
            // Arrange
            var personMock = GetPersonMock();
            personMock.SetupGet(x => x.FirstName).Returns("");
            personMock.SetupGet(x => x.LastName).Returns("");
            personMock.SetupGet(x => x.Address.State).Returns(0);

            // Act
            var validator = new PersonValidator();
            var errors = validator.GetErrors(personMock.Object);

            // Assert
            CollectionAssert.AreEquivalent(new List<string> {
                Resources.Person.FirstNameRequiredError,
                Resources.Person.LastNameRequiredError,
                Resources.Person.StateRequiredError }, errors);
        }

        private Mock<IPersonData> GetPersonMock()
        {
            var personMock = new Mock<IPersonData>();
            personMock.SetupGet(x => x.FirstName).Returns("John");
            personMock.SetupGet(x => x.LastName).Returns("Smith");
            personMock.SetupGet(x => x.BirthDate).Returns(new DateTime(2015, 01, 01));
            personMock.SetupGet(x => x.Address.City).Returns("Chicago");
            personMock.SetupGet(x => x.Address.State).Returns(State.Illinois);
            personMock.SetupGet(x => x.Address.ZipCode).Returns(99999);

            return personMock;
        }
    }
}
