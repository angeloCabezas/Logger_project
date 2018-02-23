using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JobLoggerProject.FileManager
{
    public class LogFileManager
    {

        private const string nameConfigDirectory = "LogFileDirectory";

        public static string getDirectory(string nameConfig)
        {
            string directory = System.Configuration.ConfigurationManager.AppSettings[nameConfig];

            if (string.IsNullOrEmpty(directory)) { return ""; }

            return directory;
        }

        public static bool WriteMesageToTextFile(string fileName, string textLog)
        {
            string directory = getDirectory(nameConfigDirectory);

            if(string.IsNullOrEmpty(directory)) { return false; }

            if (Directory.Exists(directory))
            {
                using (StreamWriter file = new StreamWriter(directory + fileName, true))
                {
                    file.WriteLine(textLog);
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
