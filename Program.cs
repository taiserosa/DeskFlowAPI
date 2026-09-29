using System.Text.Json.Serialization;
using DeskFlowAPI.Data;
using DeskFlowAPI.Middlewares;
using DeskFlowAPI.Repositories;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi( op => 
{
    op.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>();
        doc.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "Identity access token",
            In = ParameterLocation.Header,
            Description = "Cole apenas o accessToken retornado por /auth/login. O Swagger adiciona 'Bearer' automaticamente."
        };
        return Task.CompletedTask;
    });

    op.AddOperationTransformer((operation, context, _) =>
    {
        if (context.Description.ActionDescriptor.EndpointMetadata.Any(metadata => metadata is IAuthorizeData))
        {
            operation.Security ??= [];
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", context.Document, null)] = []
            });
        }

        return Task.CompletedTask;
    });

});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<IInteracaoRepository, InteracaoRepository>();
builder.Services.AddScoped<IInteracaoService, InteracaoService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapOpenApi();

// configurando o swagger
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGroup("/api/auth").MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();