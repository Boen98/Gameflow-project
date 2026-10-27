public static class TeamEndpoints
{
    public static RouteGroupBuilder MapTeamEndpoints(this IEndpointRouteBuilder app)
    {
        var teams = app.MapGroup("/teams");

        teams.MapPostTeamEndpoint();
        teams.MapGetTeamEndpoint();
        
        return teams;
    }
}