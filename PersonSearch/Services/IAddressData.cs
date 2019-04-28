using PersonSearch.Data.Enums;

namespace PersonSearch.Services
{
    /// <summary>
    /// Interface for all updateable properties for an Address
    /// </summary>
    public interface IAddressData
    {
        string Street1 { get; }
        string Street2 { get; }
        string City { get; }
        State State { get; }
        int ZipCode { get; }
    }
}
