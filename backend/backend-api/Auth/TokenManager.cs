using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ASP.NETCore_API_REST_Template.Auth
{
    public class HashSalt
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }

    public class TokenManager
    {
        public static bool validateToken(string token)
        {
            try
            {
                var securityToken = JsonConvert.DeserializeObject<dynamic>(AESManager.Decrypt(token));
                if (securityToken == null)
                    return false;
                else
                    return DateTime.Parse(securityToken.ExpirationDate.ToString()) > DateTime.Now;
            }
            catch (Exception) { return false; }
        }

        public static string generateToken(string userName)
        {
            try
            {
                var token = AESManager.Encrypt(JsonConvert.SerializeObject(new { UserName = userName, ExpirationDate = DateTime.Now.AddDays(10) }));
                return token;
            }
            catch (Exception) { return null; }
        }

        public static HashSalt GenerateSaltedHash(string password)
        {
            var saltBytes = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}
