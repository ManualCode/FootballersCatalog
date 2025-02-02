using FootballersCatalog.Domain.Abstractions.Repositories;
using FootballersCatalog.Domain.Abstractions.Services;
using FootballersCatalog.Domain.Models;

namespace FootballersCatalog.Application.Services
{
    public class FootballerService : IFootballerService
    {
        private readonly IFootballerRepository footballerRepository;
        private readonly ICountryRepository countryRepository;
        private readonly ITeamRepository teamRepository;
        private readonly ITeamService teamService;

        public FootballerService(IFootballerRepository footballerRepository, ICountryRepository countryRepository,
            ITeamRepository teamRepository, ITeamService teamService)
        {
            this.footballerRepository = footballerRepository;
            this.countryRepository = countryRepository;
            this.teamRepository = teamRepository;
            this.teamService = teamService;
        }

        public async Task<Footballer> GetFootballer(Guid id)
            => await footballerRepository.GetById(id);

        public async Task<List<Footballer>> GetAllFootballers()
            => await footballerRepository.GetAsync();

        public async Task<Guid> CreateFootballer(Footballer footballer)
            => await footballerRepository.CreateAsync(footballer,
                (await teamRepository.FindOrCreateAsync(footballer.Team)).Id,
                (await countryRepository.FindOrCreateAsync(footballer.Country)).Id);

        public async Task<Guid> UpdateFootballer(Guid id, Footballer footballer)
        {
            var oldTeamId = (await teamRepository.GetTeamByNameAsync(
                (await footballerRepository.GetById(id)).Team.TeamName)).Id;

            var footballerGuid = await footballerRepository.UpdateAsync(id, footballer.FirstName,
                footballer.LastName, footballer.Gender, footballer.Birthday,
                (await teamRepository.FindOrCreateAsync(footballer.Team)).Id,
                (await countryRepository.FindOrCreateAsync(footballer.Country)).Id);

            await teamService.DeleteEmptyTeam(oldTeamId);

            return footballerGuid;
        }

        public async Task DeleteFootballer(Guid id)
        {
            var teamId = (await footballerRepository.GetById(id)).Team.Id;

            await footballerRepository.DeleteAsync(id);
            await teamService.DeleteEmptyTeam(teamId);
        }
    }
}
