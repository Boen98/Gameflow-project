 public static class GetTeamEndpoint
{
    public static RouteHandlerBuilder  MapGetTeamEndpoint(this RouteGroupBuilder group)
    {
        return group.MapGet("/{id:int}", GetTeamHandler.Handle);
    }
}