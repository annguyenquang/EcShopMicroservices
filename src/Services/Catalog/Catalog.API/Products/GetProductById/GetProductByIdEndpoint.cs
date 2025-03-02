using Catalog.API.Models;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

//public record GetProductByIdRequest(Guid ProductId);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid Id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(Id);
            var result = await sender.Send(query);
            return Results.Ok(result.Adapt<GetProductByIdResponse>());
        })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
    }
}
