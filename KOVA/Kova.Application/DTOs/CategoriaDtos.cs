using System.ComponentModel.DataAnnotations;

namespace Kova.Application.DTOs;

public sealed class CategoriaRequest
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nome { get; init; } = string.Empty;

    [Required, StringLength(300, MinimumLength = 10)]
    public string Descricao { get; init; } = string.Empty;
}

public sealed record CategoriaResponse(Guid Id, string Nome, string Descricao, bool Active);
