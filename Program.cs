using CodingBasics.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using CodingBasics.Services;


var builder = WebApplication.CreateBuilder(args);

//Dependency injection
builder.Services.AddDbContext<AdventureWorksDbContext>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<SalesService>();

//CORS configuration to allow requests from the frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors("AllowLocalhost");

app.MapGet("/", () => "Hello World!");


//Methods for Persons
app.MapGet("/person", (PersonService personService) =>
{
    var employees = personService.GetAllPersons();
    return employees;
});

app.MapGet("/person/GetByName", (PersonService personService, [FromQuery] string name) =>
{
    var persons = personService.GetPersonsByName(name);
    return Results.Json(persons);
});

app.MapGet("/person/GetByEmpType", (PersonService personService, [FromQuery] string personType) =>
{
    var results = personService.GetPersonsByEmpType(personType);
    return Results.Json(results);
});

app.MapGet("/person/GetByNameAndType",(PersonService personService, [FromQuery] string name, [FromQuery] string personType) =>
{
    var results = personService.GetPersonByNameAndPersonType(name, personType);
    return Results.Json(results);
});


//Methods for Products
app.MapGet("/product", (ProductService productService) =>
{
    var products = productService.GetAllProducts();
    return products;
});

app.MapGet("/product/GetByName", (ProductService productService, [FromQuery] string name) =>
{
    var products = productService.GetProductsByName(name);
    return Results.Json(products);
});

app.MapGet("/product/GetByCategoryType", (ProductService productService, [FromQuery] string categoryType) =>
{
    var results = productService.GetProductByCategoryType(categoryType);
    return Results.Json(results);
});

app.MapGet("/product/GetByNameAndCategoryType", (ProductService productService, [FromQuery] string name, [FromQuery] string categoryType) =>
{
    var results = productService.GetProductByNameAndCategoryType(name, categoryType);
    return Results.Json(results);
});


//Methods for Sales *CHALLENGE*

//Total Sales by SalesPerson
app.MapGet("/sales", (SalesService salesService) =>
{
    var results = salesService.GetTotalSales();
    return Results.Json(results);
});

//Sales by SalesPerson and Year
app.MapGet("/sales/GetByNameAndYear", (SalesService salesService, [FromQuery] string name, [FromQuery] string year) =>
{
    var results = salesService.GetSalestByNameAndYear(name, year);
    return Results.Json(results);
});

app.Run();

