using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

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

        public void Complete(ListItem item)
        {
            using (var connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ToDoListDB;Trusted_Connection=True;"))
            {
                var sql = $"UPDATE ListItems SET Completed = 'true' WHERE Id = {item.Id}";

                var command = new SqlCommand(sql, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ListItem GetById(int id)
        {
            ListItem listItem;

            using (var connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ToDoListDB;Trusted_Connection=True;"))
            {
                var sql = $"SELECT * FROM ListItems WHERE Id = {id}";

                var command = new SqlCommand(sql, connection);

                connection.Open();

                try
                {
                    var reader = command.ExecuteReader();
                    reader.Read();

                    listItem = new ListItem
                    {
                        Id = (int) reader[0],
                        Value = (string) reader[1],
                        Completed = (bool) reader[2]
                    };

                    return listItem;
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public List<ListItem> GetAll()
        {
            var listItems = new List<ListItem>();

            using (var connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ToDoListDB;Trusted_Connection=True;"))
            {
                var sql = "SELECT * FROM ListItems";

                var command = new SqlCommand(sql, connection);

                connection.Open();

                using (var reader = command.ExecuteReader())
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
            return listItems;
        }

        public void SaveChanges()
        {
        }
    }
}
