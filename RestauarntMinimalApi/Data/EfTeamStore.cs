using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

public class EfTeamStore : ITeamStore

{
    private readonly GameFlowDb _db;

    public EfTeamStore(GameFlowDb db)
    {
        _db = db;
    }
    public async Task<List<Team>> Post(int aantalTeams, int aantalLeden, int gamesessieid)
    {
{
    var teams = new List<Team>();

    for (int i = 1; i <= aantalTeams; i++)
    {
        string randomTeamCode;
        bool randomTeamCodeBestaat;

        do
        {
            randomTeamCode = GenereerRandomCode();

            randomTeamCodeBestaat =
                teams.Any(team => team.Teamcode == randomTeamCode);
        }
       while (randomTeamCodeBestaat);

        var team = new Team
        {
            Teamnaam = $"Restaurant {i}",
            Teamcode = randomTeamCode,
            Afbeelding = "",
            Alt = "",
            Leden = new List<TeamLid>(),
            GameSessieId = gamesessieid
        };

        teams.Add(team);
    }

    foreach (var team in teams)
    {
        for (int j = 1; j <= aantalLeden; j++)
        {
            team.Leden.Add(new TeamLid
            {
                Naam = $"naam {j}"
            });
        }
    }

    _db.AddRange(teams);
    await _db.SaveChangesAsync();

    return teams;
    }
}
    public async Task<Team?> GetTeam(int id)
    {

          return await _db.teams.Include(team => team.Leden).FirstOrDefaultAsync(team => team.Id == id);
    }

    public async Task<List<Team>> GetTeams(int id)
    {
        return await _db.teams.Where(team => team.GameSessieId == id).Include(team => team.Leden).ToListAsync();
    }
    
    public async Task <bool> DeleteTeam(int teamid)
    {
        var team = await _db.teams.FirstOrDefaultAsync(team => team.Id == teamid);

        if (team is null)
        {
            return false;
        }
            
        _db.Remove(team);
        await _db.SaveChangesAsync();
        return true;

    }
  
    public static string GenereerRandomCode()
    {
        const string mogelijkheden = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var code =  new char [4];

        for (int i = 0; i<code.Length; i++)
        {
                int index = RandomNumberGenerator.GetInt32(mogelijkheden.Length);
                code[i] =  mogelijkheden[index];
        }
        return new string(code);
    }
}

