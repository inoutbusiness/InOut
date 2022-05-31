using InOut.Domain.Entities;
using InOut.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InOut.Domain.Tests
{
    [TestClass]
    public class CryptTests
    {
        [TestMethod]
        public void Encrypt_ValidString_EncryptPassword()
        {
            // Arrange
            var passwordToEncryot = "MyPassword101";
            var crypt = new Crypt();

            // Act
            var result = crypt.Encrypt(passwordToEncryot, EEncryptionType.Password);

            // Assert
        }

        [TestMethod]
        public void Encrypt_NullString_EncryptPassword()
        {
            // Arrange
            string passwordToEncryot = null;
            var crypt = new Crypt();

            // Act
            Action action = () => crypt.Encrypt(passwordToEncryot, EEncryptionType.Password);

            // Assert
            Assert.ThrowsException(action);
        }
    }
}