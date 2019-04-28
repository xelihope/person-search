using System;
using System.Collections.Generic;
using PersonSearch.Data.Enums;
using PersonSearch.Helpers;

namespace PersonSearch.Services
{
    public class PersonValidator
    {
        /// <summary>
        /// Return all invalid errors for the input Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public IList<string> GetErrors(IPersonData person)
        {
            var errors = new List<string>();

            if (person.FirstName.IsNullOrBlank()) {
                errors.Add(Resources.Person.FirstNameRequiredError);
            }

            if (person.LastName.IsNullOrBlank()) {
                errors.Add(Resources.Person.LastNameRequiredError);
            }

            if (person.BirthDate == DateTime.MinValue) {
                errors.Add(Resources.Person.BirthDateRequiredError);
            }

            if (person.Address != null) {
                if (person.Address.City.IsNullOrBlank()) {
                    errors.Add(Resources.Person.CityRequiredError);
                }

                if (!Enum.IsDefined(typeof(State), person.Address.State)) {
                    errors.Add(Resources.Person.StateRequiredError);
                }

                if (person.Address.ZipCode <= 9999
                        || person.Address.ZipCode >= 100000) {
                    errors.Add(Resources.Person.ZipCodeRequiredAndNumericError);
                }
            }

            return errors;
        }
    }
}