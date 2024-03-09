using Infrastructure;
using Microsoft.AspNetCore.Mvc;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure()
        .AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });


        var app = builder.Build();
        app.UseCors("AllowAll");
        //Test methods
        app.MapGet("/", () => "Hello World!");
        app.MapGet("/test", (DataClient dataClient) => dataClient.TestConnection());
        app.MapGet("/person", (PersonService personService) => Results.Ok(personService.GetAll()));
        app.MapGet("/person/GetByName", (PersonService personService, [FromQuery] string name) => Results.Ok(personService.GetPersonByName(name)));
        app.MapGet("/person/GetByEmpType", (PersonService personService, [FromQuery] string emplType) => Results.Ok(personService.GetPersonByPersonType(emplType)));
        app.MapGet("/person/GetByNameAndType", (PersonService personService, [FromQuery] string name, string emplType) => Results.Ok(personService.GetPersonByNameAndPersonType(name, emplType)));
        //Person methods
        //Products methods
        app.MapGet("/products", (ProductsService productsService) => Results.Ok(productsService.GetAll()));
        app.MapGet("/products/GetByCategoryType", (ProductsService productsService, [FromQuery] string categoryType) => Results.Ok(productsService.GetProductByCategoryType(categoryType)));
        app.MapGet("/products/GetByName", (ProductsService productsService, [FromQuery] string name) => Results.Ok(productsService.GetProductByName(name)));
        app.MapGet("/sales/GetOverviewByPersons", (SalesService salesService) => Results.Ok(salesService.GetOverviewByPersons()));
        app.MapGet("/sales/GetSalesByPersonAndYear", (SalesService salesService, [FromQuery] string person, int? year) => Results.Ok(salesService.GetSalesByPersonAndYear(
            person, year
        )));
        app.Run();
    }
}