using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    public static class TravelExpertsDB
    {
        public static SqlConnection GetConnection()
        {
            //string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=TravelExperts;Integrated Security=True";

            string connectionString =
                "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\APP_DATA\\TravelExperts.mdf;" +
                "Integrated Security=True";
           
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }


        public static void LoadDB()
        {
            SqlConnection conn = TravelExpertsDB.GetConnection();



            try
            {
                conn.Open();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }


}
