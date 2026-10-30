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
                Naam = $"naam {j}",
                UserID = null
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

     public async  Task<(bool Gelukt, string Bericht)> JoinTeam(string teamCode, int gameCode, string userId)
    {
        var team = await _db.teams.Include(t => t.Leden).FirstOrDefaultAsync(t => t.Teamcode == teamCode && t.GameSessieId == gameCode);

        if (team is null)
        {
         return (false, "Team is niet gevonden voor deze gamesessie");
        }
            var alLid =  team.Leden.Any(l => l.UserID == userId);

        if (alLid)
        {
            return (false, "Je bent al een Lid van deze team");
        }
        var vrijLid = team.Leden.OrderBy(l => l.Id).FirstOrDefault();

         if (vrijLid.UserID is not null)
        {
        return (false, "Dit team is al gekoppeld aan een speler.");
      }
        vrijLid.UserID = userId;
        await _db.SaveChangesAsync();

       return (true, "Je bent aan de team toegevoegd.");
    }
}

