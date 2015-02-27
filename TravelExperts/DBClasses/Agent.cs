using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    public static class Agent
    {
        public static bool AuthorizeAgent(string userName, string Password)
        {
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string select = "SELECT count(*) FROM Agents WHERE AgtFirstName ='" + userName + "' AND Password = '" + Password + "';";
            SqlCommand command = new SqlCommand(select, conn);


            try
            {
                conn.Open();
                int temp = Convert.ToInt32(command.ExecuteScalar().ToString());
                if (temp == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(SqlException ex)
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
