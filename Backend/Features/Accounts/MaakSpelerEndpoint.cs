using Microsoft.AspNetCore.Identity;

public static class MaakSpelerEndpoint
{
    public static void MapMaakSpelerEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/admin/spelers", Handle)
            .RequireAuthorization(
                policy => policy.RequireRole("Admin"));
    }

    private static async Task<IResult> Handle(
        MaakSpelerDTO request,
        UserManager<User> userManager,
        GameFlowDb db)
    {
        var email = request.Email.Trim();

        var gebruiker = new User
        {
            UserName = email,
            Email = email
        };

        await using var transactie =
            await db.Database.BeginTransactionAsync();

        var aanmaken = await userManager.CreateAsync(
            gebruiker,
            request.Password);

        if (!aanmaken.Succeeded)
        {
            return Results.ValidationProblem(
                aanmaken.Errors
                    .GroupBy(fout => fout.Code)
                    .ToDictionary(
                        groep => groep.Key,
                        groep => groep
                            .Select(fout => fout.Description)
                            .ToArray()));
        }

        var rol = await userManager.AddToRoleAsync(
            gebruiker,
            "Speler");

        if (!rol.Succeeded)
        {
            return Results.Problem(
                "De spelersrol kon niet worden opgeslagen. " +
                "Het account is niet aangemaakt.");
        }

        await transactie.CommitAsync();

        return Results.Ok(new
        {
            bericht =
                "Spelersaccount aangemaakt. " +
                "De speler kan nu inloggen."
        });
    }
}