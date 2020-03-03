using System;
using System.Text;

namespace EducationalSoftware.Extensions
{
    public class Encryption
    {
        public string EncodePassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
        public string DecodePassword(string password)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(password));
        }
    }
}