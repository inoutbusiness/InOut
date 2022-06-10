using InOut.Domain.Entities;
using InOut.Domain.Enums;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace InOut.Domain.Tests
{
    public class CryptTests
    {
        [Fact]
        public void Encrypt_ValidString_EncryptEmail()
        {
            // Arrange
            var emailToEncryot = "myEmail@gmail.com";
            var crypt = new Crypt();

            // Act
            var result = crypt.Encrypt(emailToEncryot, EEncryptionType.Email);

            // Assert
        }

        [Fact]
        public void Encrypt_NullString_EncryptEmail()
        {
            // Arrange
            string emailToEncryot = null;
            var crypt = new Crypt();

            // Act
            var act = () => crypt.Encrypt(emailToEncryot, EEncryptionType.Email);

            // Assert
            var exception = Assert.Throws<Exception>(act);
            Assert.Contains("Ocorreu um erro ao encriptografar um(a)", exception.Message);
        }
    }
}