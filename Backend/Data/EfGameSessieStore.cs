using System.Data.Common;

public class EfGameSessieStore : IGameSessieStore

{
    private readonly GameFlowDb _db;

    public EfGameSessieStore(GameFlowDb db){
        _db = db;
    }
public async Task<GameSessie> PostGameSessie(int startSituatieTijd,int aantalIngesteldeRondes,int tijdVoorRondesInMinuten, int ingesteldeAantalTeams, int ingesteldeAantalSpelersPerTeam)
    {
        var newGameSessie = new GameSessie
        {
            StartSituatieTijd = startSituatieTijd,
            AantalIngesteldeRondes = aantalIngesteldeRondes,
            TijdVoorRondesInMinuten = tijdVoorRondesInMinuten,
            IngesteldeAantalTeams = ingesteldeAantalTeams,
            IngesteldeAantalSpelersPerTeam = ingesteldeAantalSpelersPerTeam
        };

        _db.gameSessies.Add(newGameSessie);
        await _db.SaveChangesAsync();
        return newGameSessie;
    }
}