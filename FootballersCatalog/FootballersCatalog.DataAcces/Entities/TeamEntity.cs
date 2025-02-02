using FootballersCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.DataAcces.Entities
{
    public class TeamEntity
    {
        public Guid Id { get; set; }

        public string? TeamName { get; set; }

        public List<FootballerEntity> Footballers { get; set; } = new();
    }
}
