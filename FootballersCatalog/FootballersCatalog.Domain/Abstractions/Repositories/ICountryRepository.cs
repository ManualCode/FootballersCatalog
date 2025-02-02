using FootballersCatalog.Domain.Models;

namespace FootballersCatalog.Domain.Abstractions.Repositories
{
    public interface ICountryRepository
    {
        Task AddAsync(Country country);
        Task<Country> FindOrCreateAsync(Country country);
    }
}