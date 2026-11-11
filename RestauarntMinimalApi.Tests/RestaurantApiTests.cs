using Microsoft.AspNetCore.Http.HttpResults;
using Xunit;

public class unitTests

{
            private class FakeTeamStore : ITeamStore
    {
        public List<Team> TeamsOmTerugTegeven { get; set; } = new List<Team>();

        public Task<List<Team>> Post(int aantalTeams, int aantalLeden, int gamesessieid) => Task.FromResult(new List<Team>());

        public Task<Team?> GetTeam(int id) => Task.FromResult<Team?>(null); 
        public Task<List<Team>> GetTeams(int id) => Task.FromResult(TeamsOmTerugTegeven);  

        public Task<bool> DeleteTeam(int id) => Task.FromResult(false);

        public Task<(bool Gelukt, string Bericht)> JoinTeam(string teamCode, int gameCode, string userId) => Task.FromResult((false, "niet in de test"));
    }
    
        [Fact]
    public void GenereerRandomCode_Geeft4tekensTerug()
    {
        //arrange niks want de metode is static

        //act 
        var code = EfTeamStore.GenereerRandomCode();

        //assert 
        Assert.Equal(4,code.Length);
    }

    [Fact]
     public async Task GeefEenLegeLijstTerugAlsErGeenTeamsZijn()
    {
        var fakeStore = new FakeTeamStore{
        TeamsOmTerugTegeven = new List<Team>()};

        //act 
        var teams = await  MapGetTeamsHandler.Handle(5,fakeStore);
        Console.WriteLine(teams);

        //assert
         Assert.IsType<Ok<List<Team>>>(teams);

    }
}