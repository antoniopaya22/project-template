using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NETCore_API_REST_Template.Auth
{
    public class AESManager
    {
        private static string _KEY = "8CDAD5BCDB7D4760BEA486D46CB194BD";
        private static string _IV = "C35DB3EA6469C0D7";

        public static string Encrypt(string plainText)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(_KEY), Encoding.UTF8.GetBytes(_IV));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return WebEncoders.Base64UrlEncode(encrypted);
        }

        public static string Decrypt(string cipherText)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(_KEY), Encoding.UTF8.GetBytes(_IV));
                using (MemoryStream ms = new MemoryStream(WebEncoders.Base64UrlDecode(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
