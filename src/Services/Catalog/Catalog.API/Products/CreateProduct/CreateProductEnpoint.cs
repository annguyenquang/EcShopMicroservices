using Carter;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
        );
    public record CreateProductResponse(Guid Id);
    public class CreateProductEnpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
