using CSharpFunctionalExtensions;

namespace FootballersCatalog.Domain.Models
{
    public class Team
    {
        private Team(Guid id, string name, List<Footballer> footballers)
        {
            Id = id;
            TeamName = name;
            Footballers = footballers;
        }

        public Guid Id { get; }

        public string? TeamName { get; }

        public List<Footballer> Footballers { get; } = [];

        public static Result<Team> Create(Guid id, string teamName, List<Footballer> footballers = null)
        {
            if (string.IsNullOrEmpty(teamName) || teamName.Length > Footballer.MAX_FIELD_LENGTH)
                return Result.Failure<Team>($"{nameof(teamName)} не может быть пустым");

            footballers ??= [];
            var team = new Team(id, teamName, footballers);

            return Result.Success(team);
        }
    }

}
