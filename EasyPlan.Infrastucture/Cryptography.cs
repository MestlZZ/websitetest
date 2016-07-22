using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EasyPlan.Infrastructure
{
    public class Cryptography
    {
        public static string GetHash(string input)
        {
            var sBuilder = new StringBuilder();

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                foreach (byte bt in data)
                {
                    sBuilder.Append(bt.ToString("x2"));
                }
            }

            return sBuilder.ToString();
        }

        public static bool VerifyHash(string input, string hash)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var hashOfInput = GetHash(input);

            return 0 == comparer.Compare(hashOfInput, hash);
        }
    }
}
