public static class PostTeamHandler
{
    public static async Task<IResult> Handle (PostTeamDTO request, ITeamStore store)
    {
        var newTeam = await store.Post(request.AantalTeams, request.AantalLedenPerTeam);

        var response = newTeam.Select(team => new PostTeamResponse(
            team.Id,
            team.Teamnaam,
            team.Afbeelding,
            team.Alt,
            team.Leden.Select(l => new PostTeamlidResponse(l.Id, l.Naam)).ToList()
        )).ToList();

        return Results.Created((string?)null,response);
    }
}