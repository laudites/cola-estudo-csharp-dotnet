using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "João",
                    Email = "joao@example.com"
                },

                new User
                {
                    Id = 2,
                    Name = "Maria",
                    Email = "maria@example.com"
                },

                new User
                {
                    Id = 3,
                    Name = "Pedro",
                    Email = "pedro@example.com"
                }
            };

            return Ok(users);
        }
    }
}