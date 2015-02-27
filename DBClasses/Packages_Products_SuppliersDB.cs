using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// created by Garrett
namespace TravelExperts
{
    public static class Packages_Products_SuppliersDB
    {
        // class to get a product_supplier record
        public static Packages_Products_Suppliers GetPackProdSup(int packageId, int productSupplierId)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string selectStatement = "SELECT * FROM Packages_Products_Suppliers WHERE ProductSupplierId = @ProductSupplierId AND PackageId = @PackageId";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            command.Parameters.AddWithValue("@ProductSupplierId", productSupplierId);
            command.Parameters.AddWithValue("@PackageId", packageId);

            // use a try catch to attampt to get the data
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Packages_Products_Suppliers packprodSup = new Packages_Products_Suppliers();
                    packprodSup.PackageId = (int)reader["PackageId"];
                    packprodSup.ProductSupplierId = (int)reader["ProductSupplierId"];


                    return packprodSup;
                }
                else
                    return null;
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
        // class to get a product_supplier record
        public static List<Packages_Products_Suppliers> GetList(int packageId)
        {
            List<Packages_Products_Suppliers> packages = new List<Packages_Products_Suppliers>();
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string selectStatement = "SELECT * FROM Packages_Products_Suppliers pps join Products_Suppliers ps on pps.ProductSupplierId = ps.ProductSupplierId join Products p on ps.ProductId = p.ProductId join Suppliers s on ps.SupplierId = s.SupplierId  WHERE PackageId = @PackageId";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            command.Parameters.AddWithValue("@PackageId", packageId);

            // use a try catch to attampt to get the data
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Packages_Products_Suppliers packprodSup = new Packages_Products_Suppliers();
                    packprodSup.PackageId = (int)reader["PackageId"];
                    packprodSup.ProductSupplierId = (int)reader["ProductSupplierId"];
                    packprodSup.ProdName = reader["ProdName"].ToString();
                    packprodSup.SupName = reader["SupName"].ToString();
                    packages.Add(packprodSup);



                }
                return packages;
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

        // class to add a product_supplier to the database
        public static void AddPackProdSupp(Packages_Products_Suppliers packProdSup)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string insertStatement = "INSERT INTO Packages_Products_Suppliers (PackageId, ProductSupplierId) VALUES (@PackageId, @ProductSupplierId)";
            SqlCommand command = new SqlCommand(insertStatement, connection);
            command.Parameters.AddWithValue("@PackageId", packProdSup.PackageId);
            command.Parameters.AddWithValue("@ProductSupplierId", packProdSup.ProductSupplierId);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                
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

        // class to delete a prodoctsupplier from the database
        public static bool DeletePackProdSup(Packages_Products_Suppliers packProdsup)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string deleteStatement = "DELETE FROM Packages_Products_Suppliers WHERE ProductSupplierId = @ProductSupplierId AND PackageId = @PackageId";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@ProductSupplierId", packProdsup.ProductSupplierId);
            deleteCommand.Parameters.AddWithValue("@PackageId", packProdsup.PackageId);
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

        // class to update a producxt supplier to the database
        public static bool UpdatePackProdSup(Packages_Products_Suppliers oldPackProdSup, Packages_Products_Suppliers newPackProdSup)
        {
            // set up connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string updateStatement = "UPDATE Products_Suppliers SET " +
                "PackageId = @newPackageId, ProductSupplierId = @newProductSupplierId WHERE PackageId = @oldPackageId AND ProductSupplierId = @oldProductSupplierId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            // updateCommand.Parameters.AddWithValue("@newProductSupplierId", newProdSup.productsupplierid);
            updateCommand.Parameters.AddWithValue("@newPackageId", newPackProdSup.PackageId);
            updateCommand.Parameters.AddWithValue("@newProductSupplierId", newPackProdSup.ProductSupplierId);
            updateCommand.Parameters.AddWithValue("@oldPackageId", oldPackProdSup.PackageId);
            updateCommand.Parameters.AddWithValue("@oldProductSupplierId", oldPackProdSup.ProductSupplierId);

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

    }
}
