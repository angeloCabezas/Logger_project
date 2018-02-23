using System;
using JobLoggerProject.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLoggerTests
{
    [TestClass]
    public class LogFileManagerTests
    {
        [TestMethod]
        public void LogFileManagerInvalid_GetDirectory()
        {
            string result = LogFileManager.getDirectory("XXXX");

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void LogFileManagerValid_GetDirectory()
        {
            string exceptedValue = "";

            string result = LogFileManager.getDirectory("LogFileDirectory");

            Assert.AreNotEqual(exceptedValue, result);
        }

        [TestMethod]
        public void LogDataAccess_InsertTBL_LOG()
        {
            bool exceptedValue = true;
            string filename = "ArchivoPrueba.txt";

            bool result = LogFileManager.WriteMesageToTextFile(filename,"texto de prueba para almacenar archivo");

            Assert.AreEqual(exceptedValue, result);
        }
    }
}
