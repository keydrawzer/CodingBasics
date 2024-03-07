
using CodingBasics.Models;
using CodingBasics.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddSingleton<DataClient>()
    .AddSingleton<PersonService>()
    .AddSingleton<ProductService>()
    .AddSingleton<SalesService>()
    .AddSingleton<AdventureWorks2022Context>()
    .AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
