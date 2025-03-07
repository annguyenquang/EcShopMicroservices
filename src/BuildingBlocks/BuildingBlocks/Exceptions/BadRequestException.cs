namespace BuildingBlocks.Exceptions;

internal class InternalServerException : Exception
{
    public InternalServerException() : base()
    {
        
    }

    public InternalServerException(string message, string details) : base(message) 
    {
        Details = details; 
    }

    public string? Details { get; }
}
