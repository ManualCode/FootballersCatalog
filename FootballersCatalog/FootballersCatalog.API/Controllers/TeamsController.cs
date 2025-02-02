using FootballersCatalog.API.Contracts;
using FootballersCatalog.Domain.Abstractions.Services;
using FootballersCatalog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballersCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService teamService;

        public TeamsController(ITeamService teamService) => this.teamService = teamService;

        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetTeams()
        {
            var teams = await teamService.GetAllTeams();

            var teamsResponse = teams.Select(t =>
                new TeamsResponse(t.Id, t.TeamName,
                    t.Footballers.Select(f =>
                        new FootballersResponse(f.Id, f.FirstName, f.LastName, f.Gender, f.Birthday,
                            f.Team.TeamName, f.Country.CountryName, f.UpdatedDate)).ToList()));

            return Ok(teamsResponse);
        }
    }
}
