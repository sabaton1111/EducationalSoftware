using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace EducationalSoftware.Extensions
{
    public class Encryption
    {
        public string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }
    }
}