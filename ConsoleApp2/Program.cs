using System.Text.Json;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<TodoItem> todos = new List<TodoItem>();

            int nextId = 1;

            bool executando = true;

            while (executando)
            {
                Console.WriteLine();
                Console.WriteLine("TODO Console App");
                Console.WriteLine();

                Console.WriteLine("Comandos:");
                Console.WriteLine("add      - Adicionar tarefa");
                Console.WriteLine("list     - Listar tarefas");
                Console.WriteLine("done     - Marcar tarefa como concluída");
                Console.WriteLine("remove   - Remover tarefa");
                Console.WriteLine("users    - Buscar usuários da API");
                Console.WriteLine("clientes - Buscar clientes da API / banco");
                Console.WriteLine("exit     - Sair");
                Console.WriteLine();

                Console.Write("Comando: ");

                string? comando = Console.ReadLine();

                switch (comando?.ToLower())
                {
                    // =====================================================
                    // TODO - ADICIONAR
                    // =====================================================
                    case "add":

                        Console.Write("Digite a tarefa: ");

                        string? titulo = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(titulo))
                        {
                            Console.WriteLine("Tarefa inválida.");
                            break;
                        }

                        TodoItem novaTarefa = new TodoItem
                        {
                            Id = nextId++,
                            Title = titulo,
                            Done = false
                        };

                        todos.Add(novaTarefa);

                        Console.WriteLine("Tarefa adicionada.");

                        break;


                    // =====================================================
                    // TODO - LISTAR
                    // =====================================================
                    case "list":

                        if (!todos.Any())
                        {
                            Console.WriteLine("Nenhuma tarefa.");
                            break;
                        }

                        foreach (var todo in todos)
                        {
                            string status =
                                todo.Done
                                    ? "Concluída"
                                    : "Pendente";

                            Console.WriteLine(
                                $"{todo.Id} - " +
                                $"{todo.Title} - " +
                                $"{status}"
                            );
                        }

                        break;


                    // =====================================================
                    // TODO - MARCAR COMO CONCLUÍDA
                    // =====================================================
                    case "done":

                        Console.Write("Digite o Id da tarefa: ");

                        if (!int.TryParse(
                                Console.ReadLine(),
                                out int idDone))
                        {
                            Console.WriteLine("Id inválido.");
                            break;
                        }

                        TodoItem? todoEncontrado =
                            todos.FirstOrDefault(
                                t => t.Id == idDone);

                        if (todoEncontrado == null)
                        {
                            Console.WriteLine(
                                "Tarefa não encontrada."
                            );

                            break;
                        }

                        todoEncontrado.Done = true;

                        Console.WriteLine(
                            "Tarefa marcada como concluída."
                        );

                        break;


                    // =====================================================
                    // TODO - REMOVER
                    // =====================================================
                    case "remove":

                        Console.Write("Digite o Id da tarefa: ");

                        if (!int.TryParse(
                                Console.ReadLine(),
                                out int idRemove))
                        {
                            Console.WriteLine("Id inválido.");
                            break;
                        }

                        TodoItem? todoRemover =
                            todos.FirstOrDefault(
                                t => t.Id == idRemove);

                        if (todoRemover == null)
                        {
                            Console.WriteLine(
                                "Tarefa não encontrada."
                            );

                            break;
                        }

                        todos.Remove(todoRemover);

                        Console.WriteLine(
                            "Tarefa removida."
                        );

                        break;


                    // =====================================================
                    // BUSCAR USERS DA API
                    // =====================================================
                    case "users":

                        await BuscarUsuarios();

                        break;


                    // =====================================================
                    // BUSCAR CLIENTES DA API / BANCO
                    // =====================================================
                    case "clientes":

                        await BuscarClientes();

                        break;


                    // =====================================================
                    // SAIR
                    // =====================================================
                    case "exit":

                        executando = false;

                        break;


                    default:

                        Console.WriteLine(
                            "Comando inválido."
                        );

                        break;
                }
            }
        }


        // =============================================================
        // BUSCAR USERS
        // =============================================================
        static async Task BuscarUsuarios()
        {
            using HttpClient client = new HttpClient();

            string url = "https://localhost:7103/api/values";

            try
            {
                HttpResponseMessage response =
                    await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(
                        $"Erro HTTP: {response.StatusCode}"
                    );

                    return;
                }

                string json =
                    await response.Content
                        .ReadAsStringAsync();

                List<User>? users =
                    JsonSerializer.Deserialize<List<User>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                if (users == null)
                {
                    Console.WriteLine(
                        "Nenhum usuário encontrado."
                    );

                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Usuários da API:");
                Console.WriteLine();

                foreach (var user in users)
                {
                    Console.WriteLine(
                        $"{user.Id} - " +
                        $"{user.Name} - " +
                        $"{user.Email}"
                    );
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(
                    $"Erro ao acessar API: {ex.Message}"
                );
            }
        }


        // =============================================================
        // BUSCAR CLIENTES
        // API -> SERVICE -> DBContext -> SQL Server
        // =============================================================
        static async Task BuscarClientes()
        {
            using HttpClient client = new HttpClient();

            string url = "https://localhost:7103/api/clientes";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(
                        $"Erro HTTP: {response.StatusCode}"
                    );

                    return;
                }

                string json =
                    await response.Content
                        .ReadAsStringAsync();

                List<Cliente>? clientes =
                    JsonSerializer.Deserialize<List<Cliente>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                if (clientes == null)
                {
                    Console.WriteLine(
                        "Nenhum cliente encontrado."
                    );

                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Clientes da API:");
                Console.WriteLine();

                foreach (var cliente in clientes)
                {
                    string status =
                        cliente.Ativo
                            ? "Ativo"
                            : "Inativo";

                    Console.WriteLine(
                        $"{cliente.Id} - " +
                        $"{cliente.Nome} - " +
                        $"{cliente.Email} - " +
                        $"{status}"
                    );
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(
                    $"Erro ao acessar API: {ex.Message}"
                );
            }
        }
    }


    // =============================================================
    // MODEL TODO
    // =============================================================
    class TodoItem
    {
        public int Id { get; set; }

        public string Title { get; set; } =
            string.Empty;

        public bool Done { get; set; }
    }


    // =============================================================
    // MODEL USER
    // Mantemos o User original
    // =============================================================
    class User
    {
        public int Id { get; set; }

        public string Name { get; set; } =
            string.Empty;

        public string Email { get; set; } =
            string.Empty;
    }


    // =============================================================
    // MODEL CLIENTE
    // Compatível com a API /api/clientes
    // =============================================================
    class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } =
            string.Empty;

        public string Email { get; set; } =
            string.Empty;

        public bool Ativo { get; set; }
    }
}