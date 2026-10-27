using System.ComponentModel.DataAnnotations;

public class PostTeamDTO
{
    [Required]
    [Range(1, int.MaxValue)]
    public int AantalTeams { get; set; } 
    [Required]
    [Range(1, int.MaxValue)]
    public int AantalLedenPerTeam { get; set; } 
}
