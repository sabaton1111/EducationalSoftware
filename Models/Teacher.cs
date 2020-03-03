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
    public class Teacher : User
    {
        private string subject;
        private string institution;

        public string Subject { get; set; }
        public string Institution { get; set; }

        #region Constructors
        //Constructor with parameters
        public Teacher(string firstName, string lastName, string email, string password, short age,
            string subject, string institution) : base(firstName, lastName, email, password, age)
        {
            Subject = subject;
            Institution = institution;
        }
        //Default constructor
        public Teacher() : this("Empty", "Empty", "Empty", "Empty", 0, "Empty", "Empty") { }
        //Copy constructor
        public Teacher(Teacher teacher) : this(teacher.FirstName, teacher.LastName, teacher.Email,
            teacher.Password, teacher.Age, teacher.subject, teacher.institution)
        { } 
        #endregion
    }
}