using System.ComponentModel.DataAnnotations;

public class JoinTeamDTO
{
    [Required]
    public string Teamcode { get; set; } = "";

    public int Gamecode {get; set;}
}