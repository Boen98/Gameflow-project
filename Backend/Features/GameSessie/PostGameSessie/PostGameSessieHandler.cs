public static class PostGameSessieHandler
{
   public static async Task<IResult> Handle(PostGameSessieDTO request, IGameSessieStore store, ITeamStore teamStore)
    {
        var newGameSessie = await store.PostGameSessie(request.StartSituatieTijdInMinuten, request.AantalIngesteldeRondes, request.TijdVoorRondesInMinuten, request.IngesteldeAantalTeams, request.IngesteldeAantalSpelersPerTeam);

        await teamStore.Post(newGameSessie.IngesteldeAantalTeams, newGameSessie.IngesteldeAantalSpelersPerTeam, newGameSessie.GameSessieId);

        var response = new PostGameSessieResponse (newGameSessie.GameSessieId,newGameSessie.StartSituatieTijd,newGameSessie.AantalIngesteldeRondes,newGameSessie.TijdVoorRondesInMinuten,newGameSessie.IngesteldeAantalTeams, newGameSessie.IngesteldeAantalSpelersPerTeam);
        return Results.Ok(response);
    } 
}