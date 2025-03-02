using Catalog.API.Models;
using BuildingBlocks.CQRS;

namespace Catalog.API.Products.GetProducts;

public record GetProductResult(IEnumerable<Product> Products);

public record GetProductQuery(
    ) : IQuery<GetProductResult>;

public class GetProductQueryHandler (IDocumentSession session) 
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync();
        return new GetProductResult(products);
    }
}
