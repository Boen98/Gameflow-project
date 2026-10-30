public static class GetTeamHandler
{
    public static async Task<IResult> Handle(int id, ITeamStore store)
    {
        var team = await store.GetTeam(id);

        if (team is null)
        {
         return Results.NotFound();
        }

        return Results.Ok(team);
    }
}