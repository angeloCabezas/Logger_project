using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobLoggerProject;

namespace JobLoggerTests
{
    [TestClass]
    public class JobLoggerTest
    {

        [TestInitialize]
        public void Initialize()
        {
            JobLogger.getConfigurationApp();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid message")]
        public void JobLoggerWithInvalidMessage()
        {
            JobLogger.ValidateMessage("");
            //Assert => Exception
        }

        [TestMethod]
        public void JobLoggerWithValidMessage()
        {
            string expectedValue = "Prueba de mensaje";

            string result = JobLogger.ValidateMessage(" Prueba de mensaje ");
            
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, You must be specified type of Log")]
        public void JobLoggerWithInvalid_ValidateSetTypeLogAllow()
        {
            JobLogger.SetTypeLogAllow(false, false, false);

            JobLogger.ValidateSetTypeLogAllow();
            //Assert => Exception
        }

        [TestMethod]
        public void JobLoggerWithValid_ValidateSetTypeLogAllow()
        {
            bool exceptedValue = true;

            bool result = JobLogger.ValidateSetTypeLogAllow();
            
            Assert.AreEqual(exceptedValue, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error or Warning or Message must be specified")]
        public void JobLoggerWithInvalid_ValidateSetMessageAllow()
        {
            JobLogger.SetMessageAllow(false, false, false);

            JobLogger.ValidateSetMessageAllow();
            //Assert => Exception
        }

        [TestMethod]
        public void JobLoggerWithValid_ValidateSetMessageAllow()
        {
            bool exceptedValue = true;

            bool result = JobLogger.ValidateSetMessageAllow();
            //Assert => Exception
            Assert.AreEqual(exceptedValue, result);
        }

        [TestMethod]
        public void JobLogger_ValidateTypeAllowTrue()
        {
            bool expectedValue = true;

            bool result = JobLogger.ValidateTypeAllow(TypeMessage.Warning);

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void JobLogger_SaveMessageInBD()
        {
            int expectedValue = 1;

            int result = JobLogger.SaveMessageInBD("Guardar en Base de datos", TypeMessage.Warning);

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void JobLogger_SaveMessageInTextFile()
        {
            bool expectedValue = true;

            bool result = JobLogger.SaveMessageInTextFile("Guardar en Text File", TypeMessage.Error);

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void JobLogger_SaveMessageInConsole()
        {
            bool expectedValue = true;

            bool result = JobLogger.SaveMessageInConsole("Mostrar en consola", TypeMessage.Warning);

            Assert.AreEqual(expectedValue, result);
        }


    }
}
