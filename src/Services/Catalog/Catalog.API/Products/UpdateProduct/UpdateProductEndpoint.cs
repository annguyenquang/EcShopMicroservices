using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
     string Name,
     List<string> Category,
     string Description,
     string ImageFile,
     decimal Price
    );

public record UpdateProductResult(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async ([FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
    }
}
