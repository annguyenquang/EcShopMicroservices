using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductResult(IEnumerable<Product> Products);

public record GetProductQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductResult>;

public class GetProductQueryHandler (IDocumentSession session) 
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new GetProductResult(products);
    }
}
