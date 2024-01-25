using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Repository
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Product";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"])
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Product (Name, Description, Price) VALUES (@Name, @Description, @Price)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product updatedProduct)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Product SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", updatedProduct.Id);
                    command.Parameters.AddWithValue("@Name", updatedProduct.Name);
                    command.Parameters.AddWithValue("@Description", updatedProduct.Description);
                    command.Parameters.AddWithValue("@Price", updatedProduct.Price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Product WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Product WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Product product = new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"])
                                };

                                return product;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while fetching product by ID.", ex);
            }

            return null; 
        }

    }

}