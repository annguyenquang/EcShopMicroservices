namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Product);

//public record GetProductsByCategoryRequest(string Category);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var query = new GetProductsByCategoryQuery(category);
            var result = await sender.Send(query);
            return Results.Ok(new GetProductsByCategoryResponse(result.Products));
        })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get Product By Category");
    }
}
