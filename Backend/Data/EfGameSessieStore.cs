using System.Data.Common;

public class EfGameSessieStore : IGameSessieStore

{
    private readonly GameFlowDb _db;

    public EfGameSessieStore(GameFlowDb db){
        _db = db;
    }
public async Task<GameSessie> PostGameSessie(int startSituatieTijd,int aantalIngesteldeRondes,int tijdVoorRondesInMinuten, int ingesteldeAantalTeams, int ingesteldeAantalSpelersPerTeam, List<DateTimeOffset> startdatums)
    {
        var newGameSessie = new GameSessie
        {
            StartSituatieTijd = startSituatieTijd,
            AantalIngesteldeRondes = aantalIngesteldeRondes,
            TijdVoorRondesInMinuten = tijdVoorRondesInMinuten,
            IngesteldeAantalTeams = ingesteldeAantalTeams,
            IngesteldeAantalSpelersPerTeam = ingesteldeAantalSpelersPerTeam
        };

        for (int i = 0; i < aantalIngesteldeRondes; i++)
{
        newGameSessie.Rondes.Add(new Ronde
    {
            Nummer = i + 1,
            GeplandeStartMoment = startdatums[i].UtcDateTime,
            IsAfgesloten = false
    });
}

        _db.gameSessies.Add(newGameSessie);
        await _db.SaveChangesAsync();
        return newGameSessie;
    }
}