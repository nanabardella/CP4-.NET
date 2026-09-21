using System.Reflection;
using Kova.api.Exceptions;
using Kova.api.HealthChecks;
using Kova.Application.Interfaces.Services;
using Kova.Application.Services;
using Kova.Infrastructure;
 
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddKovaHealthChecks();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "KOVA E-commerce API",
        Version = "v1",
        Description = "API REST para gerenciamento de clientes, categorias e produtos do e-commerce KOVA."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});
 
builder.Services.AddInfrastructure(builder.Configuration);
 
var app = builder.Build();

app.UseExceptionHandler();
 
if (app.Environment.IsDevelopment())

{

    app.UseSwagger();

    app.UseSwaggerUI();

}
 
app.UseHttpsRedirection();
 
app.UseAuthorization();
 
app.UseKovaHealthChecks();
app.MapControllers();
 
app.Run();