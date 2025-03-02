


namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductsByCategoryHandler.Handle called with {query}");
        var products = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync();
        return new GetProductsByCategoryResult(products);
    }
}
