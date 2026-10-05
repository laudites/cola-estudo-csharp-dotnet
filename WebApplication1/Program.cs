


// ============================================================
// PROGRAM.CS - EXEMPLO DE ARQUITETURA ASP.NET CORE
//
// FLUXO DA APLICAÇÃO:
//
// ConsoleApp
//      ↓ HTTP
// Controller
//      ↓
// Interface do Service
//      ↓
// Service
//      ↓
// Banco / Lista em memória
//
// Neste exemplo estamos usando uma LISTA EM MEMÓRIA.
//
// Os exemplos de SQL Server, MySQL e PostgreSQL
// estão comentados abaixo para referência.
// ============================================================


// ============================================================
// USING ATUAL
// ============================================================

using WebApplication1.Service;


// ============================================================
// USING PARA ENTITY FRAMEWORK
//
// Descomentar caso seja utilizado banco de dados.
// ============================================================

// using Microsoft.EntityFrameworkCore;
// using WebApplication1.Data;


var builder = WebApplication.CreateBuilder(args);


// ============================================================
// MVC + API CONTROLLERS
// ============================================================
//
// Permite utilizar:
// Controller
// ControllerBase
// Views
// APIs
//
builder.Services.AddControllersWithViews();


// ============================================================
// DEPENDENCY INJECTION - SERVICE
// ============================================================
//
// Quando algum Controller pedir:
//
// IClienteService
//
// o .NET cria:
//
// ClienteService
//
// Exemplo:
//
// public ClientesController(IClienteService clienteService)
//
//                  ↓
//
// builder.Services.AddScoped<IClienteService, ClienteService>();
//
// ============================================================

builder.Services.AddScoped<IClienteService, ClienteService>();


// ============================================================
// BANCO DE DADOS
//
// NÃO ESTAMOS USANDO BANCO NESTE EXERCÍCIO.
//
// Os exemplos abaixo ficam somente como COLA.
// ============================================================



// ============================================================
// SQL SERVER
// ============================================================
//
// PACOTE NUGET:
//
// Microsoft.EntityFrameworkCore.SqlServer
//
// USING:
//
// using Microsoft.EntityFrameworkCore;
// using WebApplication1.Data;
//
// CONFIGURAÇÃO:
//
// builder.Services.AddDbContext<AppDbContext>(
//     options =>
//         options.UseSqlServer(
//             builder.Configuration
//                 .GetConnectionString("SqlServer")
//         )
// );
//
// FLUXO:
//
// ClienteService
//      ↓
// AppDbContext
//      ↓
// Entity Framework
//      ↓
// SQL Server
//
// EXEMPLO DE CONSULTA:
//
// return await _context.Clientes.ToListAsync();
//
// Buscar por Id:
//
// return await _context.Clientes
//     .FirstOrDefaultAsync(c => c.Id == id);
//
// ============================================================



// ============================================================
// MYSQL
// ============================================================
//
// PROVIDER MAIS COMUM:
//
// Pomelo.EntityFrameworkCore.MySql
//
// USING:
//
// using Microsoft.EntityFrameworkCore;
// using WebApplication1.Data;
//
// PRIMEIRO PEGAMOS A CONNECTION STRING:
//
// string connectionString =
//     builder.Configuration
//         .GetConnectionString("MySql")!;
//
// DEPOIS:
//
// builder.Services.AddDbContext<AppDbContext>(
//     options =>
//         options.UseMySql(
//             connectionString,
//             ServerVersion.AutoDetect(connectionString)
//         )
// );
//
// FLUXO:
//
// ClienteService
//      ↓
// AppDbContext
//      ↓
// Entity Framework
//      ↓
// MySQL
//
// ============================================================



// ============================================================
// POSTGRESQL
// ============================================================
//
// PACOTE NUGET:
//
// Npgsql.EntityFrameworkCore.PostgreSQL
//
// USING:
//
// using Microsoft.EntityFrameworkCore;
// using WebApplication1.Data;
//
// CONFIGURAÇÃO:
//
// builder.Services.AddDbContext<AppDbContext>(
//     options =>
//         options.UseNpgsql(
//             builder.Configuration
//                 .GetConnectionString("PostgreSql")
//         )
// );
//
// FLUXO:
//
// ClienteService
//      ↓
// AppDbContext
//      ↓
// Entity Framework
//      ↓
// PostgreSQL
//
// ============================================================



// ============================================================
// CRIA A APLICAÇÃO
// ============================================================

var app = builder.Build();


// ============================================================
// TRATAMENTO DE ERROS
// ============================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}


// ============================================================
// REDIRECIONA HTTP → HTTPS
// ============================================================

app.UseHttpsRedirection();


// ============================================================
// ROUTING
// ============================================================
//
// Permite ao ASP.NET descobrir qual Controller deve atender
// determinada URL.
//
app.UseRouting();


// ============================================================
// AUTORIZAÇÃO
// ============================================================

app.UseAuthorization();


// ============================================================
// ARQUIVOS ESTÁTICOS
//
// CSS
// JavaScript
// imagens
// etc.
// ============================================================

app.MapStaticAssets();


// ============================================================
// API CONTROLLERS
// ============================================================
//
// Necessário para funcionar:
//
// [ApiController]
// [Route("api/[controller]")]
//
// Por exemplo:
//
// ClientesController
//
// vira:
//
// /api/clientes
//
app.MapControllers();


// ============================================================
// MVC TRADICIONAL
// ============================================================
//
// Exemplo:
//
// /Home/Index
//
// executa:
//
// HomeController.Index()
//
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


// ============================================================
// INICIA A APLICAÇÃO
// ============================================================

app.Run();