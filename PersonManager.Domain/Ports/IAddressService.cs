using PersonManager.Domain.Entities;

namespace PersonManager.Domain.Ports
{
    public interface IAddressService
    {
        Task<Address> GetAddressByZipCodeAsync(string zipCode);
    }
}
