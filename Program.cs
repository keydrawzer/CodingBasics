using Microsoft.AspNetCore.Mvc;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
        .AddSingleton<DataClient>()
        .AddSingleton<PersonService>()
        .AddSingleton<ProductService>()
        .AddSingleton<SalesService>()
        .AddSingleton<SalesPersonYearService>()

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
        //Person methods
        app.MapGet("/person", (PersonService personService) => Results.Ok(personService.GetAll()));
        app.MapGet("/person/GetByName", (PersonService personService, [FromQuery] string name) => Results.Ok(personService.GetPersonByName(name)));
        app.MapGet("/person/GetByEmpType", (PersonService personService, [FromQuery] string emplType) => Results.Ok(personService.GetPersonByPersonType(emplType)));
        app.MapGet("/person/GetByNameAndType", (PersonService personService, [FromQuery] string name, string emplType) => Results.Ok(personService.GetPersonByNameAndPersonType(name, emplType)));
        //Products methods
        app.MapGet("/products", (ProductService productsService) => Results.Ok(productsService.GetAll()));
        app.MapGet("/products/GetByName", (ProductService ProductsService, [FromQuery] string name) => Results.Ok(ProductsService.GetProductsByName(name)));
        app.MapGet("/products/GetByCategory", (ProductService ProductsService, [FromQuery] string categoryType) => Results.Ok(ProductsService.GetProductsByCategory(categoryType)));
        //Sales methods
        app.MapGet("/sales", (SalesService salesService) => Results.Ok(salesService.GetAll()));
        app.MapGet("/sales/GetByPersonAndYear", (SalesPersonYearService salespyServices, [FromQuery] string salesPersonName, int year) => Results.Ok(salespyServices.GetSalesByPersonAndYear(salesPersonName, year)));

        app.Run();
    }
}

