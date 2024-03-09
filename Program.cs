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
        app.MapGet("/person/GetByNameAndType", (PersonService personService, [FromQuery] string name, string emplType) 
        => Results.Ok(personService.GetPersonByNameAndPersonType(name, emplType)));
        //Products methods
        app.MapGet("/product", (ProductService productService) => Results.Ok(productService.GetAll()));
        app.MapGet("/product/GetByName", (ProductService productService, [FromQuery] string name) => Results.Ok(productService.GetProductsByName(name)));
        app.MapGet("/product/GetByCatType", (ProductService productService, [FromQuery] string catType) => Results.Ok(productService.GetProductByCategoryType(catType)));
        app.MapGet("/product/GetByNameAndType", (ProductService productService, [FromQuery] string name, string catType) 
        => Results.Ok(productService.GetProductByNameAndCategoryType(name, catType)));
        //Sales methods
        app.MapGet("/sales", (SalesService salesService) => Results.Ok(salesService.GetAll()));
        app.MapGet("/sales/GetByNameAndYear", (SalesService salesService, [FromQuery] string name, string year) 
        => Results.Ok(salesService.GetSalesByNameAndYear(name, year)));
        app.Run();
    }
}