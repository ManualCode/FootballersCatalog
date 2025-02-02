using FootballersCatalog.DataAcces.Repositories;
using FootballersCatalog.Domain.Abstractions.Repositories;
using FootballersCatalog.Domain.Abstractions.Services;
using FootballersCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return await teamRepository.GetAsync();
        }

        public async Task DeleteEmptyTeam(Guid teamId)
        {
            if ((await teamRepository.GetTeamAsync(teamId)).Footballers.Count() == 0)
                await teamRepository.DeleteAsync(teamId);
        }
    }
}
