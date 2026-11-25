public static class JoinTeamEndpoint
{
    public static RouteHandlerBuilder MapJoinTeamEndpoint(this RouteGroupBuilder group)
    {
        return group.MapPost("/join", JoinTeamHandler.Handle).RequireAuthorization();
    }
}