using System.Collections.Generic;

namespace PersonSearch.Models
{
    public class PersonView
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string PictureFile { get; set; }

        public Address Address { get; set; }

        public List<string> Hobbies { get; set; }
    }
}