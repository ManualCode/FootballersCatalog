namespace FootballersCatalog.API.Contracts
{
    public record TeamsResponse(Guid Id, string Name,
        List<FootballersResponse> footballers);
}
