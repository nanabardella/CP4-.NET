namespace Kova.Domain.Exceptions;

public sealed class ResourceNotFoundException(string resource, Guid id)
    : DomainException($"{resource} com o identificador '{id}' não foi encontrado.")
{
}
