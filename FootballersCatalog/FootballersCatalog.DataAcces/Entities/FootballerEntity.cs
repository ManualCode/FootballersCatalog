using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.DataAcces.Entities
{
    public class FootballerEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateOnly Birthday { get; set; }

        public Guid TeamId { get; set; }

        public TeamEntity? Team { get; set; }

        public Guid CountryId { get; set; }

        public CountryEntity? Country { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
