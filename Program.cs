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
        .AddSingleton<SalesPersonService>()
        .AddSingleton<SalesOverviewService>()
        .AddProblemDetails()
        .AddExceptionHandler<GlobalExceptionHandler>()
        .AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });


        var app = builder.Build();

        app.UseStatusCodePages();
        app.UseExceptionHandler();

        app.UseCors("AllowAll");
        //Test methods
        app.MapGet("/", () => "Hello World!");
        app.MapGet("/test", (DataClient dataClient) => dataClient.TestConnection());
        //Person methods
        app.MapGet("/person", (PersonService personService) => personService.GetAll());
        app.MapGet("/person/GetByName", (PersonService personService, [FromQuery] string name) => personService.GetPersonByName(name));
        app.MapGet("/person/GetByEmpType", (PersonService personService, [FromQuery] string emplType) => personService.GetPersonByPersonType(emplType));
        app.MapGet("/person/GetByNameAndType", (PersonService personService, [FromQuery] string name, string emplType) => personService.GetPersonByNameAndPersonType(name, emplType));
        //Products methods
        app.MapGet("/product", (ProductService productService) => productService.GetAll());
        app.MapGet("/product/GetByName", (ProductService productService, [FromQuery] string name) => productService.GetProductByName(name));
        app.MapGet("/product/GetByCategoryType", (ProductService productService, [FromQuery] string categoryType) => productService.GetProductsByCategoryType(categoryType));
        app.MapGet("/product/GetByCulture", (ProductService productService, [FromQuery] string cultureID = "en") => productService.GetByCultureID(cultureID));
        app.MapGet("/product/GetByNameCultureAndCategoryType", (ProductService productService, [FromQuery] string name, string categoryType, string cultureID = "en") => productService.GetByNameCultureAndCategoryType(name, cultureID, categoryType));

        //SalesOverviewMethods
        app.MapGet("/sales-overview", (SalesOverviewService salesOverviewService) => salesOverviewService.GetGroupedByTerritory());

        //SalesPersonMethods
        app.MapGet("/sales-person", (SalesPersonService salesPersonService) => salesPersonService.GetAll());
        app.MapGet("/sales-person/GetByNameAndYear", (SalesPersonService salesPersonService, [FromQuery] string name, string year) => salesPersonService.GetByNameAndYear(name, year));


        app.Run();
    }
}