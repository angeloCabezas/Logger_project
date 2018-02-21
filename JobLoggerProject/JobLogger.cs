using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLoggerProject
{
    public class JobLogger
    {
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDatabase;

        #region Enum Logger
        public enum TypeMessage
        {
            Message = 1,
            Warning = 2,
            Error = 3
        }
        #endregion
        
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase,bool logMessage, bool logWarning, bool logError)
        {
            _logError   = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToDatabase = logToDatabase; 
            _logToFile  = logToFile;
            _logToConsole  = logToConsole;
        }

        
        public static void LogMessage(string message, TypeMessage typeMessage)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new Exception("Invalid message");
            }
            message.Trim();

            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {
                throw new Exception("Invalid configuration");
            }

            if ((!_logError && !_logMessage && !_logWarning))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            //Se ha abstraido los metodos de Validar el tipo de mensaje permitido,
            //grabar en base de datos, grabar en Archivo de Texto y mostrar en Consola
            //para efectuar las pruebas unitarias de los metodos por separado.
            if (ValidateTypeAllow(typeMessage))
            {
                if (_logToDatabase)
                {
                    SaveMessageInBD(message, typeMessage);
                }

                if (_logToFile)
                {
                    SaveMessageInTextFile(message, typeMessage);
                }

                if (_logToConsole)
                {
                    SaveMessageInConsole(message, typeMessage);
                }
            }
        }

        public static bool ValidateTypeAllow(TypeMessage typeMessage)
        {
            if (_logError && TypeMessage.Error == typeMessage) return true;
            if (_logMessage && TypeMessage.Message == typeMessage) return true;
            if (_logWarning && TypeMessage.Warning == typeMessage) return true;
            return false;
        }

        public static int SaveMessageInBD(string message, TypeMessage typeMessage)
        {
            try
            {
                int resultado = 0;
                string conectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

                if (string.IsNullOrEmpty(conectionString)) { return 0; }

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(conectionString))
                {
                    connection.Open();
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "usp_InsertTBL_LOG";
                    command.Parameters.Add("@messageLog", System.Data.SqlDbType.VarChar).Value = message;
                    command.Parameters.Add("@IdTipoMensaje", System.Data.SqlDbType.Int).Value = ((int)typeMessage);
                    resultado = command.ExecuteNonQuery();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                //Se deberia escribir un log de errores aquí. Puede fallar por no encontrar información en el config o por permisos para escribir.
                return 0;
            }
        }

        public static int SaveMessageInTextFile(string message,TypeMessage typeMessage)
        {
            try
            {
                string textLog = "";

                string filePath = System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"];
                string fileName = "LogFile" + DateTime.Today.ToString("ddMMyyyy") + ".txt";

                if (string.IsNullOrEmpty(filePath)) { return 0; }

                if (System.IO.File.Exists(filePath + fileName))
                {
                    textLog = System.IO.File.ReadAllText(filePath + fileName);
                }

                textLog += Environment.NewLine;
                textLog += typeMessage + " " + DateTime.Now.ToShortDateString() + " : "  +message;

                System.IO.File.WriteAllText(filePath + "LogFile" + DateTime.Today.ToString("ddMMyyyy") + ".txt", textLog);

                return 1;
            }
            catch (Exception ex)
            {
                //Se deberia escribir un log de errores aquí. Puede fallar por no encontrar información en el config o por permisos para escribir.
                return 0;
            }
            
        }

        public static int SaveMessageInConsole(string message, TypeMessage typeMessage)
        {
            try
            {
                Console.ForegroundColor = typeMessage == TypeMessage.Error ? ConsoleColor.Red : typeMessage == TypeMessage.Warning ? ConsoleColor.Yellow : ConsoleColor.White;
                Console.WriteLine(DateTime.Now.ToShortDateString() + message);
                return 1;
            }
            catch (Exception ex)
            {
                //Se deberia escribir un log de errores aquí. Puede fallar por no encontrar información en el config o por permisos para escribir.
                return 0;
            }
            
        }
    }
}
