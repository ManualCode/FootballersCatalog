using FootballersCatalog.Domain.Models;

namespace FootballersCatalog.Domain.Abstractions.Services
{
    public interface IFootballerService
    {
        Task<Footballer> GetFootballer(Guid id);
        Task<Guid> CreateFootballer(Footballer footballer);
        Task<List<Footballer>> GetAllFootballers();
        Task<Guid> UpdateFootballer(Guid Id, Footballer footballer);
        Task DeleteFootballer(Guid id);

    }
}