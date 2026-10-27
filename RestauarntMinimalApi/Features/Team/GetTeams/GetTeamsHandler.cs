public static class MapGetTeamsHandler
{
    public static async Task<IResult> Handle(int id ,ITeamStore store)
    {
        var teams = await store.GetTeams(id);

        return Results.Ok(teams);
    }
}