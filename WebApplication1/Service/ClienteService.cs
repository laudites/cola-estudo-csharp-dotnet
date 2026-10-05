using WebApplication1.Models;

// ============================================================
// ESTES USING SERIAM NECESSÁRIOS COM ENTITY FRAMEWORK:
//
// using Microsoft.EntityFrameworkCore;
// using WebApplication1.Data;
// ============================================================


namespace WebApplication1.Service
{
    public class ClienteService : IClienteService
    {
        // ====================================================
        // VERSÃO ATUAL:
        //
        // Dados em memória.
        //
        // Não estamos conectados em banco.
        // ====================================================

        private readonly List<Cliente> _clientes;


        public ClienteService()
        {
            _clientes = new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Nome = "João",
                    Email = "joao@example.com",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 2,
                    Nome = "Maria",
                    Email = "maria@example.com",
                    Ativo = false
                },

                new Cliente
                {
                    Id = 3,
                    Nome = "Pedro",
                    Email = "pedro@example.com",
                    Ativo = false
                }
            };
        }


        // ====================================================
        // BUSCAR TODOS
        // ====================================================

        public Task<List<Cliente>> BuscarTodosAsync()
        {
            // Como os dados já estão na memória,
            // não existe uma operação assíncrona real.
            //
            // Task.FromResult transforma o resultado
            // numa Task.

            return Task.FromResult(_clientes);
        }


        // ====================================================
        // BUSCAR POR ID
        // ====================================================

        public Task<Cliente?> BuscarPorIdAsync(int id)
        {
            Cliente? cliente =
                _clientes.FirstOrDefault(
                    c => c.Id == id
                );

            return Task.FromResult(cliente);
        }



        // ====================================================
        // COMO FICARIA COM ENTITY FRAMEWORK + BANCO
        // ====================================================
        //
        // Em vez da List<Cliente>, teríamos:
        //
        // private readonly AppDbContext _context;
        //
        //
        // O construtor seria:
        //
        // public ClienteService(AppDbContext context)
        // {
        //     _context = context;
        // }
        //
        //
        // Buscar todos:
        //
        // public async Task<List<Cliente>> BuscarTodosAsync()
        // {
        //     return await _context.Clientes
        //         .ToListAsync();
        // }
        //
        //
        // Buscar por Id:
        //
        // public async Task<Cliente?> BuscarPorIdAsync(int id)
        // {
        //     return await _context.Clientes
        //         .FirstOrDefaultAsync(c => c.Id == id);
        // }
        //
        //
        // IMPORTANTE:
        //
        // O código acima seria praticamente o mesmo
        // para:
        //
        // SQL Server
        // MySQL
        // PostgreSQL
        //
        // O que muda principalmente é o provider configurado
        // no Program.cs.
        //
        // ====================================================
    }
}