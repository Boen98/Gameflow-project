using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public static class JoinTeamHandler{
    public static async Task<IResult> Handle(JoinTeamDTO request, ClaimsPrincipal user, ITeamStore teamStore,  UserManager<User> userManager, SignInManager<User> signInManager)
    {
        var gebruiker = await userManager.GetUserAsync(user);

        if (gebruiker is null)
        { 
         return Results.Unauthorized();
        }  

        var resultaat = await teamStore.JoinTeam(request.Teamcode,request.Gamecode, gebruiker.Id);

        if (!resultaat.Gelukt)
        {
            return Results.BadRequest(resultaat.Bericht);
        }

        if (!await userManager.IsInRoleAsync(gebruiker,"Speler"))
        {
            var rolResultaat = await userManager.AddToRoleAsync(gebruiker,"Speler");

             if (!rolResultaat.Succeeded)
            {
                return Results.Problem("Je bent aan het team gekopeld, maar je kon de speler rol neit krijgen");
            }
        }

        await signInManager.RefreshSignInAsync(gebruiker);
        
        return Results.Ok(resultaat.Bericht);
    }

}