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

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private const int MAX_NAME_LENGTH = 150;
    private const int MIN_NAME_LENGTH = 2;
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("Product Id is required");
        
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name is required")
            .Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH)
                .WithMessage($"Name must be between {MIN_NAME_LENGTH} and {MAX_NAME_LENGTH} characters");

        RuleFor(x => x.Price)
            .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
    }
}

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
