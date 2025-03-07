using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.GetProductsByCategory;
using Catalog.API.Products.UpdateProduct;
using BuildingBlocks.Behaviours;
using Catalog.API.Products.DeleteProduct;

var builder = WebApplication.CreateBuilder(args);

// Add services to container

builder.Services.AddCarter(configurator: (config) =>
{
    config.WithModules([
        typeof(GetProductsByCategoryEndpoint), 
        typeof(GetProductByIdEndpoint), 
        typeof(UpdateProductEndpoint), 
        typeof(DeleteProductEndpoint), 
        typeof(CreateProductEnpoint), 
        typeof(GetProductsEnpoint)]);
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")! );
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline

app.MapCarter();

app.Run();
