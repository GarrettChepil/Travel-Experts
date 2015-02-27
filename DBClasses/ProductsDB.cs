using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    public static class ProductsDB
    {
        // Team 4 - Mehmet Demirci

        public static List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            SqlConnection conn = TravelExpertsDB.GetConnection();
            string select = "SELECT ProductId, ProdName FROM Products";
            SqlCommand selectCommand = new SqlCommand(select, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.ProductId = (int)reader["ProductId"];
                    product.ProdName = reader["ProdName"].ToString();
                    products.Add(product);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return products;
        }

        public static Product GetProduct(int productID)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement
                = "SELECT ProductId, ProdName "
                + "FROM Products "
                + "WHERE ProductId = @ProductID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ProductID", productID);

            try
            {
                connection.Open();
                SqlDataReader productReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (productReader.Read())
                {
                    Product product = new Product();
                    product.ProductId = (int) productReader["ProductId"];
                    product.ProdName = productReader["ProdName"].ToString();
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int AddProduct(Product product)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string insertStatement =
                "INSERT Products " + "(ProdName) " + "VALUES (@ProdName)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@ProdName", product.ProdName);
            
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Products') FROM Products";
                SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
                return Convert.ToInt32(selectCommand.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateProduct(Product oldProduct, Product newProduct)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string updateStatement =
                "UPDATE Products SET " +
                "ProdName = @NewProdName " +
                "WHERE ProductId = @OldProductId";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@NewProdName", newProduct.ProdName);
            updateCommand.Parameters.AddWithValue("@OldProductId", oldProduct.ProductId);
            
            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteProduct(Product product)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string deleteStatement =
                "DELETE FROM Products " +
                "WHERE ProductId = @ProductId " +
                "AND ProdName = @ProdName";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@ProductId", product.ProductId);
            deleteCommand.Parameters.AddWithValue("@ProdName", product.ProdName);
            
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
