
public static class DeleteTeamHandler
{
    public static async Task<IResult>Handle (int teamid, ITeamStore store)
    {
        var team = await store.DeleteTeam(teamid);

        if (!team)
        {
            return Results.NotFound();
        }
            return Results.NoContent();
    }
}