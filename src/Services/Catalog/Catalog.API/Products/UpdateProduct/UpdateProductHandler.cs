
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
     string Name,
     List<string> Category,
     string Description,
     string ImageFile,
     decimal Price
    ) : ICommand<UpdateProductCommandResult>;

public record UpdateProductCommandResult(bool IsSuccess);

internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"UpdateProductHandler.Handle called with {command}");

        var product = await session.LoadAsync<Product>(command.Id);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);

        await session.SaveChangesAsync();

        return new UpdateProductCommandResult(true);
    }
}
