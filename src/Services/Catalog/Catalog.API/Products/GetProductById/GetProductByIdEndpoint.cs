using Catalog.API.Models;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

public record GetProductByIdRequest(Guid ProductId);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid Id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(Id);
            var result = await sender.Send(query);
            return result.Adapt<GetProductByIdResponse>();
        });
    }
}
