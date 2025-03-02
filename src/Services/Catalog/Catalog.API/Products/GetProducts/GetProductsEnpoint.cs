using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

//public record GetProductRequest();

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery());

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
                .WithName("Get Product")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Description");

        }
    }
}
