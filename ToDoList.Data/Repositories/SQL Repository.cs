using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories
{
    public class SqlRepository : IToDoListRepository
    {
        public void Add(ListItem item)
        {
            using (var connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ToDoListDB;Trusted_Connection=True;"))
            {
                var sql = $"INSERT INTO ListItems (Value, Completed) VALUES ('{item.Value}', '{item.Completed}')";

                var command = new SqlCommand(sql, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(ListItem item)
        {
            using (var connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ToDoListDB;Trusted_Connection=True;"))
            {
                var sql = $"UPDATE ListItems SET Value = {item.Value}, Completed = '{item.Completed}' WHERE Id = {item.Id}";

                var command = new SqlCommand(sql, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public async Task<RepoResultWrapper<ListItem>> GetByIdAsync(int id)
        {
            await using (var connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ToDoListDB;Trusted_Connection=True;"))
            {
                var sql = $"SELECT Id, Value, Completed FROM ListItems WHERE Id = {id}";

                var command = new SqlCommand(sql, connection);

                connection.Open();

                try
                {
                    var reader = await command.ExecuteReaderAsync();
                    reader.Read();

                    var listItem = new ListItem
                    {
                        Id = (int)reader[0],
                        Value = (string)reader[1],
                        Completed = (bool)reader[2]
                    };

                    return RepoResultWrapper<ListItem>.Success(listItem);
                }
                catch (InvalidOperationException)
                {
                    return RepoResultWrapper<ListItem>.NotFound();
                }
            }
        }

        public async Task<RepoResultWrapper<List<ListItem>>> GetAllAsync()
        {
            var listItems = new List<ListItem>();

            await using (var connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ToDoListDB;Trusted_Connection=True;"))
            {
                var sql = "SELECT * FROM ListItems";

                var command = new SqlCommand(sql, connection);

                connection.Open();

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        listItems.Add(new ListItem
                        {
                            Id = (int)reader[0],
                            Value = (string)reader[1],
                            Completed = (bool)reader[2]
                        });
                    }
                }
            }
            return RepoResultWrapper<List<ListItem>>.Success(listItems);
        }

        public async Task SaveChangesAsync()
        {
        }
    }
}
