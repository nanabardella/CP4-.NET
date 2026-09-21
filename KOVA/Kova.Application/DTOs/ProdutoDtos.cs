using System.ComponentModel.DataAnnotations;

namespace Kova.Application.DTOs;

public sealed class ProdutoRequest
{
	[Required, StringLength(100, MinimumLength = 2)]
	public string Nome { get; init; } = string.Empty;

	[Required, StringLength(300, MinimumLength = 10)]
	public string Descricao { get; init; } = string.Empty;

	[Range(0.01, double.MaxValue)]
	public decimal Preco { get; init; }

	public Guid CategoriaId { get; init; }
}

public sealed record ProdutoResponse(Guid Id, string Nome, string Descricao, decimal Preco, Guid CategoriaId, bool Active);
