using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// created by Garrett Chepil
// class to connect the the database and access the SupplierContacts table
namespace TravelExperts
{
    public static class SupplierContactsDB
    {
        // gets info from database and puts in list
        public static List<SupplierContacts> listSuppliers()
        {
            List<SupplierContacts> supplier = new List<SupplierContacts>();

            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string selectStatement = "SELECT * FROM SupplierContacts ORDER BY SupConCompany";
        
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

            // use a try catch to attampt to get the data
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                // reads the data into the object
                while (reader.Read())
                {
                    SupplierContacts s = new SupplierContacts();

                    s.SupplierContactId = Convert.ToInt32(reader["SupplierContactId"]);
                    s.SupConFirstName = reader["SupConFirstName"].ToString();
                    s.SupConLastName = reader["SupConLastName"].ToString();
                    s.SupConCompany = reader["SupConCompany"].ToString();
                    s.SupConAddress = reader["SupConAddress"].ToString();
                    s.SupConCity = reader["SupConCity"].ToString();
                    s.SupConProv = reader["SupConProv"].ToString();
                    s.SupConPostal = reader["SupConPostal"].ToString();
                    s.SupConCountry = reader["SupConCountry"].ToString();
                    s.SupConBusPhone = reader["SupConBusPhone"].ToString();
                    s.SupConFax = reader["SupConFax"].ToString();
                    s.SupConEmail = reader["SupConEmail"].ToString();
                    s.SupConURL = reader["SupConURL"].ToString();
                    s.AffiliationId = reader["AffiliationId"].ToString();
                    s.SupplierId = Convert.ToInt32(reader["SupplierID"]);
                    
                    supplier.Add(s);
                }
                reader.Close();
                return supplier;
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

        // method to return a list based on a filter from the db
        public static List<SupplierContacts> listSuppliersByFilter(string field, string filter)
        {
            List<SupplierContacts> supplier = new List<SupplierContacts>();

            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string selectStatement = "SELECT * FROM SupplierContacts WHERE " + field + " LIKE @filter ORDER BY SupConCompany";

            // creates the sql command and parameters
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@filter", "%" + filter + "%");

            // use a try catch to attampt to get the data
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                // reads the data into the object
                while (reader.Read())
                {
                    SupplierContacts s = new SupplierContacts();

                    s.SupplierContactId = Convert.ToInt32(reader["SupplierContactId"]);
                    s.SupConFirstName = reader["SupConFirstName"].ToString();
                    s.SupConLastName = reader["SupConLastName"].ToString();
                    s.SupConCompany = reader["SupConCompany"].ToString();
                    s.SupConAddress = reader["SupConAddress"].ToString();
                    s.SupConCity = reader["SupConCity"].ToString();
                    s.SupConProv = reader["SupConProv"].ToString();
                    s.SupConPostal = reader["SupConPostal"].ToString();
                    s.SupConCountry = reader["SupConCountry"].ToString();
                    s.SupConBusPhone = reader["SupConBusPhone"].ToString();
                    s.SupConFax = reader["SupConFax"].ToString();
                    s.SupConEmail = reader["SupConEmail"].ToString();
                    s.SupConURL = reader["SupConURL"].ToString();
                    s.AffiliationId = reader["AffiliationId"].ToString();
                    s.SupplierId = Convert.ToInt32(reader["SupplierID"]);

                    supplier.Add(s);
                }
                reader.Close();
                return supplier;
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


        // gets suppliers from DB
        public static SupplierContacts GetSupplier(int SupplierContactId)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string selectStatement = "SELECT * FROM SupplierContacts WHERE SupplierContactId = @SupplierContactId";

            // creates the sql command and parameters
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@SupplierContactId", SupplierContactId);

            // use a try catch to attampt to get the data
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                // reads the data into the object
                if (reader.Read())
                {
                    SupplierContacts s = new SupplierContacts();
                    s.SupplierContactId = Convert.ToInt32(reader["SupplierContactId"]);
                    s.SupConFirstName = reader["SupConFirstName"].ToString();
                    s.SupConLastName = reader["SupConLastName"].ToString();
                    s.SupConCompany = reader["SupConCompany"].ToString();
                    s.SupConAddress = reader["SupConAddress"].ToString();
                    s.SupConCity = reader["SupConCity"].ToString();
                    s.SupConProv = reader["SupConProv"].ToString();
                    s.SupConPostal = reader["SupConPostal"].ToString();
                    s.SupConCountry = reader["SupConCountry"].ToString();
                    s.SupConBusPhone = reader["SupConBusPhone"].ToString();
                    s.SupConFax = reader["SupConFax"].ToString();
                    s.SupConEmail = reader["SupConEmail"].ToString();
                    s.SupConURL = reader["SupConURL"].ToString();
                    s.AffiliationId = reader["AffiliationId"].ToString();

                    s.SupplierId = Convert.ToInt32(reader["SupplierID"]);

                    return s;
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


        public static SupplierContacts GetSupplierbySupID(int Supplierid)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string selectStatement = "SELECT * FROM SupplierContacts WHERE SupplierId = @SupplierId";

            // creates the sql command and parameters
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@SupplierId", Supplierid);

            // use a try catch to attampt to get the data
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                // reads the data into the object
                if (reader.Read())
                {
                    SupplierContacts s = new SupplierContacts();
                    s.SupplierContactId = Convert.ToInt32(reader["SupplierContactId"]);
                    s.SupConFirstName = reader["SupConFirstName"].ToString();
                    s.SupConLastName = reader["SupConLastName"].ToString();
                    s.SupConCompany = reader["SupConCompany"].ToString();
                    s.SupConAddress = reader["SupConAddress"].ToString();
                    s.SupConCity = reader["SupConCity"].ToString();
                    s.SupConProv = reader["SupConProv"].ToString();
                    s.SupConPostal = reader["SupConPostal"].ToString();
                    s.SupConCountry = reader["SupConCountry"].ToString();
                    s.SupConBusPhone = reader["SupConBusPhone"].ToString();
                    s.SupConFax = reader["SupConFax"].ToString();
                    s.SupConEmail = reader["SupConEmail"].ToString();
                    s.SupConURL = reader["SupConURL"].ToString();
                    s.AffiliationId = reader["AffiliationId"].ToString();

                    s.SupplierId = Convert.ToInt32(reader["SupplierID"]);

                    return s;
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
        // method to add data to the db
        public static void AddSupp(SupplierContacts sup)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string insertStatement = "INSERT INTO SupplierContacts VALUES " +
                "(@SupplierContactId, @SupConFirstName, @SupConLastName, @SupConCompany, @SupConAddress, @SupConCity, @SupConProv, @SupConPostal, " +
                "@SupConCountry, @SupConBusPhone, @SupConFax, @SupConEmail, @SupConURL, NULL, @SupplierId)";

            // creates the sql command and parameters
            SqlCommand command = new SqlCommand(insertStatement, connection);
            command.Parameters.AddWithValue("@SupplierContactId", sup.SupplierContactId);
            command.Parameters.AddWithValue("@SupConFirstName", sup.SupConFirstName);
            command.Parameters.AddWithValue("@SupConLastName", sup.SupConLastName);
            command.Parameters.AddWithValue("@SupConCompany", sup.SupConCompany);
            command.Parameters.AddWithValue("@SupConAddress", sup.SupConAddress);
            command.Parameters.AddWithValue("@SupConCity", sup.SupConCity);
            command.Parameters.AddWithValue("@SupConProv", sup.SupConProv);
            command.Parameters.AddWithValue("@SupConPostal", sup.SupConPostal);
            command.Parameters.AddWithValue("@SupConCountry", sup.SupConCountry);
            command.Parameters.AddWithValue("@SupConBusPhone", sup.SupConBusPhone);
            command.Parameters.AddWithValue("@SupConFax", sup.SupConFax);
            command.Parameters.AddWithValue("@SupConEmail", sup.SupConEmail);
            command.Parameters.AddWithValue("@SupConURL", sup.SupConURL);
            //command.Parameters.AddWithValue("@AffiliationId", sup.AffiliationId);
            command.Parameters.AddWithValue("@SupplierId", sup.SupplierId);

            // use a try catch to attampt to add the data
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
        public static bool DeleteSup(SupplierContacts sup)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string deleteStatement = "DELETE FROM SupplierContacts WHERE SupplierContactId = @SupplierContactId";

            // creates the sql command and parameters
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@SupplierContactId", sup.SupplierContactId);

            // use a try catch to attampt to delete the data
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


        //method to update the supplier data to the db
        public static bool UpdateSupplier(SupplierContacts oldSup, SupplierContacts newSup)
        {
            // set up connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string updateStatement = "UPDATE SupplierContacts SET " +
                "SupplierContactId = @newSupplierContactId, SupConFirstName = @newSupConFirstName, SupConLastName = @newSupConLastName, " +
                "SupConCompany = @newSupConCompany, SupConAddress = @newSupConAddress, SupConCity = @newSupConCity, SupConProv = @newSupConProv, " +
                "SupConPostal = @newSupConPostal, SupConCountry = @newSupConCountry, SupConBusPhone = @newSupConBusPhone, SupConFax = @newSupConFax, " +
                "SupConEmail = @newSupConEmail, SupConURL = @newSupConURL" +
                " WHERE SupplierContactId = @oldSupplierContactId";

            // creates the sql command and parameters
            SqlCommand command = new SqlCommand(updateStatement, connection);
            command.Parameters.AddWithValue("@newSupplierContactId", newSup.SupplierContactId);
            command.Parameters.AddWithValue("@newSupConFirstName", newSup.SupConFirstName);
            command.Parameters.AddWithValue("@newSupConLastName", newSup.SupConLastName);
            command.Parameters.AddWithValue("@newSupConCompany", newSup.SupConCompany);
            command.Parameters.AddWithValue("@newSupConAddress", newSup.SupConAddress);
            command.Parameters.AddWithValue("@newSupConCity", newSup.SupConCity);
            command.Parameters.AddWithValue("@newSupConProv", newSup.SupConProv);
            command.Parameters.AddWithValue("@newSupConPostal", newSup.SupConPostal);
            command.Parameters.AddWithValue("@newSupConCountry", newSup.SupConCountry);
            command.Parameters.AddWithValue("@newSupConBusPhone", newSup.SupConBusPhone);
            command.Parameters.AddWithValue("@newSupConFax", newSup.SupConFax);
            command.Parameters.AddWithValue("@newSupConEmail", newSup.SupConEmail);
            command.Parameters.AddWithValue("@newSupConURL", newSup.SupConURL);
            command.Parameters.AddWithValue("@newAffiliationId", newSup.AffiliationId);
            command.Parameters.AddWithValue("@newSupplierId", newSup.SupplierId);
            command.Parameters.AddWithValue("@oldSupplierContactId", oldSup.SupplierContactId);

            // use a try catch to attampt to update the data
            try
            {
                connection.Open();
                int count = command.ExecuteNonQuery();
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

            string select = "SELECT MAX(SupplierContactId) AS ID FROM SupplierContacts";

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
            catch(SqlException ex)
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
