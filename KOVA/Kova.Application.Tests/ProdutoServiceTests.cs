using System;
using Kova.Application.DTOs;
using Kova.Application.Interfaces.Repositories;
using Kova.Application.Services;
using Kova.Domain.Entities;
using Kova.Domain.Exceptions;
using Moq;
using Xunit;

namespace Kova.Application.Tests;

public sealed class ProdutoServiceTests
{
    [Fact]
    public void CriacaoSemCategoriaExistente_DeveLancarExcecaoENaoPersistir()
    {
        var categoriaId = Guid.NewGuid();
        var produtoRepository = new Mock<IRepository<Produto>>();
        var categoriaRepository = new Mock<IRepository<Categoria>>();
        categoriaRepository.Setup(repository => repository.ExistsById(categoriaId)).Returns(false);
        var service = new ProdutoService(produtoRepository.Object, categoriaRepository.Object);
        var request = ValidRequest(categoriaId);

        var exception = Assert.Throws<ResourceNotFoundException>(() => service.Create(request));

        Assert.Contains(categoriaId.ToString(), exception.Message);
        produtoRepository.Verify(repository => repository.Add(It.IsAny<Produto>()), Times.Never);
        produtoRepository.Verify(repository => repository.SaveChanges(), Times.Never);
    }

    [Fact]
    public void CriacaoComCategoriaExistente_DevePersistirUmaVez()
    {
        var categoriaId = Guid.NewGuid();
        var produtoRepository = new Mock<IRepository<Produto>>();
        var categoriaRepository = new Mock<IRepository<Categoria>>();
        categoriaRepository.Setup(repository => repository.ExistsById(categoriaId)).Returns(true);
        var service = new ProdutoService(produtoRepository.Object, categoriaRepository.Object);

        var response = service.Create(ValidRequest(categoriaId));

        Assert.Equal(categoriaId, response.CategoriaId);
        produtoRepository.Verify(repository => repository.Add(It.IsAny<Produto>()), Times.Once);
        produtoRepository.Verify(repository => repository.SaveChanges(), Times.Once);
    }

    private static ProdutoRequest ValidRequest(Guid categoriaId) => new()
    {
        Nome = "Camiseta basica",
        Descricao = "Camiseta de algodao para teste",
        Preco = 79.90m,
        CategoriaId = categoriaId
    };
}
