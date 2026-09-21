namespace Kova.Domain.Exceptions;

public sealed class ConflictException(string message) : DomainException(message)
{
}
