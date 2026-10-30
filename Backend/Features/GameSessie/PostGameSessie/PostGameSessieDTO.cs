using System.ComponentModel.DataAnnotations;
public class PostGameSessieDTO
{
    [Range(0,60)]
   public int StartSituatieTijdInMinuten { get; set; }

   [Required]
   [Range(1,4)]
    public int AantalIngesteldeRondes { get; set; }

    [Range(0,60)]
    public int TijdVoorRondesInMinuten {get; set;}

    [Required]
    [Range(1, int.MaxValue)]
    public int IngesteldeAantalTeams { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int IngesteldeAantalSpelersPerTeam { get; set; }
}