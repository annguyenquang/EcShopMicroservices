using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.GetProductsByCategory;
using Catalog.API.Products.UpdateProduct;
using Catalog.API.Products.DeleteProduct;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to container

builder.Services.AddCarter(configurator: (config) =>
{
    config.WithModules([
        typeof(GetProductsByCategoryEndpoint),
        typeof(GetProductByIdEndpoint),
        typeof(UpdateProductEndpoint),
        typeof(DeleteProductEndpoint),
        typeof(CreateProductEndpoint),
        typeof(GetProductsEnpoint)]);
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>), ServiceLifetime.Scoped);
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Scoped);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddLogging();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler("/Error");

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
