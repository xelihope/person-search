using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonSearch.Data.Entities
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string PictureFile { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Hobby> Hobbies { get; set; }

        public Person()
        {
            Hobbies = new HashSet<Hobby>();
        }
    }
}