using System;
using System.Text;

namespace CentralDeErrosApi.Utils
{
    public static class Encrypt
    {
        public static string FromBase64String(string encoded)
        {
            var data = Convert.FromBase64String(encoded);
            return Encoding.ASCII.GetString(data);
        }

        public static string ToBase64String(string decoded)
        {
            var data = ASCIIEncoding.ASCII.GetBytes(decoded);
            return Convert.ToBase64String(data);
        }
    }
}
