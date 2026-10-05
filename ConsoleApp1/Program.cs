public class Program
{
    static void ClientFixo(List<Cliente> clientes)
    {
        clientes.Add(new Cliente
        {
            Id = 1,
            Nome = "João",
            Email = "joao@example.com",
            Ativo = true
        });

        clientes.Add(new Cliente
        {
            Id = 2,
            Nome = "Maria",
            Email = "maria@example.com",
            Ativo = false
        });

        clientes.Add(new Cliente
        {
            Id = 3,
            Nome = "Pedro",
            Email = "pedro@example.com",
            Ativo = false
        });
    }

    static void Main(string[] args)
    {
        List<Cliente> clientes = new List<Cliente>();
        ClientFixo(clientes);
        bool sistemaAtivo = true;
        while (sistemaAtivo)
        {

            Console.WriteLine("\nClique nas teclas para:");
            Console.WriteLine("9 - Listar clientes");
            Console.WriteLine("1 - Adicionar cliente");
            Console.WriteLine("2 - Editar cliente");
            Console.WriteLine("3 - Remover cliente");
            Console.WriteLine("4 - Buscar cliente");
            Console.WriteLine("5 - Sair\n");

            switch (Console.ReadLine())
            {
                case "9":
                    Console.Clear();
                    ListarClientes(clientes);
                    break;
                case "1":
                    Console.Clear();
                    Console.WriteLine("Digite o nome do cliente:");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Digite o email do cliente:");
                    string email = Console.ReadLine();
                    int id = clientes.Count == 0 ? 1 : clientes.Max(c => c.Id) + 1;
                    bool ativo = true;
                    AdicionarCliente(clientes, new Cliente { Id = id, Nome = nome, Email = email, Ativo = ativo });
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Digita o id do cliente que deseja editar ou 9 para listar:");
                    int idEditar = int.Parse(Console.ReadLine());
                    if (idEditar == 9)
                    {
                        ListarClientes(clientes);

                    }

                    var clienteEditar = clientes.FirstOrDefault(c => c.Id == idEditar);

                    if (clienteEditar == null)
                    {
                        Console.WriteLine("Cliente não encontrado.");
                        break;
                    }

                    Console.WriteLine($"1 - Nome atual: {clienteEditar.Nome}");
                    Console.WriteLine($"2 - Email atual: {clienteEditar.Email}");
                    Console.WriteLine($"Digita a opção que deseja editar:");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("Digite o novo nome do cliente:");
                            string novoNome = Console.ReadLine();
                            EditarCliente(clientes, idEditar, novoNome, clienteEditar.Email);
                            break;
                        case "2":
                            Console.WriteLine("Digite o novo email do cliente:");
                            string novoEmail = Console.ReadLine();
                            EditarCliente(clientes, idEditar, clienteEditar.Nome, novoEmail);
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                    break;
                case "3":
                    Console.Clear();

                    Console.WriteLine("Digita o id do cliente que deseja remover ou 0 para listar:");
                    int idRemover = int.Parse(Console.ReadLine());
                    if (idRemover == 0)
                    {
                        ListarClientes(clientes);
                        Console.WriteLine("Digite o id do cliente que deseja remover:");
                        idRemover = int.Parse(Console.ReadLine());
                    }

                    bool removeu = RemoverCliente(clientes, idRemover);
                    if(removeu)
                    {
                        Console.WriteLine("Cliente removido com sucesso.");

                    }
                    else
                    {
                        Console.WriteLine("Cliente nao encontrado.");

                    }
                    break;
                case "4":
                    Console.Clear();

                    Console.WriteLine("Digite o id do cliente que deseja buscar ou 0 para listar:");
                    int idBuscar = int.Parse(Console.ReadLine());
                    var clienteEncontrado = BuscarCliente(clientes, idBuscar);
                    Console.WriteLine($"Id: {clienteEncontrado.Id} - Nome: {clienteEncontrado.Nome} - Email: {clienteEncontrado.Email} - Ativo: {clienteEncontrado.Ativo}");
                    break;
                case "5":
                    Console.Clear();

                    Console.WriteLine("Saindo do programa...");
                    sistemaAtivo = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void AdicionarCliente(List<Cliente> clientes, Cliente cliente)
    {
        clientes.Add(cliente);
    }

    static void ListarClientes(List<Cliente> clientes)
    {
        foreach (var cliente in clientes)
        {
            string status = cliente.Ativo ? "Ativo" : "Inativo";
            Console.WriteLine($"Id: {cliente.Id} - Nome: {cliente.Nome} - Email: {cliente.Email} - {status}");
        }
    }


    static Cliente? BuscarCliente(List<Cliente> clientes, int id)
    {
        return clientes.FirstOrDefault(c => c.Id == id);
    }


    static void EditarCliente(List<Cliente> clientes, int id, string novoNome, string novoEmail)
    {
        foreach (var cliente in clientes)
        {
            if (cliente.Id == id)
            {
                cliente.Nome = novoNome;
                cliente.Email = novoEmail;
                break;
            }
        }
    }

    static bool RemoverCliente(List<Cliente> clientes,int id)
    {
        Cliente? cliente = BuscarCliente(clientes, id);

        if (cliente == null)
        {
            return false;
        }

        clientes.Remove(cliente);

        return true;
    }
}

class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
}
