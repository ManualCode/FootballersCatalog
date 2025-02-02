using FootballersCatalog.DataAcces.Entities;
using FootballersCatalog.Domain.Abstractions.Repositories;
using FootballersCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballersCatalog.DataAcces.Repositories
{
    public class FootballerRepository: IFootballerRepository
    {
        private readonly FootballersCatalogDbContext dbContext;

        public FootballerRepository(FootballersCatalogDbContext dbContext)
            => this.dbContext = dbContext;
        
        public async Task<List<Footballer>> GetAsync()
        {
            var footballersEntities = await dbContext.Footballers
                .AsNoTracking().Include(f => f.Country).Include(f => f.Team).ToListAsync();
            
            var footballers = footballersEntities
                .Select(f => Footballer.Create(f.Id, f.FirstName, f.LastName, f.Gender, f.Birthday,
                    Team.Create(f.Team.Id, f.Team.TeamName, []).Value,
                    Country.Create(f.Country.Id, f.Country.CountryName).Value).Value)
                .ToList();

            return footballers;
        }

        public async Task<Footballer> GetById(Guid Id)
        {
            var footballerEntity = await dbContext.Footballers
                .AsNoTracking().Where(f => f.Id == Id).Include(f => f.Team).Include(f => f.Country).FirstOrDefaultAsync();

            if (footballerEntity == null) return null;

            return Footballer.Create(
                    footballerEntity.Id,
                    footballerEntity.FirstName,
                    footballerEntity.LastName,
                    footballerEntity.Gender,
                    footballerEntity.Birthday,
                    Team.Create(footballerEntity.Team.Id, footballerEntity.Team.TeamName).Value,
                    Country.Create(footballerEntity.Country.Id, footballerEntity.Country.CountryName).Value).Value;
        }

        public async Task<Guid> CreateAsync(Footballer footballer, Guid teamId, Guid countryId)
        {
            var footballerEntity = new FootballerEntity
            {
                Id = footballer.Id,
                FirstName = footballer.FirstName,
                LastName = footballer.LastName,
                Gender = footballer.Gender,
                Birthday = footballer.Birthday,
                TeamId = teamId,
                CountryId = countryId,
                CreatedDate = DateTime.UtcNow,
            };

            await dbContext.Footballers.AddAsync(footballerEntity);
            await dbContext.SaveChangesAsync();

            return footballerEntity.Id;
        }


        public async Task<Guid> UpdateAsync(Guid id, string firstName, string lastName, string gender, DateOnly birthday, Guid teamId, Guid countryId)
        {
            await dbContext.Footballers
                .Where(f => f.Id == id)
                .Include(f => f.Team).Include(f => f.Country)
                .ExecuteUpdateAsync(s => s
                .SetProperty(f => f.FirstName, firstName)
                .SetProperty(f => f.LastName, lastName)
                .SetProperty(f => f.Gender, gender)
                .SetProperty(f => f.Birthday, birthday)
                .SetProperty(f => f.TeamId, teamId)
                .SetProperty(f => f.CountryId, countryId));

            return id;
        }

        public async Task DeleteAsync(Guid id)
           => await dbContext.Footballers
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();
    }
}
