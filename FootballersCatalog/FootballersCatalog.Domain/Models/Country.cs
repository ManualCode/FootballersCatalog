using CSharpFunctionalExtensions;

namespace FootballersCatalog.Domain.Models
{
    public class Country
    {
        protected static IReadOnlyCollection<string> ALLOWED_COUNTRIES = new List<string> { "Россия", "CША", "Италия" }.AsReadOnly();
        private Country(Guid id, string name)
        {
            Id = id;
            CountryName = name;
        }

        public Guid Id { get; }

        public string? CountryName { get; }

        public static Result<Country> Create(Guid id, string countryName)
        {
            if (!new List<string> { "Россия", "США", "Италия" }.Contains(countryName))
                return Result.Failure<Country>($"'{nameof(countryName)}' должен быть выбран из списка");

            var country = new Country(id, countryName);

            return Result.Success(country);
        }
    }
}
