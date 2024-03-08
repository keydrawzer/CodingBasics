using Microsoft.AspNetCore.Mvc;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
        .AddSingleton<DataClient>() //Singleton es una instancia unica
        .AddSingleton<PersonService>()
        .AddSingleton<ProductsService>()
        .AddSingleton<SalesOverService>()
        .AddSingleton<SalesPYService>()
        .AddCors(options => //AddCors aplicacion necesaria
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
        app.MapGet("/products/GetByName", (ProductsService ProductsService, [FromQuery] string name) => Results.Ok(ProductsService.GetProductsByName(name)));
        app.MapGet("/products/GetByCategory", (ProductsService ProductsService, [FromQuery] string categoryType) => Results.Ok(ProductsService.GetProductsByCategory(categoryType)));
        //Sales methods
        app.MapGet("/sales", (SalesOverService salesService) => Results.Ok(salesService.GetAll()));
        app.MapGet("/sales/GetByPersonAndYear", (SalesPYService salespyServices, [FromQuery] string salesPersonName, int year) => Results.Ok(salespyServices.GetSalesByPersonAndYear(salesPersonName, year)));
        app.Run();
    }
}