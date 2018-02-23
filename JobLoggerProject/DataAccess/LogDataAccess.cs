using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace JobLoggerProject.DataAccess
{
    public class LogDataAccess
    {
        private const string nameConectionConfig = "ConnectionString";

        public string getConection(string nameConection)
        {
            string conectionString = System.Configuration.ConfigurationManager.AppSettings[nameConection];

            if (string.IsNullOrEmpty(conectionString)) { return ""; }

            return conectionString;
        }

        public int InsertTBL_LOG(string message,int typeMessage)
        {
            int resultado;

            string conectionString = getConection(nameConectionConfig);

            using (SqlConnection connection = new SqlConnection(conectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_InsertTBL_LOG", connection))
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@messageLog", System.Data.SqlDbType.VarChar).Value = message;
                    command.Parameters.Add("@IdTipoMensaje", System.Data.SqlDbType.Int).Value = (typeMessage);
                    resultado = command.ExecuteNonQuery();
                }
            }
            return resultado;
        }
    }
}
