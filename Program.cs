using Microsoft.EntityFrameworkCore;
using CodingBasics.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorks2022Context>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("localServer"))
);

builder.Services.AddControllers();


var app = builder.Build();

app.MapControllers();

app.Run();

