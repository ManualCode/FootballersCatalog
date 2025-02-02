using FootballersCatalog.DataAcces.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballersCatalog.DataAcces
{
    public class FootballersCatalogDbContext: DbContext
    {
        public FootballersCatalogDbContext(DbContextOptions<FootballersCatalogDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryEntity>().HasData(
            new CountryEntity { Id = Guid.NewGuid(), CountryName = "Россия" },
                 new CountryEntity { Id = Guid.NewGuid(), CountryName = "США" },
                 new CountryEntity { Id = Guid.NewGuid(), CountryName = "Италия" }
            );
        }

        public DbSet<FootballerEntity> Footballers { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
    }
}
