using System.Collections;
using Microsoft.EntityFrameworkCore;

public class EfTeamStore : ITeamStore

{
    private readonly GameFlowDb _db;

    public EfTeamStore(GameFlowDb db)
    {
        _db = db;
    }
    public async Task<List<Team>> Post(int aantalTeams, int aantalLeden)
    {
        var teams = new List<Team>();

         for (int i = 1; i <= aantalTeams; i++){

         var team = new Team        {
            Teamnaam = $"Restaurant {i}",
            Afbeelding = "",
            Alt =  "",
            Leden = new List<TeamLid>()
        };
        teams.Add(team);
         }

             foreach (var team in teams){

                for (int j = 1; j <= aantalLeden; j++)
        {
            team.Leden.Add(new TeamLid {Naam = $"naam {j}"});
        }
     }
        

        
        _db.AddRange(teams);
        await _db.SaveChangesAsync();
        return teams; 
    }
    public async Task<Team?> GetTeam(int id)
    {

          return await _db.teams.Include(team => team.Leden).FirstOrDefaultAsync(team => team.Id == id);
    }
}