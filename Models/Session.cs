using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EducationalSoftware.Models
{
    public class Session
    {
        private string sessionString;
        private string email;
        private string token;

        public string SessionString { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public Session(string sessionString, string email, string token)
        {
            SessionString = sessionString;
            Token = token;
        }

        public Session() : this("Empty", "Empty", "Empty")
        {

        }

        public Session(Session session) : this(session.sessionString, session.email, session.token)
        {

        }
    }
}