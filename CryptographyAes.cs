using System;
using System.IO;
using System.Security.Cryptography;

namespace Mister.Utils.Cryptography
{
    public sealed class CryptographyAes
    {
        public static byte[] EncryptData(byte[] data, byte[] key, byte[] iv)
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        public static byte[] DecryptData(byte[] encryptedData, byte[] key, byte[] iv)
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[msDecrypt.Length];
                        using (MemoryStream decryptedData = new MemoryStream())
                        {
                            int bytesRead;
                            while ((bytesRead = csDecrypt.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                decryptedData.Write(buffer, 0, bytesRead);
                                if (bytesRead < buffer.Length) break;
                            }
                            return decryptedData.ToArray();
                        }
                    }
                }
            }
        }

        public static byte[] GenerateIV()
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }
    }
}