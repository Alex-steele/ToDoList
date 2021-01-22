using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Cosmos.Entities;

namespace ToDoList.Data.Cosmos
{
    public class ToDoListCosmosContext : DbContext
    {
        public DbSet<CosmosListItem> ListItems { get; set; }

        public ToDoListCosmosContext(DbContextOptions<ToDoListCosmosContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("ListItems");
            modelBuilder.Entity<CosmosListItem>().HasPartitionKey(x => x.UserId);
        }
    }
}
