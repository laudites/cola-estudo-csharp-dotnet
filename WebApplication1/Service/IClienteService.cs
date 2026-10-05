using WebApplication1.Models;

namespace WebApplication1.Service
{
    // ========================================================
    // INTERFACE = CONTRATO
    //
    // Quem implementar IClienteService precisa fornecer
    // estes métodos.
    // ========================================================

    public interface IClienteService
    {
        // Retorna vários clientes.
        //
        // Task<List<Cliente>>
        //
        // Task = operação assíncrona
        // List<Cliente> = resultado esperado
        //
        Task<List<Cliente>> BuscarTodosAsync();


        // Retorna:
        //
        // Cliente
        //
        // ou
        //
        // null
        //
        // caso o Id não exista.
        //
        Task<Cliente?> BuscarPorIdAsync(int id);
    }
}