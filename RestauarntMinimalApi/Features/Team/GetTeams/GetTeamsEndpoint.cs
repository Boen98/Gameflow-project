public static class GetTeamsEndpoint
{
    public static RouteHandlerBuilder MapGetTeamsEndpoint (this RouteGroupBuilder group)
    {
        return group.MapGet("", MapGetTeamsHandler.Handle);
    }
}