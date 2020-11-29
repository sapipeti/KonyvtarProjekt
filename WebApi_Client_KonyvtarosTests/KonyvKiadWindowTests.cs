using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi_Client_Konyvtaros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebApi_Client_Konyvtaros.Tests
{
    [TestClass()]
    public class KonyvKiadWindowTests
    {
        [DataRow("12345", "1", true)]
        [DataTestMethod()]
        public void ValidateKiadTest_ReturnsTrue(String neptunKod, String darabszam, bool date)
        {
            //Arrange
            KonyvKiadWindow kkw = new KonyvKiadWindow();
            //Act
            bool result = kkw.ValidateKiad(neptunKod, darabszam, date);
            //Assert
            Assert.IsTrue(result);
        }

        [DataRow("12345", "", true)]
        [DataRow("", "1", true)]
        [DataRow("12345", "1", false)]
        [DataTestMethod()]
        public void ValidateKiadTest_ReturnsFalse(String neptunKod, String darabszam, bool date)
        {
            //Arrange
            KonyvKiadWindow kkw = new KonyvKiadWindow();
            //Act
            bool result = kkw.ValidateKiad(neptunKod, darabszam, date);
            //Assert
            Assert.IsFalse(result);
        }
    }
}