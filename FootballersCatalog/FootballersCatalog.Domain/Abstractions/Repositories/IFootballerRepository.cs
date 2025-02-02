using FootballersCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.Domain.Abstractions.Repositories
{
    public interface IFootballerRepository
    {
        Task<List<Footballer>> GetAsync();
        Task<Footballer> GetById(Guid Id);
        Task<Guid> CreateAsync(Footballer footballer, Guid teamId, Guid countryId);
        Task<Guid> UpdateAsync(Guid id, string firstName, string lastName, string gender, DateOnly birthday, Guid teamId, Guid countryId);
        Task DeleteAsync(Guid id);
    }
}
