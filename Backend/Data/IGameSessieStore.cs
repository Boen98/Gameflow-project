public interface IGameSessieStore
{
    Task<GameSessie> PostGameSessie(int startSituatieTijd,int aantalIngesteldeRondes,int tijdVoorRondesInMinuten, int ingesteldeAantalTeams, int ingesteldeAantalSpelersPerTeam);

}