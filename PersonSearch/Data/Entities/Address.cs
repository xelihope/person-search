using PersonSearch.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonSearch.Data.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public State State { get; set; }

        public int ZipCode { get; set; }

        [Required]
        public virtual Person Person { get; set; }
    }
}