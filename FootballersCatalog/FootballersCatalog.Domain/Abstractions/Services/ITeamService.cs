using FootballersCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.Domain.Abstractions.Services
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeams();

        Task DeleteEmptyTeam(Guid teamId);
    }
}
