using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidation();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITeamStore, EfTeamStore>();

var connectionString = builder.Configuration.GetConnectionString("Database");
    builder.Services.AddSqlServer<GameFlowDb>(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapTeamEndpoints();
app.Run();
