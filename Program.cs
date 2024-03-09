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
        app.MapGet("/", () => "Hello I am  Jonathan");
        app.MapGet("/test", (DataClient dataClient) => dataClient.TestConnection());
        //Person methods
        app.MapGet("/person", (PersonService personService) => Results.Ok(personService.GetAll()));
        app.MapGet("/person/GetPersonByName", (PersonService personService, [FromQuery] string name) => Results.Ok(personService.GetPersonByName(name)));
        app.MapGet("/person/GetPersonByPersonType", (PersonService personService, [FromQuery] string emplType) => Results.Ok(personService.GetPersonByPersonType(emplType)));
        app.MapGet("/person/GetByNameAndType", (PersonService personService, [FromQuery] string name, string emplType) 
        => Results.Ok(personService.GetPersonByNameAndPersonType(name, emplType)));
        //Products methods
        app.MapGet("/product", (ProductService productService) => Results.Ok(productService.GetAll()));
        app.MapGet("/product/GetProductsByName", (ProductService productService, [FromQuery] string name) => Results.Ok(productService.GetProductsByName(name)));
        app.MapGet("/product/GetProductByCategoryType", (ProductService productService, [FromQuery] string catType) => Results.Ok(productService.GetProductByCategoryType(catType)));
        app.MapGet("/product/GetByNameAndType", (ProductService productService, [FromQuery] string name, string catType) 
        => Results.Ok(productService.GetProductByNameAndCategoryType(name, catType)));
        app.Run();
    }
}