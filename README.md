# CP4 - Health Checks, Observabilidade e Testes | KOVA

## Integrantes do Grupo

| Nome                           | RM     |
| ------------------------------ | ------ |
| Erick Takeshi Andrade Nakajune | 566059 |
| Giovanna Bardella Gomes        | 561439 |

---

## Domínio do Projeto

O domínio escolhido para o projeto é um **E-commerce de Roupas**.

O projeto KOVA representa uma loja online, modelada com foco no fluxo principal de compra. O sistema permite que clientes visualizem produtos, realizem pedidos e efetuem pagamentos, com as entidades organizadas em camadas seguindo os princípios de Clean Architecture.

---

## Objetivo desta Entrega

Esta entrega evolui o CP3 para uma API operável, observável e testada, mantendo:

* persistência com Entity Framework Core
* organização em camadas
* criação de DbContext
* mapeamentos com Fluent API
* criação e versionamento de migrations
* implementação de repositórios por contrato
* configuração de injeção de dependência
* uso de connection string de forma segura
* endpoints REST documentados com Swagger/OpenAPI
* repositório genérico e tratamento global de exceções
* health checks do processo e do banco em `GET /health`
* logs estruturados com `traceId`
* testes unitários de Domain e Application com xUnit e Moq

---

## Tecnologias Utilizadas

* .NET
* ASP.NET Core Web API
* Entity Framework Core
* Oracle Entity Framework Core Provider
* Oracle Database

---

## Estrutura da Solução

A solução está organizada em camadas:

* **Kova.Domain**
  Contém as entidades e regras centrais do domínio.

* **Kova.Application**
  Contém as interfaces de repositório e contratos da aplicação.

* **Kova.Infrastructure**
  Contém:

  * DbContext
  * configurações de mapeamento com Fluent API
  * implementações de repositório
  * migrations

* **Kova.api**
  Projeto responsável pela exposição da API, controllers, DTOs de HTTP, Swagger e configuração de DI.

---

## Entidades Modeladas

As principais entidades do sistema são:

* Cliente
* Pedido
* Pagamento
* Produto
* Categoria

---

## Relacionamentos do Sistema

Os relacionamentos foram modelados da seguinte forma:

* Cliente -> Pedido
  Um cliente pode realizar um ou mais pedidos.

* Pedido -> Pagamento
  Todo pedido possui um pagamento associado.

* Produto -> Categoria
  Cada produto possui uma categoria.

* Pedido <-> Produto
  Relacionamento muitos-para-muitos (N:N) mapeado explicitamente.

---

## Persistência com EF Core

A persistência foi implementada na camada Infrastructure, contendo:

* KovaDbContext
* configurações por entidade com IEntityTypeConfiguration
* aplicação automática das configurações com ApplyConfigurationsFromAssembly
* mapeamentos com Fluent API
* migration inicial com o esquema do banco
* repositórios concretos

---

## Repositórios

As interfaces dos repositórios foram definidas na camada Application, e suas implementações foram criadas na camada Infrastructure.

Exemplos:

* IClienteRepository
* ICategoriaRepository
* IProdutoRepository
* IPedidoRepository
* IPagamentoRepository

---

## Injeção de Dependência

O registro de dependências foi configurado no projeto da API por meio do método:

```csharp
builder.Services.AddInfrastructure(builder.Configuration);
```

Esse método centraliza o registro do DbContext e dos repositórios no container de injeção de dependência da aplicação.

O contrato genérico `IRepository<T>` fica na camada Application e a implementação `Repository<T>` fica na Infrastructure. O registro é feito com `AddScoped(typeof(IRepository<>), typeof(Repository<>))`. O fluxo de categorias usa esse contrato para listar, consultar e criar registros; os repositórios específicos continuam disponíveis para consultas com relacionamentos.

## API REST e Swagger

Os endpoints usam DTOs de request/response e não expõem entidades de domínio diretamente:

* `GET /api/categorias`, `GET /api/categorias/{id}` e `POST /api/categorias`
* `GET /api/clientes`, `GET /api/clientes/{id}` e `POST /api/clientes`
* `GET /api/produtos`, `GET /api/produtos/{id}` e `POST /api/produtos`

Depois de iniciar a API, a documentação está disponível em:

`https://localhost:<porta>/swagger`

As actions possuem comentários XML, respostas HTTP declaradas e exemplos de sucesso, validação e recurso não encontrado na interface do Swagger.

## Tratamento global de erros

`GlobalExceptionHandler` implementa `IExceptionHandler` e retorna respostas RFC 7807 com `Content-Type: application/problem+json`:

| Exceção | Status |
| --- | --- |
| `ArgumentException` ou `DomainException` | 400 |
| `ResourceNotFoundException` ou `KeyNotFoundException` | 404 |
| `ConflictException` | 409 |
| Demais exceções | 500 |

Detalhes internos de erros 500 são exibidos apenas em Development; em outros ambientes a resposta usa uma mensagem genérica.

---

## Banco de Dados Utilizado

O SGBD utilizado neste projeto é:

**Oracle Database**

---

## Connection String

A connection string deve ser configurada no arquivo `appsettings.Development.json` do projeto da API.

Exemplo seguro:

```json
{
  "ConnectionStrings": {
    "KovaOracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=<USUARIO>;Password=<SENHA>;"
  }
}
```


## Como Executar o Projeto

1. Restaurar os pacotes

```
dotnet restore
```

2. Compilar a solução

```
dotnet build
```

3. Aplicar a migration no banco

```
dotnet ef database update --project Kova.Infrastructure --startup-project Kova.api
```

4. Executar a API

```
dotnet run --project Kova.api
```

5. Abrir o Swagger

Com a API em execução, acesse `https://localhost:<porta>/swagger` ou a URL HTTP informada pelo terminal.

Exemplo de criação de categoria:

```http
POST /api/categorias
Content-Type: application/json

{
  "nome": "Camisetas",
  "descricao": "Camisetas e peças básicas"
}
```

---

## Migrations

A solução contém migration versionada para criação inicial do schema do banco.

Exemplo de comando para gerar migrations:

```
dotnet ef migrations add InitialCreate --project Kova.Infrastructure --startup-project Kova.api
```

Exemplo de comando para aplicar:

```
dotnet ef database update --project Kova.Infrastructure --startup-project Kova.api
```

---

## Evidências

As evidências complementares da entrega podem ser encontradas na pasta `/docs`, incluindo:

* diagrama do esquema físico
* MER atualizado
* prints do banco após aplicação da migration
* justificativas de ajustes realizados

---

## Observações Finais

O projeto foi estruturado respeitando os princípios de separação por camadas, mantendo a persistência concentrada na Infrastructure, os contratos na Application e a configuração da API no projeto Kova.api.

Nenhuma regra de negócio complexa foi colocada na camada de infraestrutura, mantendo o foco da entrega em persistência, mapeamento e organização arquitetural.

## CP4: Health Checks, Observabilidade e Testes

### Health checks

O endpoint único `GET /health` retorna JSON com o status geral, duração, `traceId` e os checks nomeados `self` e `database`. O check `self` confirma que o processo está ativo e `database` usa `AddDbContextCheck<KovaDbContext>` para verificar o Oracle do CP2. Respostas `Healthy` e `Degraded` usam HTTP 200; `Unhealthy` usa HTTP 503. Detalhes de exceções do banco aparecem somente em Development.

Exemplos de respostas estão em `docs/health-healthy.json` e `docs/health-unhealthy.json`.

### Logs estruturados

O POST de produtos registra início e sucesso com propriedades nomeadas (`CategoriaId`, `ProdutoId`) e `HttpContext.TraceIdentifier`. O `GlobalExceptionHandler` registra exceções em nível Error com o mesmo `traceId`; em Development esse identificador também aparece no `ProblemDetails`.

### Testes automatizados

Os projetos `Kova.Domain.Tests` e `Kova.Application.Tests` fazem parte da solução. O primeiro testa as regras de `Categoria` sem mocks, com `[Fact]` e `[Theory]`; o segundo testa `ProdutoService` com Moq, verificando que uma categoria inexistente lança `ResourceNotFoundException` sem chamar `Add` ou `SaveChanges`.

Execute todos os testes a partir da pasta `KOVA`:

```powershell
dotnet test
```

As evidências de observabilidade estão em `docs/observability-example.log`.
