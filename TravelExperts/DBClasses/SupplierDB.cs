using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//LEVI DID THIS
namespace TravelExperts
{
    public static class SupplierDB
    {
        // gets info from database and puts in list
        public static List<Supplier> ListSupplier()
        {
            List<Supplier> supplier = new List<Supplier>();
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT SupplierID, SupName "
                                   + "FROM Suppliers "
                                   + "ORDER BY SupName";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Supplier s = new Supplier();
                    s.SupplierId = Convert.ToInt32(reader["SupplierID"]);
                    s.SupName = reader["SupName"].ToString();
                    supplier.Add(s);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return supplier;
        }
        // gets suppliers from DB
        public static Supplier GetSupplier(int supplierId)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement
                = "SELECT SupplierId, SupName "
                + "FROM Suppliers "
                + "WHERE SupplierId = @SupplierId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@SupplierId", supplierId);

            try
            {
                connection.Open();
                SqlDataReader Reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (Reader.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierId = (int)Reader["SupplierId"];
                    supplier.SupName = Reader["SupName"].ToString();

                    return supplier;
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

        public static void AddSupp(Supplier sup)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string insertStatement = "INSERT INTO Suppliers (SupplierId, SupName) VALUES (@SupplierId, @SupName)";
            SqlCommand command = new SqlCommand(insertStatement, connection);
            command.Parameters.AddWithValue("@SupplierId", sup.SupplierId);
            command.Parameters.AddWithValue("@SupName", sup.SupName);
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
        public static bool DeleteSup(Supplier sup)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string deleteStatement = "DELETE FROM Suppliers WHERE SupplierId = @SupplierId";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@SupplierId", sup.SupplierId);
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
        public static bool UpdateProdSup(Supplier oldSup, Supplier newSup)
        {
            // set up connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string updateStatement = "UPDATE Suppliers SET " +
                "SupplierId = @newSupplierId, SupName = @newSupName WHERE SupplierId = @oldSupplierId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            // updateCommand.Parameters.AddWithValue("@newProductSupplierId", newProdSup.productsupplierid);
            updateCommand.Parameters.AddWithValue("@newSupplierId", newSup.SupplierId);
            updateCommand.Parameters.AddWithValue("@newSupName", newSup.SupName);
            updateCommand.Parameters.AddWithValue("@oldSupplierId", oldSup.SupplierId);

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

        public static int GetNextAvailableID()
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string select = "SELECT MAX(SupplierId) AS ID FROM Suppliers";

            SqlCommand command = new SqlCommand(select, connection);

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                // reads the data into the object
                reader.Read();
                int nextid = Convert.ToInt32(reader["ID"]);
                nextid++;
                return nextid;
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
