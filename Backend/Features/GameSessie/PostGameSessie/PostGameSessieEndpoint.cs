public static class PostGameSessieEndpoint
{
    public static RouteHandlerBuilder MapPostGameSessieEndpoint (this RouteGroupBuilder group)
    {
        return group.MapPost("", PostGameSessieHandler.Handle).RequireAuthorization("canManageGameSessions");
    }
}