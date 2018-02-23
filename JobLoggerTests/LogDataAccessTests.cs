using System;
using JobLoggerProject.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLoggerTests
{
    [TestClass]
    public class LogDataAccessTests
    {
        private LogDataAccess logDataAccess;

        [TestMethod]
        public void LogDataAccessInvalid_GetConnection()
        {
            logDataAccess = new LogDataAccess();

            string result = logDataAccess.getConection("XXXX");

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void LogDataAccessValid_GetConnection()
        {
            logDataAccess = new LogDataAccess();
            string exceptedValue = "";

            string result = logDataAccess.getConection("ConnectionString");

            Assert.AreNotEqual(exceptedValue, result);
        }

        [TestMethod]
        public void LogDataAccess_InsertTBL_LOG()
        {
            logDataAccess = new LogDataAccess();
            int exceptedValue = 1;

            int result = logDataAccess.InsertTBL_LOG("Mensaje de Prueba",1);

            Assert.AreEqual(exceptedValue, result);
        }

    }
}
