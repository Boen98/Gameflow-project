public class Team {
 public int Id { get; set; }
public string Teamnaam { get; set; } = "";

public string Afbeelding { get; set; } = "";

public string Alt { get; set; } = "";

public List<TeamLid> Leden { get; set;}
}