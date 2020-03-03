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
    public class RegistrationRequest
    {
        private string requestString;
        private string token; //User type to register

        public string RequestString { get; set; }
        public string Token { get; set; }

        #region Constructors
        //Constructor with parameters
        public RegistrationRequest(string requestString, string token)
        {
            RequestString = requestString;
            Token = token;
        }
        //Default constructor
        public RegistrationRequest() : this("Empty", "Empty") { }
        //Copy constructor
        public RegistrationRequest(RegistrationRequest request) : this(request.requestString, request.token) { } 
        #endregion
    }
}