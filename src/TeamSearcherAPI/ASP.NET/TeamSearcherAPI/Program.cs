using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TeamSearcherAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

var configurationString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(configurationString);
});

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", policy =>
    {
       policy.WithOrigins("https://localhost:7169")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials();
    }); 
});

var app = builder.Build();

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

personGroup.MapGet("/", async (AppDbContext db) =>
{
    return await db.Person.ToListAsync();
});

personGroup.MapGet("/{id}", async (int Id, AppDbContext db) =>
{
    var person = await db.Person.FindAsync(Id);

    return person != null ? Results.Ok(person) : Results.NotFound("Person not found");
});

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

teamGroup.MapPost("/", async (Team team, AppDbContext db) =>
{
    db.Team.Add(team);
    await db.SaveChangesAsync();

    return Results.Created($"api/v1/team/{team.Id}", team.Id);
});

teamGroup.MapPost("/request/{id}", async (int id, int personId, AppDbContext db) =>
{
    var exists = await db.TeamPersons.AnyAsync(tp => tp.TeamId == id && tp.PersonId == personId);

    if (exists) return Results.Conflict("Request already exists");

    db.TeamPersons.Add(new TeamPerson{ TeamId = id, PersonId = personId, Status = Status.NotAccepted });
    await db.SaveChangesAsync();

    return Results.Created();
});

teamGroup.MapGet("/", async (AppDbContext db) =>
{
    return await db.Team.ToListAsync();
});

teamGroup.MapGet("/{id}", async (int Id, AppDbContext db) =>
{
    var team = await db.Team.FindAsync(Id);

    return team != null ? Results.Ok(team) : Results.NotFound("Team not found");
});

app.UseHttpsRedirection();

app.Run();