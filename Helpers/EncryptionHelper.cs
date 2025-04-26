using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;



    public static class EncryptionHelper
    {
        // Genera una clave segura a partir de la contraseña del usuario
        private static byte[] GenerateKey(string userPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(userPassword));
            }
        }

        public static string Encrypt(string plainText, string userPassword)
        {
            byte[] keyBytes = GenerateKey(userPassword);
            byte[] iv = new byte[16]; // IV de 16 bytes (todo ceros)

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                    sw.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string userPassword)
        {
            byte[] keyBytes = GenerateKey(userPassword);  // Generamos la misma clave que usamos para encriptar
            byte[] iv = new byte[16]; // IV de 16 bytes (todo ceros)
            byte[] buffer = Convert.FromBase64String(cipherText);  // Convertimos el texto cifrado desde Base64

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;  // Usamos el mismo IV
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(buffer))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();  // Retornamos el texto desencriptado
                }
            }
        }

    }
}






