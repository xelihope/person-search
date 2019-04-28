using PersonSearch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonSearch.Data.Entities
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }

        public HobbyType HobbyType { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public Hobby()
        {
            People = new HashSet<Person>();
        }
    }
}