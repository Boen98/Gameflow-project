public interface ITeamStore
{
    Task<List<Team>> Post(int aantalTeams, int aantalLeden);

    Task<Team?> GetTeam(int id);

    Task<List<Team>> GetTeams(int gameSessie);

    Task <bool> DeleteTeam(int teamid);
}