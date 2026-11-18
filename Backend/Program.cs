using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidation();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITeamStore, EfTeamStore>();
builder.Services.AddScoped<IGameSessieStore, EfGameSessieStore>();

var connectionString =
    builder.Configuration.GetConnectionString("Database");

builder.Services.AddSqlServer<GameFlowDb>(connectionString);

builder.Services.AddAuthentication();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(
        "canManageGameSessions",
        policy => policy.RequireRole("Editor", "Admin"))
    .AddPolicy(
        "IsPlayer",
        policy => policy.RequireRole("Speler"));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GameFlowDb>();

builder.Services.AddCors(options =>
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/auth").MapIdentityApi<User>();

app.MapTeamEndpoints();
app.MapGameSessiesEndpoints();

await DbSeeder.SeedAsync(app.Services);

app.MapGet(
    "/auth/me",
    (System.Security.Claims.ClaimsPrincipal gebruiker) =>
        Results.Ok(new
        {
            isAdmin = gebruiker.IsInRole("Admin"),
            isSpeler = gebruiker.IsInRole("Speler")
        }))
    .RequireAuthorization();

app.Run();