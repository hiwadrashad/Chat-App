using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Database.Encryption
{
    public class HashingAndSalting
    {
        public static byte[] GetHash(string password, string salt)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(String.Concat(salt, password));

            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);

            return hashedBytes;
        }

        public static bool CompareHash(string attemptedPassword, string base64hashinput, string salt)
        {
            byte[] hash = Convert.FromBase64String(base64hashinput);
            string base64Hash = Convert.ToBase64String(hash);
            string base64AttemptedHash = Convert.ToBase64String(GetHash(attemptedPassword, salt));

            return base64Hash == base64AttemptedHash;
        }
    }
}
