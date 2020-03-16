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
using Java.Net;
using Android.Net;

namespace EducationalSoftware.Verification
{
    public class VerifyRegistration
    {
        private FirebaseHelper helper = new FirebaseHelper();
        FirebaseClient client = new FirebaseClient("https://educationalsoftware-ba7e4.firebaseio.com/");
        public bool VerifyName(string name)
        {
            //Checking for numbers
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
            //Check if email is taken
            try
            {
                var item = helper.GetLogin(email).GetAwaiter().GetResult().Email;
                return false;
            }
            catch
            {

            }
            //Validating email
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
        public int VerifyPassword(string password, string repeatPassword)
        {
            //Password should be between 6 and 20 characters long
            if (password.Length < 6 || password.Length > 20)
            {
                return 0;
            }
            //Password should contain upper case, lower case and digit
            else if (!password.Any(ch => char.IsUpper(ch)) || !password.Any(ch => char.IsDigit(ch)) || !password.Any(ch => char.IsLower(ch)))
            {
                return 1;
            }
            //Checking for illegal characters
            else if (password.Contains('<') || password.Contains('>') || password.Contains(';') ||
                    password.Contains('\\') || password.Contains('{') || password.Contains('}') ||
                    password.Contains('[') || password.Contains(']') || password.Contains('+') ||
                    password.Contains(',') || password.Contains('?') || password.Contains('\'') ||
                    password.Contains('\"') || password.Contains('`') || password.Contains(':'))
            {
                return 2;
            }
            //Checking for whitespaces
            else if (password.Contains(' ') || password.Contains('\t') || password.Contains('\n'))
            {
                return 3;
            }
            //Confirm password 
            else if (string.Equals(password, repeatPassword) == false)
            {
                return 4;
            }
            //Password accepted
            else
            {
                return 5;
            }
        }
        public bool VerifyAge(short age)
        {
            //Users must be between 10 and 99
            if (age < 10 || age > 99)
            {
                return false;
            }
            return true;
        }
        public bool VerifyClass(short classInSchool)
        {
            if (classInSchool < 0 || classInSchool > 12)
            {
                return false;
            }
            return true;
        }
        public bool VerifyCourse(short course)
        {
            if (course < 1 || course > 6)
            {
                return false;
            }
            return true;
        }
        public bool CheckInternetConnection()
        {
            ConnectivityManager cm = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = cm.ActiveNetworkInfo;
            return (activeConnection != null) && activeConnection.IsConnected;
        }
    }
}