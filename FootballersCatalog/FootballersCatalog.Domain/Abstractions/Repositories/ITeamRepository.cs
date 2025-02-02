using FootballersCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.Domain.Abstractions.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAsync();

        Task AddAsync(Team team);

        Task<Team> FindOrCreateAsync(Team team);

        Task<Team> GetTeamAsync(Guid id);

        Task<Team> GetTeamByNameAsync(string name);

        Task DeleteAsync(Guid id);
    }
}
