using CSharpFunctionalExtensions;

namespace FootballersCatalog.Domain.Models
{
    public class Footballer
    {
        public const int MAX_FIELD_LENGTH = 25;
        protected static IReadOnlyCollection<string> ALLOWED_GENDERS = new List<string> { "Мужчина", "Женщина" }.AsReadOnly();
        private Footballer(Guid id, string firstName, string lastName,
            string gender, DateOnly birthday, Team team, Country country)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Birthday = birthday;
            Team = team;
            Country = country;
            UpdatedDate = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Gender { get; }

        public DateOnly Birthday { get; }

        public Team Team { get; }

        public Country Country { get; }

        public DateTime UpdatedDate { get; }

        public static Result<Footballer> Create(Guid id, string firstName, string lastName,
            string gender, DateOnly birthday, Team team, Country country)
        {
            if (string.IsNullOrEmpty(firstName) || firstName.Length > MAX_FIELD_LENGTH)
                return Result.Failure<Footballer>($"{nameof(firstName)} не может быть пустым");

            if (string.IsNullOrEmpty(lastName) || lastName.Length > MAX_FIELD_LENGTH)
                return Result.Failure<Footballer>($"{nameof(lastName)} не может быть пустым");

            if (string.IsNullOrEmpty(gender) || !new List<string> { "Мужчина", "Женщина" }.Contains(gender))
                return Result.Failure<Footballer>($"{nameof(gender)} не может быть пустым");

            var footballer = new Footballer(id, firstName, lastName, gender, birthday, team, country);

            return Result.Success(footballer);
        }
    }
}
