namespace BuildingBlocks.Exceptions;

internal class BadRequestException : Exception
{
    public BadRequestException() : base()
    {
        
    }

    public BadRequestException(string message, string details) : base(message) 
    {
        Details = details; 
    }

    public string? Details { get; }
}
