using System.Data.Common;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TeamSearcherAPI.Hubs;
using TeamSearcherAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

var configurationString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(configurationString);
});

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", policy =>
    {
       policy.WithOrigins("https://localhost:7169", "https://team-searcher.vercel.app")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials();
    }); 
});

builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCors("policy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var personGroup = app.MapGroup("/api/v1/person");
var teamGroup = app.MapGroup("/api/v1/team");

personGroup.MapPost("/", async (Person person, AppDbContext db) =>
{
    db.Person.Add(person);
    await db.SaveChangesAsync();

    return Results.Created($"api/v1/person/{person.Id}", person.Id);
});

personGroup.MapGet("/requests/{id}", async (int id, AppDbContext db) =>
{
    var persons = await db.TeamPersons
        .Where(tp => tp.TeamId == id && tp.Status == Status.NotAccepted)
        .Select(tp => tp.Person)
        .ToListAsync();

    return persons != null ? Results.Ok(persons) : Results.Ok();
});

personGroup.MapGet("/{id}", async (int Id, AppDbContext db) =>
{
    var person = await db.Person.FindAsync(Id);

    return person != null ? Results.Ok(person) : Results.NotFound("Person not found");
});

personGroup.MapPut("/request/{id}", async (int id, int teamId, AppDbContext db, IHubContext<PersonHub> hubContext) =>
{
    var accepted = await db.TeamPersons.AnyAsync(tp => tp.TeamId == teamId && tp.PersonId == id && tp.Status == Status.Accepted);

    if (accepted) return Results.Conflict("Request already accepted");

    var teamPerson = await db.TeamPersons.FindAsync(id, teamId);
    var team = await db.Team.FindAsync(teamId);

    teamPerson!.Status = Status.Accepted;
    team!.CurrentCount++;
    await db.SaveChangesAsync();

    await hubContext.Clients.All.SendAsync("GetTeams");

    return Results.Ok();
});

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

teamGroup.MapPost("/", async (Team team, AppDbContext db, IHubContext<PersonHub> hubContext) =>
{
    db.Team.Add(team);
    await db.SaveChangesAsync();

    await hubContext.Clients.All.SendAsync("GetTeams");

    return Results.Created($"api/v1/team/{team.Id}", team.Id);
});

teamGroup.MapPost("/request/{id}", async (int id, int personId, AppDbContext db, IHubContext<TeamHub> hubContext) =>
{
    var exists = await db.TeamPersons.AnyAsync(tp => tp.TeamId == id && tp.PersonId == personId);

    if (exists) return Results.Conflict("Request already exists");

    db.TeamPersons.Add(new TeamPerson{ TeamId = id, PersonId = personId, Status = Status.NotAccepted });
    await db.SaveChangesAsync();

    await hubContext.Clients.User(id.ToString()).SendAsync("GetRequests");

    return Results.Created();
});

teamGroup.MapGet("/", async (int personId, AppDbContext db) =>
{
    var teams = await db.Team
        .Where(t => !db.TeamPersons.Any(tp => 
            tp.TeamId == t.Id && 
            tp.PersonId == personId && 
            tp.Status == Status.Accepted))
        .ToListAsync();

    return teams.Where(t => t.CurrentCount < t.MaxCount);
});

teamGroup.MapGet("/{id}", async (int Id, AppDbContext db) =>
{
    var team = await db.Team.FindAsync(Id);

    return team != null ? Results.Ok(team) : Results.NotFound("Team not found");
});

app.UseHttpsRedirection();

app.MapHub<PersonHub>("/personHub");
app.MapHub<TeamHub>("/teamHub");

app.Run();