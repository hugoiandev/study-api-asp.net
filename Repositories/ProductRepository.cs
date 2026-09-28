using study_api.Entities;
using study_api.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace study_api.Repository
{
    public class ProductRepository : IProductRepository
    {

        public ProductRepository() { }

        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Product> Create(Product product)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            const string sql = """
                INSERT INTO dbo.Products (Name, Color, Price, Stock)
                OUTPUT INSERTED.Id,
                       INSERTED.Name,
                       INSERTED.Color,
                       INSERTED.Price,
                       INSERTED.Stock
                VALUES (@Name, @Color, @Price, @Stock)
                """;

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@Name", SqlDbType.Text).Value = product.Name;
            command.Parameters.Add("@Color", SqlDbType.Text).Value = product.Color;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            command.Parameters.Add("@Stock", SqlDbType.Int).Value = product.Stock;

            await using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                throw new Exception("Failed to create the product! Try again later.");
            }

            return new Product
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Color = reader.GetString("Color"),
                Price = reader.GetDecimal("Price"),
                Stock = reader.GetInt32("Stock")

            };

        }



        public async Task DeleteById(int Id)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            const string sql = """
                DELETE FROM dbo.Products WHERE Id = @Id
                """;

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            var products = new List<Product>();

            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            const string sql = """
                SELECT * FROM dbo.Products
                """;

            await using var command = new SqlCommand(sql, connection);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Color = reader.GetString("Color"),
                    Price = reader.GetDecimal("Price"),
                    Stock = reader.GetInt32("Stock")
                    
                });
            }

            return products;

        }

        public async Task<Product?> GetById(int Id)
        {

            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            const string sql = """
                SELECT * FROM dbo.Products WHERE Id = @Id
                """;

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

            if (reader.Read()) {

                return new Product
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Color = reader.GetString("Color"),
                    Price = reader.GetDecimal("Price"),
                    Stock = reader.GetInt32("Stock")

                };
            }

            return null;
        }

        public async Task<Product> UpdateById(Product product)
        {

            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            const string findSql = """
                SELECT id FROM dbo.Products WHERE Id = @Id
                """;

            await using var findCommand = new SqlCommand(findSql, connection);

            findCommand.Parameters.Add("@Id", SqlDbType.Int).Value = product.Id;

            await using var findReader = await findCommand.ExecuteReaderAsync(CommandBehavior.SingleRow);

            if (!findReader.Read()) {
                throw new Exception("Product not found!");
            }

            const string updateSql = """"
                    UPDATE dbo.Products
                    SET Name = @Name, Color = @Color, Price = @Price, Stock = @Stock
                    OUTPUT INSERTED.Id,
                           INSERTED.Name,
                           INSERTED.Color,
                           INSERTED.Price,
                           INSERTED.Stock
                    WHERE Id = @Id
                """";

            await using var updateCommand = new SqlCommand(updateSql, connection);

            updateCommand.Parameters.Add("@Id", SqlDbType.Int).Value = product.Id;
            updateCommand.Parameters.Add("@Name", SqlDbType.Text).Value = product.Name;
            updateCommand.Parameters.Add("@Color", SqlDbType.Text).Value = product.Color;
            updateCommand.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            updateCommand.Parameters.Add("@Stock", SqlDbType.Int).Value = product.Stock;

            await using var updateReader = await updateCommand.ExecuteReaderAsync();

            if (!updateReader.Read()) {
                throw new Exception("Failed to update the product! Try again later.");
            }

            return new Product
            {
                Id = updateReader.GetInt32("Id"),
                Name = updateReader.GetString("Name"),
                Color = updateReader.GetString("Color"),
                Price = updateReader.GetDecimal("Price"),
                Stock = updateReader.GetInt32("Stock")
            };
        }
    }
}
