using Microsoft.EntityFrameworkCore;
using CodingBasics.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorks2022Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("localServer"))
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

