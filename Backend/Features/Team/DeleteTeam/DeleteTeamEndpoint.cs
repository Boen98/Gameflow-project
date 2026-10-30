public static class DeleteTeamEndpoint
{
    public static RouteHandlerBuilder MapDeleteTeamEndpoint(this RouteGroupBuilder group)
    {
        return group.MapDelete("", DeleteTeamHandler.Handle).RequireAuthorization("canManageGameSessions");
    }
}