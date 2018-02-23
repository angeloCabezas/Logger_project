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
            try
            {
                JobLogger.getConfigurationApp();
                JobLogger.LogMessage("Este es la prueba 1002 mensaje de prueba", TypeMessage.Message);
                JobLogger.LogMessage("Este es la prueba 1002 mensaje de prueba", TypeMessage.Warning);
                JobLogger.LogMessage("Este es la prueba 1002 mensaje de prueba", TypeMessage.Error);
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
