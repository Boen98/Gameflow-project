public static class PostTeamEndpoint
{
    public static RouteHandlerBuilder MapPostTeamEndpoint (this RouteGroupBuilder group)
    {
        return group.MapPost("", PostTeamHandler.Handle);
    }
}