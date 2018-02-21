using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLoggerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            JobLogger Job = new JobLogger(false, true, false, true, true, true);
            
            try
            {
                JobLogger.LogMessage("Mensaje de Prueba Consola Warning",JobLogger.TypeMessage.Warning);
                JobLogger.LogMessage("Mensaje de Prueba Consola Message", JobLogger.TypeMessage.Message);
                JobLogger.LogMessage("Mensaje de Prueba Consola Error", JobLogger.TypeMessage.Error);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            
        }
    }
}
