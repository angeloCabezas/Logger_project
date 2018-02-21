using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobLoggerProject;

namespace JobLoggerTests
{
    [TestClass]
    public class JobLoggerTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid message")]
        public void JobLoggerWithInvalidMessage()
        {
            JobLogger jobLogger = new JobLogger(true, true, true, true, true, true);

            JobLogger.LogMessage("", JobLogger.TypeMessage.Message);
            //Assert => Exception
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid configuration")]
        public void JobLoggerWithInvalidConfiguration()
        {
            JobLogger jobLogger = new JobLogger(false, false, false, true, true, true);

            JobLogger.LogMessage("", JobLogger.TypeMessage.Message);
            //Assert => Exception
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error or Warning or Message must be specified")]
        public void JobLoggerWithInvalidConfigurationMessage()
        {
            JobLogger jobLogger = new JobLogger(true, true, true, false, false, false);

            JobLogger.LogMessage("", JobLogger.TypeMessage.Message);
            //Assert => Exception
        }

        [TestMethod]
        public void JobLogger_SaveMessageInBD()
        {
            int expectedValue = 1;

            int result = JobLogger.SaveMessageInBD("Guardar en Base de datos", JobLogger.TypeMessage.Warning);

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void JobLogger_SaveMessageInTextFile()
        {
            int expectedValue = 1;

            int result = JobLogger.SaveMessageInTextFile("Guardar en Text File", JobLogger.TypeMessage.Error);

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void JobLogger_SaveMessageInConsole()
        {
            int expectedValue = 1;

            int result = JobLogger.SaveMessageInConsole("Mostrar en consola", JobLogger.TypeMessage.Warning);

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void JobLogger_ValidateTypeAllowTrue()
        {//Se habilita el uso de warning
            bool expectedValue = true;
            JobLogger jobLogger = new JobLogger(true, true, true, false, true, false); 

            bool result = JobLogger.ValidateTypeAllow(JobLogger.TypeMessage.Warning);

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void JobLogger_ValidateTypeAllowFalse()
        {//Se deshabilita el uso de warning
            bool expectedValue = false;
            JobLogger jobLogger = new JobLogger(true, true, true, true, false, true); 

            bool result = JobLogger.ValidateTypeAllow(JobLogger.TypeMessage.Warning);

            Assert.AreEqual(expectedValue, result);
        }
    }
}
