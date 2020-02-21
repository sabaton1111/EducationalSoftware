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
        private string token;

        public string RequestString { get; set; }
        public string Token { get; set; }

        public RegistrationRequest(string requestString, string token)
        {
            RequestString = requestString;
            Token = token;
        }

        public RegistrationRequest() : this("Empty", "Empty")
        {

        }

        public RegistrationRequest(RegistrationRequest request) : this(request.requestString, request.token)
        {

        }
    }
}