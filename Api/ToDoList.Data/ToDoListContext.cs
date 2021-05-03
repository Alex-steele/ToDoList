using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;

namespace ToDoList.Data
{
    public class ToDoListContext : DbContext
    {
        public DbSet<ListItem> ListItems { get; set; }

        public DbSet<ListUser> ListUsers { get; set; }

        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
        {
        }
    }
}
