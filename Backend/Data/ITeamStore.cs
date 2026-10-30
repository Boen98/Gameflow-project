public interface ITeamStore
{
    Task<List<Team>> Post(int aantalTeams, int aantalLeden, int gamesessieid);

    Task<Team?> GetTeam(int id);

    Task<List<Team>> GetTeams(int gameSessie);

    Task <bool> DeleteTeam(int teamid);

   Task<(bool Gelukt, string Bericht)> JoinTeam(string teamCode, int gameCode, string userId);
} 