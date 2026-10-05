using System.Runtime.CompilerServices;

class program
{
    static void Main(string[] args)
    {
        List<User> users = new List<User>();
        users.Add(
            new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Active = true
            }
        );

        users.Add(
            new User
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Active = false
            }
        );

        users.Add(
            new User
            {
                Id = 3,
                Name = "Willian Laudites",
                Email = "willian.laudites@example.com",
                Active = true
            }
            );

        bool running = true;

        while (running == true)
        {
            Console.WriteLine("selecione uma opcao.");
            Console.WriteLine("1 - Listar usuarios..");
            Console.WriteLine("2 - Criar usuario.");
            Console.WriteLine("3 - Alterar usuario.");
            Console.WriteLine("4 - Deletar usuario.");
            Console.WriteLine("5 - Sair.");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    foreach(var user in users)
                    {
                        Console.WriteLine($"\nID: {user.Id}\nName: {user.Name}\nEmail: {user.Email}\nActive: {user.Active}\n\n");
                    }
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    Console.WriteLine("Saindo do programa...");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opcao invalida.");
                    break;
            }

        }
    }


    class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; }
    }

}