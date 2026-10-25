public interface ITeamStore
{
    Task<Team> Post(string teamnaam, string? afbeelding, string? alt, int aantalLeden);
}