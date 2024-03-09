using Microsoft.AspNetCore.Mvc;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        // Register services
        RegisterServices(builder.Services);

        var app = builder.Build();

        // Use CORS policy
        app.UseCors("AllowAll");

        // Test methods
        app.MapGet("/", () => "Hello World!");
        app.MapGet("/test", (DataClient dataClient) => dataClient.TestConnection());

        // Person methods
        MapPersonRoutes(app);

        // Products methods
        MapProductRoutes(app);

        // Sales methods
        MapSalesRoutes(app);

        app.Run();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<DataClient>();
        services.AddSingleton<PersonService>();
        services.AddSingleton<ProductService>();
        services.AddSingleton<SalesService>();
    }

    private static void MapPersonRoutes(WebApplication app)
    {
        app.MapGet("/person", (PersonService personService) => Results.Ok(personService.GetAll()));
        app.MapGet("/person/GetByName", (PersonService personService, [FromQuery] string name) => Results.Ok(personService.GetPersonByName(name)));
        app.MapGet("/person/GetByEmpType", (PersonService personService, [FromQuery] string emplType) => Results.Ok(personService.GetPersonByPersonType(emplType)));
        app.MapGet("/person/GetByNameAndType", (PersonService personService, [FromQuery] string name, string emplType) => Results.Ok(personService.GetPersonByNameAndPersonType(name, emplType)));
    }

    private static void MapProductRoutes(WebApplication app)
    {
        app.MapGet("/product", (ProductService productService) => Results.Ok(productService.GetAll()));
        app.MapGet("/product/GetByName", (ProductService productService, [FromQuery] string name) => Results.Ok(productService.GetProductByName(name)));
        app.MapGet("/product/GetByModel", (ProductService productService, [FromQuery] string model) => Results.Ok(productService.GetProductByModel(model)));
    }

    private static void MapSalesRoutes(WebApplication app)
    {
        app.MapGet("/sales", (SalesService salesService) => Results.Ok(salesService.GetAll()));
        app.MapGet("/sales/GetByName", (SalesService salesService, [FromQuery] string name) => Results.Ok(salesService.GetSalesByName(name)));
        app.MapGet("/sales/GetByNameAndYear", (SalesService salesService, [FromQuery] string name, string year) => Results.Ok(salesService.GetSalesByNameAndYear(name, year)));
    }

}