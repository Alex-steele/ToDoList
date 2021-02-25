using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoListContext context;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(ToDoListContext context, ILogger<UserRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<bool> UserExists(string username, string hashedPassword)
        {
            return await context.ListUsers.AnyAsync(x => x.Email == username && x.Password == hashedPassword);
        }
    }
}
