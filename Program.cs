using DeskFlowAPI.ChamadoService;
using DeskFlowAPI.Data;
using DeskFlowAPI.Middlewares;
using DeskFlowAPI.Repositories;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<AppDbContext>();

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

app.MapControllers();

app.Run();