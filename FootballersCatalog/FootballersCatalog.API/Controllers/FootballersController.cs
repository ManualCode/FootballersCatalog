using FootballersCatalog.API.Contracts;
using FootballersCatalog.Domain.Abstractions.Services;
using FootballersCatalog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballersCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FootballersController : ControllerBase
    {
        private readonly IFootballerService footballerService;

        public FootballersController(IFootballerService footballerService)
            => this.footballerService = footballerService;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FootballersResponse>> GetFootballer(Guid id)
        {
            var footballer = await footballerService.GetFootballer(id);

            var responses = new FootballersResponse(footballer.Id, footballer.FirstName,
                footballer.LastName, footballer.Gender, footballer.Birthday, footballer.Team.TeamName,
                footballer.Country.CountryName, footballer.UpdatedDate);

            return Ok(responses);
        }

        [HttpGet]
        public async Task<ActionResult<List<FootballersResponse>>> GetFootballers()
        {
            var footballers = await footballerService.GetAllFootballers();

            var responses = footballers.Select(f => new FootballersResponse(f.Id, f.FirstName, f.LastName,
                f.Gender, f.Birthday, f.Team.TeamName, f.Country.CountryName, f.UpdatedDate));

            return Ok(responses);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateFootballer([FromBody] FootballersRequest request)
        {
            var teamResult = Team.Create(Guid.NewGuid(), request.TeamName);
            var countryResult = Country.Create(Guid.NewGuid(), request.CountryName);

            if (teamResult.IsFailure) return BadRequest(teamResult.Error);
            if (countryResult.IsFailure) return BadRequest(countryResult.Error);

            var footballerResult = Footballer.Create(Guid.NewGuid(), request.FirstName, request.LastName,
                request.Gender, request.Birthday, teamResult.Value, countryResult.Value);

            if (footballerResult.IsFailure) return BadRequest(footballerResult.Error);

            return Ok(await footballerService.CreateFootballer(footballerResult.Value));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateFootballer(Guid id, [FromBody] FootballersRequest request)
        {
            var teamResult = Team.Create(Guid.NewGuid(), request.TeamName);
            var countryResult = Country.Create(Guid.NewGuid(), request.CountryName);

            if (teamResult.IsFailure) return BadRequest(teamResult.Error);
            if (countryResult.IsFailure) return BadRequest(countryResult.Error);

            var footballerResult = Footballer.Create(id, request.FirstName, request.LastName, request.Gender,
                request.Birthday, teamResult.Value, countryResult.Value);

            if (footballerResult.IsFailure) return BadRequest(footballerResult.Error);

            return Ok(await footballerService.UpdateFootballer(id, footballerResult.Value));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteFotobaler(Guid id)
        {
            await footballerService.DeleteFootballer(id);

            return Ok(id);
        }
    }


}
