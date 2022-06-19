using InOut.Domain.Enums;

namespace InOut.Domain.Interfaces
{
    public interface ICrypt
    {
        byte[] Encrypt(string valueToEncrypt, EEncryptionType encryptionType);

        string Decrypt(byte[] valueToDecrypt, EEncryptionType encryptionType);
    }
}