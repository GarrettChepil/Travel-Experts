using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// created by Garrett Chepil
// class to connect the the database and access the product_supplier table
namespace TravelExperts
{
    public static class Products_SuppliersDB
    {

        // class to get a product_supplier record
        public static Products_Suppliers GetProdSup(int productSupplierId)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId FROM Products_Suppliers WHERE ProductSupplierId = @ProductSupplierId";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            command.Parameters.AddWithValue("@ProductSupplierId", productSupplierId);

            // use a try catch to attampt to get the data
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Products_Suppliers prodSup = new Products_Suppliers();
                    prodSup.productsupplierid = (int)reader["ProductSupplierId"];
                    prodSup.productid = (int)reader["ProductId"];
                    prodSup.supplierid = (int)reader["SupplierId"];

                    return prodSup;
                }
                else
                    return null;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        // class to add a product_supplier to the database
        public static int AddProdSupp(Products_Suppliers prodSup)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string insertStatement = "INSERT INTO Products_Suppliers (ProductId, SupplierId) VALUES (@ProductId, @SupplierId)";
            SqlCommand command = new SqlCommand(insertStatement, connection);
            command.Parameters.AddWithValue("@ProductId", prodSup.productid);
            command.Parameters.AddWithValue("@SupplierId", prodSup.supplierid);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Products_Suppliers') FROM Products_Suppliers";
                SqlCommand selectCommand =
                    new SqlCommand(selectStatement, connection);
                int Products_SuppliersID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return Products_SuppliersID;
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
        public static bool DeleteProdSup(Products_Suppliers prodsup)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string deleteStatement = "DELETE FROM Products_Suppliers WHERE ProductSupplierId = @ProductSupplierId";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@ProductSupplierId", prodsup.productsupplierid);
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
        public static bool UpdateProdSup(Products_Suppliers oldProdSup, Products_Suppliers newProdSup)
        {
            // set up connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string updateStatement = "UPDATE Products_Suppliers SET " +
                "ProductId = @newProductId, SupplierId = @newSupplierId WHERE ProductSupplierId = @oldProductSupplierId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            // updateCommand.Parameters.AddWithValue("@newProductSupplierId", newProdSup.productsupplierid);
            updateCommand.Parameters.AddWithValue("@newProductId", newProdSup.productid);
            updateCommand.Parameters.AddWithValue("@newSupplierId", newProdSup.supplierid);
            updateCommand.Parameters.AddWithValue("@oldProductSupplierId", oldProdSup.productsupplierid);
            
            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }


        // class to return a list of all records in the table
        public static List<Products_Suppliers> GetAllProdSup()
        {
            List<Products_Suppliers> product_suppliers = new List<Products_Suppliers>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT * FROM Products_Suppliers";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                while(reader.Read())
                {
                    Products_Suppliers prodsup = new Products_Suppliers();
                    prodsup.productsupplierid = (int)reader["ProductSupplierId"];
                    prodsup.productid = (int)reader["ProductId"];
                    prodsup.supplierid = (int)reader["SupplierId"];
                    product_suppliers.Add(prodsup);
                }

                return product_suppliers;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        // class to return a list of all records in the table
        public static List<Products_Suppliers> GetAllProdSupOnID(int supplierID)
        {
            List<Products_Suppliers> product_suppliers = new List<Products_Suppliers>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT * FROM Products_Suppliers ps join Products p on ps.ProductId = p.ProductId  WHERE SupplierId = @SupplierId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@SupplierId", supplierID);

            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    Products_Suppliers prodsup = new Products_Suppliers();
                    prodsup.productsupplierid = (int)reader["ProductSupplierId"];
                    prodsup.productid = (int)reader["ProductId"];
                    prodsup.supplierid = (int)reader["SupplierId"];
                    prodsup.productName = reader["ProdName"].ToString();
                    product_suppliers.Add(prodsup);
                }

                return product_suppliers;
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

        // class to return a list of all records in the table based on productid
        public static List<Products_Suppliers> GetAllProdSupOnProdID(int productid)
        {
            List<Products_Suppliers> product_suppliers = new List<Products_Suppliers>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT * FROM Products_Suppliers ps join Suppliers s on ps.SupplierId = s.SupplierId  WHERE ProductId = @ProductId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ProductId", productid);

            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    Products_Suppliers prodsup = new Products_Suppliers();
                    prodsup.productsupplierid = (int)reader["ProductSupplierId"];
                    prodsup.productid = (int)reader["ProductId"];
                    prodsup.supplierid = (int)reader["SupplierId"];
                    prodsup.SupName = reader["SupName"].ToString();
                    product_suppliers.Add(prodsup);
                }

                return product_suppliers;
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

        public static List<Products_Suppliers> GetSuppliersByProductName(string productName)
        {
            List<Products_Suppliers> product_suppliers = new List<Products_Suppliers>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            //string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId FROM Products_Suppliers WHERE ProductSupplierId = @ProductSupplierId";
            string selectStatement = "SELECT ProdName, SupName " +
                    "FROM Products prd JOIN Products_Suppliers prdSpp ON prd.ProductId=prdSpp.ProductId " +
                    "JOIN Suppliers spp ON spp.SupplierId=prdSpp.SupplierId WHERE prd.ProdName=@ProductName";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            command.Parameters.AddWithValue("@ProductName", productName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Products_Suppliers prodSup = new Products_Suppliers();
                    prodSup.productName = reader["ProdName"].ToString();
                    prodSup.SupName = reader["SupName"].ToString();
                    product_suppliers.Add(prodSup);
                }
                return product_suppliers;
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

        public static List<Products_Suppliers> GetProductsBySupplierName(string supplierName)
        {
            List<Products_Suppliers> supplier_products = new List<Products_Suppliers>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT ProdName, SupName " +
                    "FROM Products prd JOIN Products_Suppliers prdSpp ON prd.ProductId=prdSpp.ProductId " +
                    "JOIN Suppliers spp ON spp.SupplierId=prdSpp.SupplierId WHERE spp.SupName=@SupplierName";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            command.Parameters.AddWithValue("@SupplierName", supplierName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Products_Suppliers prodSup = new Products_Suppliers();
                    prodSup.productName = reader["ProdName"].ToString();
                    prodSup.SupName = reader["SupName"].ToString();
                    supplier_products.Add(prodSup);
                }
                return supplier_products;
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

        public static List<Products_Suppliers> GetProductsByPackageName(string packageName)
        {
            List<Products_Suppliers> supplier_products = new List<Products_Suppliers>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT ProdName, SupName " +
                    "FROM Packages pk JOIN Packages_Products_Suppliers pps ON pk.PackageId=pps.PackageId " +
                    "JOIN Products_Suppliers ps ON pps.ProductSupplierId=ps.ProductSupplierId " +
                    "JOIN Products p ON ps.ProductId=p.ProductId " +
                    "JOIN Suppliers s ON ps.SupplierId=s.SupplierId " +
                    "WHERE pk.PkgName=@PackageName";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            command.Parameters.AddWithValue("@PackageName", packageName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Products_Suppliers prodSup = new Products_Suppliers();
                    prodSup.productName = reader["ProdName"].ToString();
                    prodSup.SupName = reader["SupName"].ToString();
                    supplier_products.Add(prodSup);
                }
                return supplier_products;
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
