using InOut.Domain.Enums;
using InOut.Domain.Exceptions;
using InOut.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace InOut.Domain.Entities
{
    public class Crypt : ICrypt
    {
        public byte[] Encrypt(string valueToEncrypt, EEncryptionType encryptionType)
        {
            try
            {
                byte[] valueToBeEncryptedBytes = Encoding.UTF8.GetBytes(valueToEncrypt);
                byte[] encryptionTypeBytes = Encoding.UTF8.GetBytes(encryptionType.ToString());
                byte[] encryptedBytes = null;
                byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };
                using (MemoryStream ms = new MemoryStream())
                {
                    using (var AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(encryptionTypeBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(valueToBeEncryptedBytes, 0, valueToBeEncryptedBytes.Length);
                            cs.Close();
                        }
                        encryptedBytes = ms.ToArray();
                    }
                }

                return encryptedBytes;
            }
            catch (Exception ex)
            {
                throw new CryptException($"Ocorreu um erro ao encriptografar um(a) {encryptionType.ToString()}", ex);
            }
        }

        public string Decrypt(byte[] valueToDecrypt, EEncryptionType encryptionType)
        {
            try
            {
                byte[] encryptionTypeBytes = Encoding.UTF8.GetBytes(encryptionType.ToString());
                byte[] decryptedBytes = null;
                byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (var AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(encryptionTypeBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(valueToDecrypt, 0, valueToDecrypt.Length);
                            cs.Close();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                }

                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (Exception ex)
            {
                throw new CryptException($"Ocorreu um erro ao descriptografar um(a) {encryptionType.ToString()}", ex);
            }
        }
    }
}