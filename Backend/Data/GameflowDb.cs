using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class GameFlowDb : IdentityDbContext<User>
{
    public GameFlowDb(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<GameSessie> gameSessies {get; set;}
    public DbSet<Team> teams {get; set;} = null!;
    public DbSet<Ronde> rondes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //veiligheid check dat in de databse een team wordt altijd aangemaakt dus teamcode nooit null, hasmaxlength zodat hij niet meer opslag neemt
         modelBuilder.Entity<Team>().Property(team => team.Teamcode).IsRequired().HasMaxLength(4);

        //zorgen dat teamcode uniek is binnen gamesessies niet hetzelfde is, ipv alle game sessies
         modelBuilder.Entity<Team>().HasIndex(team => new { team.GameSessieId,team.Teamcode}).IsUnique();
         
         //zorgen dat rondes gekoppeld zijn aan een gamesessie, en dat de foreign key in de ronde tabel wordt gezet
         modelBuilder.Entity<GameSessie>() .HasMany(g => g.Rondes) .WithOne() .HasForeignKey(r => r.GameSessieId);

         //zorgen dat rondes uniek zijn binnen een gamesessie, dus niet hetzelfde ronde nummer in dezelfde gamesessie
         modelBuilder.Entity<Ronde>() .HasIndex(r => new { r.GameSessieId, r.Nummer }) .IsUnique();


    }
}