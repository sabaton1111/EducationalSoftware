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
        #region Datamembers
        private string sessionString;
        private string email;
        private string token;
        #endregion

        #region Properties
        public string SessionString { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        #endregion

        #region Constructors
        //Costructor with parameters
        public Session(string sessionString, string email, string token)
        {
            SessionString = sessionString;
            Token = token;
        }
        //Default constructor
        public Session() : this("Empty", "Empty", "Empty") { }
        //Copy constructor
        public Session(Session session) : this(session.sessionString, session.email, session.token) { } 
        #endregion
    }
}