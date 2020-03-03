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
    public class SchoolStudent : User
    {
        #region Datamembers
        private short classInSchool;
        private string schoolName;
        private string city;
        private string country;
        #endregion

        #region Properties
        public short ClassInSchool { get; set; }
        public string SchoolName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        #endregion

        #region Constructors
        //Constructor with parameters
        public SchoolStudent(string firstName, string lastName, string email, string password, short age,
            short classInSchool, string schoolName, string city, string country) : base(firstName, lastName, email, password, age)
        {
            ClassInSchool = classInSchool;
            SchoolName = schoolName;
            City = city;
            Country = country;
        }
        //Default constructor
        public SchoolStudent() : this("Empty", "Empty", "Empty", "Empty", 0, 0, "Empty", "Empty", "Empty") { }
        //Copy constructor
        public SchoolStudent(SchoolStudent schoolStudent) : this(schoolStudent.FirstName, schoolStudent.LastName,
            schoolStudent.Email, schoolStudent.Password, schoolStudent.Age, schoolStudent.classInSchool,
            schoolStudent.schoolName, schoolStudent.city, schoolStudent.country)
        { } 
        #endregion
    }
}