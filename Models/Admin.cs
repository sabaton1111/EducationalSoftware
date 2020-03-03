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
    public class Admin : User
    {
        private string verificationCode; //For registrating new Admin
        public string VerificationCode { get; set; }

        #region Constructors
        //Constructor with parameters
        public Admin(string verificationCode, string firstName, string lastName, string email, string password, short age)
            : base(firstName, lastName, email, password, age)
        {
            VerificationCode = verificationCode;
        }
        //Default constructor
        public Admin() : this("Empty", "Empty", "Empty", "Empty", "Empty", 0) { }
        //Copy constructor
        public Admin(Admin admin) : this(admin.verificationCode, admin.FirstName, admin.LastName, admin.Email, admin.Password, admin.Age) { }
        #endregion 
    }
}