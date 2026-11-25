using Microsoft.AspNetCore.Identity;

public class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // rol aanmaken als hij nog niet bestaat
        string [] rollen = {"Admin","Speler"};
        const string  adminRole = "Admin";

        foreach( string rol in rollen){
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }

        var admin = await userManager.FindByEmailAsync("opdrachtgever@gameflow.test");

        if (admin is null )
        {
            admin = new User{
                UserName = "opdrachtgever@gameflow.test", 
                Email = "opdrachtgever@gameflow.test",
                 EmailConfirmed = true
                 };

            await userManager.CreateAsync(admin, "Admin123!");
        }
        if (!await userManager.IsInRoleAsync(admin, adminRole))
        {
            await userManager.AddToRoleAsync(admin, adminRole);
        }
    }
}