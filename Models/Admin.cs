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
        private string verificationCode;
        public string VerificationCode { get; set; }
        #region Constructors
        public Admin(string firstName, string lastName, string email, string password, short age, string verificationCode)
            : base(firstName, lastName, email, password, age)
        {
            VerificationCode = verificationCode;
        }
        public Admin() : this("Empty", "Empty", "Empty", "Empty", 0, "Empty") { }
        public Admin(Admin admin) : this(admin.FirstName, admin.LastName, admin.Email, admin.Password, admin.Age, admin.verificationCode) { }
        #endregion 
    }
}