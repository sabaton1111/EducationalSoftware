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
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;
using Firebase.Database;
using System.Threading.Tasks;

namespace EducationalSoftware.Verification
{
    public class VerifyRegistration
    {
        private FirebaseHelper helper = new FirebaseHelper();
        FirebaseClient client = new FirebaseClient("https://educationalsoftware-ba7e4.firebaseio.com/");

    
        public bool VerifyName(string name)
        {
            if (Regex.IsMatch(name, @"^[\p{L}]+$") == false || string.IsNullOrEmpty(name) == true)
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
           
            // bool result = await helper.GetEmailAddresses()
            if (helper.GetEmail(email).GetAwaiter().GetResult() == false)
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