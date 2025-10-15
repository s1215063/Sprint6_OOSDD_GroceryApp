﻿using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Grocery.Core.Data.Helpers;
using Microsoft.Data.Sqlite;

namespace Grocery.Core.Data.Repositories
{
    public class ProductRepository : DatabaseConnection, IProductRepository
    {
        private readonly List<Product> products = [];
        public ProductRepository()
        {
            CreateTable(@"CREATE TABLE IF NOT EXISTS Product (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Name] NVARCHAR(100) NOT NULL,
                            [Stock] INTEGER NOT NULL,
                            [ShelfLife] DATE,
                            [Price] DECIMAL(5,2) NOT NULL)");
            List<string> insertQueries = [@"INSERT OR IGNORE INTO Product(Id, Name, Stock, ShelfLife, Price) VALUES(1, 'Melk', 300, '2025-09-25', 0.95)",
                                          @"INSERT OR IGNORE INTO Product(Id, Name, Stock, ShelfLife, Price) VALUES(2, 'Kaas', 100, '2025-09-30', 7.98)",
                                          @"INSERT OR IGNORE INTO Product(Id, Name, Stock, ShelfLife, Price) VALUES(3, 'Brood', 400, '2025-09-12', 2.19)",
                                          @"INSERT OR IGNORE INTO Product(Id, Name, Stock, ShelfLife, Price) VALUES(4, 'Cornflakes', 0, '2025-12-31', 1.48)"];
            InsertMultipleWithTransaction(insertQueries);
            GetAll();
        }
        public List<Product> GetAll()
        {
            products.Clear();
            string selectQuery = "SELECT Id, Name, Stock, ShelfLife, Price FROM Product";
            OpenConnection();
            using (SqliteCommand command = new(selectQuery, Connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int stock = reader.GetInt32(2);
                    DateOnly shelfLife = reader.IsDBNull(3) ? default : DateOnly.FromDateTime(reader.GetDateTime(3));
                    Decimal price = reader.GetDecimal(4);
                    products.Add(new(id, name, stock, shelfLife, price));
                }
            }
            CloseConnection();
            return products;
        }

        public Product? Get(int id)
        {
            string selectQuery = $"SELECT Id, Name, Stock, ShelfLife, Price FROM Product WHERE Id = {id}";
            Product? listItem = null;
            OpenConnection();
            using (SqliteCommand command = new(selectQuery, Connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int productId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int stock = reader.GetInt32(2);
                    DateOnly shelfLife = reader.IsDBNull(3) ? default : DateOnly.FromDateTime(reader.GetDateTime(3));
                    Decimal price = reader.GetDecimal(4);
                    listItem = new(productId, name, stock, shelfLife, price);
                }
            }
            CloseConnection();
            return listItem;
        }

        public Product Add(Product item)
        {
            int recordsAffected;
            string insertQuery = $"INSERT INTO Product(Name, Stock, ShelfLife, Price) VALUES(@Name, @Stock, @ShelfLife, @Price) Returning RowId";
            OpenConnection();
            using (SqliteCommand command = new(insertQuery, Connection))
            {
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Stock", item.Stock);
                command.Parameters.AddWithValue("@ShelfLife", item.ShelfLife);
                command.Parameters.AddWithValue("@Price", item.Price);
                
                //recordsAffected = command.ExecuteNonQuery();
                item.Id = Convert.ToInt32(command.ExecuteScalar());
            }
            CloseConnection();
            return item;
        }

        public Product? Delete(Product item)
        {
            string deleteQuery = $"DELETE FROM Product WHERE Id = {item.Id}";
            OpenConnection();
            Connection.ExecuteNonQuery(deleteQuery);
            CloseConnection();
            return item;
        }

        public Product? Update(Product item)
        {
            int recordsAffected;
            string updateQuery = $"UPDATE Product SET Name = @Name, Stock = @Stock, ShelfLife = @ShelfLife, Price = @Price WHERE Id = {item.Id}";
            OpenConnection();
            using (SqliteCommand command = new(updateQuery, Connection))
            {
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Stock", item.Stock);
                command.Parameters.AddWithValue("@ShelfLife", item.ShelfLife);
                command.Parameters.AddWithValue("@Price", item.Price);
                recordsAffected = command.ExecuteNonQuery();
            }
            CloseConnection();
            return item;
        }
    }
}
