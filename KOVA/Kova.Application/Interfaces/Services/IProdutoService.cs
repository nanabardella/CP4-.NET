using Kova.Application.DTOs;

namespace Kova.Application.Interfaces.Services;

public interface IProdutoService
{
    ProdutoResponse Create(ProdutoRequest request);
}
