using Microsoft.EntityFrameworkCore;

public class GameFlowDb : DbContext
{
    public GameFlowDb(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<GameSessie> gameSessies {get; set;}
    public DbSet<Team> teams {get; set;} = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //veiligheid check dat in de databse een team wordt altijd aangemaakt dus teamcode nooit null, hasmaxlength zodat hij niet meer opslag neemt
        modelBuilder.Entity<Team>().Property(team => team.Teamcode).IsRequired().HasMaxLength(4);

        //zorgen dat teamcode uniek is binnen gamesessies niet hetzelfde is, ipv alle game sessies
         modelBuilder.Entity<Team>().HasIndex(team => new { team.GameSessieId,team.Teamcode}).IsUnique();
    }
}