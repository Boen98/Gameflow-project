public static class PostTeamHandler
{
    public static async Task<IResult> Handle (PostTeamDTO request, ITeamStore store)
    {
        var newTeam = await store.Post(request.Teamnaam, request.Afbeelding, request.Alt, request.AantalLedenPerTeam);

        var response = new PostTeamResponse(
            newTeam.Id,
            newTeam.Teamnaam,
            newTeam.Afbeelding,
            newTeam.Alt,
            newTeam.Leden.Select(l => new PostTeamlidResponse(l.Id, l.Naam)).ToList());

        return Results.Created($"/teams/{newTeam.Id}",response);
    }
}