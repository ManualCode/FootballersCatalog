namespace FootballersCatalog.API.Contracts
{
    public record FootballersResponse(Guid Id, string FirstName, string LastName, string Gender,
        DateOnly Birthday, string TeamName, string CountryName, DateTime UpdatedDate);
}
