using System.Threading.Tasks;

namespace ToDoList.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string username, string hashedPassword);
    }
}