using System.ComponentModel.DataAnnotations;

public class PostTeamDTO
{
    [Required]
    public string Teamnaam { get; set; } = "";

    public string? Afbeelding { get; set; }

    public string? Alt { get; set; }   

    [Range(1, int.MaxValue)]
    public int AantalLedenPerTeam { get; set; } 
}
