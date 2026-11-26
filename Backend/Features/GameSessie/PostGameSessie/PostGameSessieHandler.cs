public static class PostGameSessieHandler
{
    public static async Task<IResult> Handle(
        PostGameSessieDTO request,
        IGameSessieStore store,
        ITeamStore teamStore)
    {
        // Nieuw: aantal rondes controleren
        if (request.AantalIngesteldeRondes < 1 ||
            request.AantalIngesteldeRondes > 4)
        {
            return Results.BadRequest(new
            {
                message = "Het aantal rondes moet tussen 1 en 4 liggen."
            });
        }

        // Nieuw: aantal startdatums controleren
        if (request.Startdatums == null ||
            request.Startdatums.Count != request.AantalIngesteldeRondes)
        {
            return Results.BadRequest(new
            {
                message = "Het aantal startdatums moet gelijk zijn aan het aantal rondes."
            });
        }

        // Nieuw: volgorde van startdatums controleren
        for (int i = 1; i < request.Startdatums.Count; i++)
        {
            if (request.Startdatums[i] < request.Startdatums[i - 1])
            {
                return Results.BadRequest(new
                {
                    message = "Een startdatum mag niet eerder zijn dan die van de vorige ronde."
                });
            }
        }

        var newGameSessie = await store.PostGameSessie(
            request.StartSituatieTijdInMinuten,
            request.AantalIngesteldeRondes,
            request.TijdVoorRondesInMinuten,
            request.IngesteldeAantalTeams,
            request.IngesteldeAantalSpelersPerTeam,
            request.Startdatums); // Nieuw

        await teamStore.Post(
            newGameSessie.IngesteldeAantalTeams,
            newGameSessie.IngesteldeAantalSpelersPerTeam,
            newGameSessie.GameSessieId);

        var response = new PostGameSessieResponse(
            newGameSessie.GameSessieId,
            newGameSessie.StartSituatieTijd,
            newGameSessie.AantalIngesteldeRondes,
            newGameSessie.TijdVoorRondesInMinuten,
            newGameSessie.IngesteldeAantalTeams,
            newGameSessie.IngesteldeAantalSpelersPerTeam);

        return Results.Ok(response);
    }
}