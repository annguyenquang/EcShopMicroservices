﻿namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);

public record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResult>;

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductByIdQueryHandler.Handle called with {query}");

        var product = await session.LoadAsync<Product>(query.Id);

        if(product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}
