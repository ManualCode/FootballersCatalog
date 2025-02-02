using FootballersCatalog.DataAcces.Entities;
using FootballersCatalog.Domain.Abstractions.Repositories;
using FootballersCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballersCatalog.DataAcces.Repositories
{
    public class TeamRepository: ITeamRepository
    {
        private readonly FootballersCatalogDbContext dbContext;

        public TeamRepository(FootballersCatalogDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<List<Team>> GetAsync()
        {
            var teamsEntities = await dbContext.Teams
                .AsNoTracking().Include(f => f.Footballers).ThenInclude(f => f.Country).ToListAsync();

            return teamsEntities.Select(t => Team.Create(t.Id, t.TeamName,
                t.Footballers.Select(f =>
                    Footballer.Create(f.Id, f.FirstName, f.LastName, f.Gender, f.Birthday,
                        Team.Create(f.Team.Id, f.Team.TeamName, []).Value,
                        Country.Create(f.Country.Id, f.Country.CountryName).Value)
                .Value).ToList()).Value).ToList();
        }

        public async Task AddAsync(Team team)
        {
            var teamEntity = new TeamEntity
            {
                Id = team.Id,
                TeamName = team.TeamName
            };

            await dbContext.Teams.AddAsync(teamEntity);

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
            => await dbContext.Teams.Where(t => t.Id == id).ExecuteDeleteAsync();

        public async Task<Team> FindOrCreateAsync(Team team)
        {
            var existingTeam = await dbContext.Teams.FirstOrDefaultAsync(t => t.TeamName == team.TeamName);

            if (existingTeam == null)
            {
                existingTeam = new TeamEntity
                {
                    Id = team.Id,
                    TeamName = team.TeamName
                };
                dbContext.Teams.Add(existingTeam);
                dbContext.SaveChanges();
            }

            return Team.Create(existingTeam.Id, existingTeam.TeamName, []).Value;
        }

        public async Task<Team> GetTeamAsync(Guid id)
        {
            var teamWithFootballers = await dbContext.Teams
                .Include(t => t.Footballers)
                .ThenInclude(f => f.Country)
                .FirstOrDefaultAsync(t => t.Id == id);

            var t = teamWithFootballers.Footballers.Select(f => Footballer.Create(f.Id, f.FirstName, f.LastName, f.Gender, f.Birthday,
                Team.Create(Guid.NewGuid(), f.Team.TeamName).Value,
                Country.Create(Guid.NewGuid(), f.Country.CountryName).Value)
                .Value).ToList();

            return Team.Create(teamWithFootballers.Id, teamWithFootballers.TeamName, t).Value;
        }

        public async Task<Team> GetTeamByNameAsync(string name)
        {
            var teamEntity = await dbContext.Teams.FirstOrDefaultAsync(t => t.TeamName  == name);

            return Team.Create(teamEntity.Id, teamEntity.TeamName).Value;
        }
    }
}