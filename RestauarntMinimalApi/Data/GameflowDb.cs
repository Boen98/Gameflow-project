using Microsoft.EntityFrameworkCore;

public class GameFlowDb : DbContext
{
    public GameFlowDb(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Team> teams {get; set;} = null!;
}