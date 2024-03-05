using CodingBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// implement the dependency injection
builder.Services.AddDbContext<AdventureWorksDbContext>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// This is the endpoint that will return the list of employees
app.MapGet("/person", async(AdventureWorksDbContext dbContext) =>
{
    var employees = await dbContext.VEmployees
        .OrderBy(e => e.BusinessEntityId)
        .ToListAsync();

    return employees;
});

app.MapGet("/person/GetByName", ([FromQuery] string name, AdventureWorksDbContext dbContext) =>
{
    try
    {
        var persons = dbContext.VEmployees
            .AsEnumerable()
            .Where(e => $"{e.FirstName} {e.MiddleName} {e.LastName}".Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Results.Json(persons);
    }
    catch (Exception ex)
    {
        return Results.Json($"Error: {ex.Message}");
    }
});

app.MapGet("/persons/byPersonType", (AdventureWorksDbContext dbContext, [FromQuery] string personType) =>
{
    try
    {
        var results = dbContext.VEmployees
            .FromSqlRaw($"SELECT * FROM HumanResources.vEmployee WHERE BusinessEntityID IN (SELECT BusinessEntityID FROM Person.Person WHERE PersonType = {personType})")
            .ToList();

        return Results.Json(results);
    }
    catch (Exception ex)
    {
        return Results.Json($"Error: {ex.Message}");
    }
});








app.Run();
