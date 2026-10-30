public class GameSessie
{
     public int GameSessieId { get; set; }
    public string GameStatus { get; set; } = "";
    public List<Team>? GekopeldeTeams { get; set; }
    
    // wat ingevult gaat worden door de beheerder
    public int StartSituatieTijd { get; set; }
    public int AantalIngesteldeRondes { get; set; }
    public int TijdVoorRondesInMinuten {get; set;}
    public int IngesteldeAantalTeams { get; set; }
    public int IngesteldeAantalSpelersPerTeam { get; set; }

} 
    