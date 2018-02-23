using System;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLoggerProject
{
    public class JobLogger
    {
        private static bool _logToFile = false;
        private static bool _logToConsole = false;
        private static bool _logMessage = false;
        private static bool _logWarning = false;
        private static bool _logError = false;
        private static bool _logToDatabase = false;

        #region Init Configuration
        public static void SetTypeLogAllow(bool logToTextFile, bool logToConsole, bool logToDatabase)
        {

            _logToFile = logToTextFile;
            _logToConsole = logToConsole;
            _logToDatabase = logToDatabase;
        }

        public static void SetMessageAllow(bool allowError, bool allowMessage, bool allowWarning)
        {
            _logError = allowError;
            _logMessage = allowMessage;
            _logWarning = allowWarning;
        }

        public static bool getConfigurationApp()
        {
            try
            {
                string logToTextFile = ConfigurationManager.AppSettings["LogToTextFile"];
                string logToConsole = ConfigurationManager.AppSettings["LogToConsole"];
                string logToDatabase = ConfigurationManager.AppSettings["LogToDatabase"];
                string allowError = ConfigurationManager.AppSettings["AllowError"];
                string allowMessage = ConfigurationManager.AppSettings["AllowMessage"];
                string allowWarning = ConfigurationManager.AppSettings["AllowWarning"];

                if (logToTextFile == null || logToConsole == null || logToDatabase == null)
                {
                    throw new Exception("App.config is not configured correctly, Check LogToTextFile | LogToConsole | LogToDatabase");
                }

                if (allowError == null || allowMessage == null || allowWarning == null)
                {
                    throw new Exception("App.config is not configured correctly, Check AllowError | AllowMessage | AllowWarning");
                }

                SetTypeLogAllow(logToTextFile == "1" ? true : false, logToConsole == "1" ? true : false, logToDatabase == "1" ? true : false);
                SetMessageAllow(allowError == "1" ? true : false, allowMessage == "1" ? true : false, allowWarning == "1" ? true : false);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Validation
        public static string ValidateMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new Exception("Invalid message");
            }

            return message.Trim();
        }

        public static bool ValidateSetTypeLogAllow()
        {
            if ((!_logToConsole && !_logToFile && !_logToDatabase))
            {
                throw new Exception("Error, You must be specified type of Log");
            }
            return true;
        }

        public static bool ValidateSetMessageAllow()
        {
            if ((!_logError && !_logMessage && !_logWarning))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }
            return true;
        }

        public static bool ValidateTypeAllow(TypeMessage typeMessage)
        {
            if (_logError && TypeMessage.Error == typeMessage) return true;
            if (_logMessage && TypeMessage.Message == typeMessage) return true;
            if (_logWarning && TypeMessage.Warning == typeMessage) return true;
            return false;
        }
        #endregion

        #region Main Methods
        public static void LogMessage(string message, TypeMessage typeMessage)
        {
            try
            {
                message = ValidateMessage(message);

                ValidateSetTypeLogAllow();

                ValidateSetMessageAllow();

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveMessageInLogs
        public static int SaveMessageInBD(string message, TypeMessage typeMessage)
        {
            try
            {
                DataAccess.LogDataAccess dataAccess = new DataAccess.LogDataAccess();
                return dataAccess.InsertTBL_LOG(message, (int)typeMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SaveMessageInTextFile(string message, TypeMessage typeMessage)
        {
            try
            {
                string fileName = "LogFile" + DateTime.Today.ToString("ddMMyyyy") + ".txt";
                string textLog = GetFormatMessage(message, typeMessage);
                return FileManager.LogFileManager.WriteMesageToTextFile(fileName, textLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static bool SaveMessageInConsole(string message, TypeMessage typeMessage)
        {
            try
            {
                Console.ForegroundColor = typeMessage == TypeMessage.Error ? ConsoleColor.Red : typeMessage == TypeMessage.Warning ? ConsoleColor.Yellow : ConsoleColor.White;
                Console.WriteLine(GetFormatMessage(message,typeMessage));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion

        #region Formats
        public static string GetFormatMessage(string message, TypeMessage typeMessage)
        {
            return typeMessage + " " + DateTime.Now.ToString() + " : " + message;
        }
        #endregion
    }
}
