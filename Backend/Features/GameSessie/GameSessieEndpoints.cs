public static class GameSessieEndpoints
{
    public static RouteGroupBuilder MapGameSessiesEndpoints (this IEndpointRouteBuilder app)
    {
        var gameSessies = app.MapGroup("/gamesessies");

        gameSessies.MapPostGameSessieEndpoint();
        return gameSessies;
    }
}