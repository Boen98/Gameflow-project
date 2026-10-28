public interface ITeamStore
{
    Task<List<Team>> Post(int aantalTeams, int aantalLeden, int gamesessieid);

    Task<Team?> GetTeam(int id);

    Task<List<Team>> GetTeams(int gameSessie);

    Task <bool> DeleteTeam(int teamid);
}