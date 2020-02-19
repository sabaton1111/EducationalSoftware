using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
namespace EducationalSoftware.Verification
{
    public class VerifyRegistration
    {
        private List<string> GetEmailAddresses()
        {
            List<string> emailAddresses = new List<string>();
            using (MySqlConnection connection = new MySqlConnection("server=sql2.freemysqlhosting.net;port=3306;database=sql2323477;user=sql2323477;password=zS1!nT9!;"))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT email FROM user",connection);
                cmd.Connection = connection;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        emailAddresses.Add(Convert.ToString(dataReader["email"]));
                    }
                }
            }
            return emailAddresses;
        }
        public bool VerifyName(string name)
        {
            if(Regex.IsMatch(name, @"^[\p{L}]+$") == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool VerifyEmail(string email)
        {
            if (GetEmailAddresses().Contains(email) == true)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                 @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                 RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch
            {
                return false;
            }
        }
    }
}