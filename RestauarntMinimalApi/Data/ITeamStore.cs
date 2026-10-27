public interface ITeamStore
{
    Task<List<Team>> Post(int aantalTeams, int aantalLeden);

    Task<Team?> GetTeam(int id);
}