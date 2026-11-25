public static class TeamEndpoints
{
    public static RouteGroupBuilder MapTeamEndpoints(this IEndpointRouteBuilder app)
    {
        var teams = app.MapGroup("/teams");
        
        teams.MapGetTeamEndpoint();
        teams.MapGetTeamsEndpoint();
        teams.MapDeleteTeamEndpoint();
        teams.MapJoinTeamEndpoint();
        
        return teams;
    }
}