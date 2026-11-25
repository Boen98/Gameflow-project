public class Ronde
{
    public int Id { get; set; }

    public int GameSessieId { get; set; }

    public int Nummer { get; set; }

    public DateTime GeplandeStartMoment { get; set; }

    public bool IsAfgesloten { get; set; } = false;
}