using PersonSearch.Data.Enums;
using PersonSearch.Services;
using System.Web.ModelBinding;

namespace PersonSearch.Models
{
    public class Address : IAddressData
    {
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        [BindNever]
        public string PrettyState { get; set; }

        public State State { get; set; }

        public int ZipCode { get; set; }
    }
}