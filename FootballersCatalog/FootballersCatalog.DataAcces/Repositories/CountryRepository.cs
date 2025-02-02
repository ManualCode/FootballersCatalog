using FootballersCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using FootballersCatalog.Domain.Abstractions.Repositories;
using FootballersCatalog.DataAcces.Entities;

namespace FootballersCatalog.DataAcces.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly FootballersCatalogDbContext dbContext;

        public CountryRepository(FootballersCatalogDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task AddAsync(Country country)
        {
            var countryEntity = new CountryEntity
            {
                Id = country.Id,
                CountryName = country.CountryName
            };

            await dbContext.Countries.AddAsync(countryEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Country> FindOrCreateAsync(Country country)
        {
            var existingCountry = await dbContext.Countries.FirstOrDefaultAsync(g => g.CountryName == country.CountryName);

            if (existingCountry == null)
            {
                existingCountry = new CountryEntity
                {
                    Id = country.Id,
                    CountryName = country.CountryName
                };
                dbContext.Entry(existingCountry).State = EntityState.Added;
            }

            return Country.Create(existingCountry.Id,existingCountry.CountryName).Value;
        }
    }
}
