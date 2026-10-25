public class EfTeamStore : ITeamStore

{
    private readonly GameFlowDb _db;

    public EfTeamStore(GameFlowDb db)
    {
        _db = db;
    }
    public async Task<Team> Post(string teamnaam, string? afbeelding, string? alt, int aantalLeden)
    {
        var team = new Team
        {
            Teamnaam = teamnaam,
            Afbeelding = afbeelding ?? "",
            Alt = alt ?? "",
            Leden = new List<TeamLid>()
        };

        for (int i = 1; i <= aantalLeden; i++)
        {
            team.Leden.Add(new TeamLid {Naam = $"naam {i}"});
        }
        
        _db.Add(team);
        await _db.SaveChangesAsync();
        return team; 
    }
}