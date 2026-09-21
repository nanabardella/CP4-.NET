using System.ComponentModel.DataAnnotations;

namespace Kova.Application.DTOs;

public sealed class ClienteRequest
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nome { get; init; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; init; } = string.Empty;

    [Required, RegularExpression(@"^\d{11}$", ErrorMessage = "Telefone deve conter 11 dígitos.")]
    public string Telefone { get; init; } = string.Empty;
}

public sealed record ClienteResponse(Guid Id, string Nome, string Email, string Telefone, bool Active);
