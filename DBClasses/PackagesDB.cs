using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    // Team 4 - Mehmet Demirci

    public static class PackagesDB
    {
        public static List<Package> GetAll()
        {
            List<Package> packages = new List<Package>();

            SqlConnection conn = TravelExpertsDB.GetConnection();
            string select = "SELECT PackageId, PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission FROM Packages";
            SqlCommand selectCommand = new SqlCommand(select, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    Package package = new Package();
                    package.PackageId = (int)reader["PackageId"];
                    package.PkgName = reader["PkgName"].ToString();
                    package.PkgStartDate = (DateTime)reader["PkgStartDate"];
                    package.PkgEndDate = (DateTime)reader["PkgEndDate"];
                    package.PkgDesc = reader["PkgDesc"].ToString();
                    package.PkgBasePrice = (decimal)reader["PkgBasePrice"];
                    package.PkgAgencyCommission = (decimal)reader["PkgAgencyCommission"];
                    packages.Add(package);
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

            return packages;
        }

        // method to add data to the db
        public static int AddPackage(Package package)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string insertStatement = "INSERT INTO Packages VALUES (@PkgName, @PkgStartDate, @PkgEndDate, @PkgDesc, @PkgBasePrice, @PkgAgencyCommission)";

            // creates the sql command and parameters
            SqlCommand command = new SqlCommand(insertStatement, connection);
            command.Parameters.AddWithValue("@PkgName", package.PkgName);
            command.Parameters.AddWithValue("@PkgStartDate", package.PkgStartDate);
            command.Parameters.AddWithValue("@PkgEndDate", package.PkgEndDate);
            command.Parameters.AddWithValue("@PkgDesc", package.PkgDesc);
            command.Parameters.AddWithValue("@PkgBasePrice", package.PkgBasePrice);
            command.Parameters.AddWithValue("@PkgAgencyCommission", package.PkgAgencyCommission);
            
            // use a try catch to attampt to add the data
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                string selectStatement = "SELECT IDENT_CURRENT('Packages') FROM Packages";
                SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
                int PackageId = Convert.ToInt32(selectCommand.ExecuteScalar());
                return PackageId;

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
        public static bool DeletePackage(Package package)
        {
            // set up the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string deleteStatement = "DELETE FROM Packages WHERE PackageId = @PackageId";

            // creates the sql command and parameters
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@PackageId", package.PackageId);

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
        public static bool UpdatePackage(Package oldPack, Package newPack)
        {
            // set up connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // sql statement
            string updateStatement = "UPDATE Packages SET " +
                "PkgName = @newPkgName, PkgStartDate = @newPkgStartDate, PkgEndDate = @newPkgEndDate, " +
                "PkgDesc = @newPkgDesc, PkgBasePrice = @newPkgBasePrice, PkgAgencyCommission = @newPkgAgencyCommission" +
                " WHERE PackageId = @oldPackageId";

            // creates the sql command and parameters
            SqlCommand command = new SqlCommand(updateStatement, connection);
            command.Parameters.AddWithValue("@newPkgName", newPack.PkgName);
            command.Parameters.AddWithValue("@newPkgStartDate", newPack.PkgStartDate);
            command.Parameters.AddWithValue("@newPkgEndDate", newPack.PkgEndDate);
            command.Parameters.AddWithValue("@newPkgDesc", newPack.PkgDesc);
            command.Parameters.AddWithValue("@newPkgBasePrice", newPack.PkgBasePrice);
            command.Parameters.AddWithValue("@newPkgAgencyCommission", newPack.PkgAgencyCommission);
            command.Parameters.AddWithValue("@oldPackageId", oldPack.PackageId);

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

    }
}
