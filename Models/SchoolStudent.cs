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
        private int classInSchool;
        private string schoolName;
        private string city;
        private string country;

        public int ClassInSchool { get; set; }
        public string SchoolName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public SchoolStudent(string firstName, string lastName, string email, short age, int classInSchool, string schoolName,
            string city, string country) : base(firstName, lastName, email, age)
        {
            ClassInSchool = classInSchool;
            SchoolName = schoolName;
            City = city;
            Country = country;
        }

        public SchoolStudent() : this("Empty", "Empty", "Empty", 0, 0, "Empty", "Empty", "Empty") { }

        public SchoolStudent(SchoolStudent schoolStudent):this(schoolStudent.FirstName, schoolStudent.LastName, schoolStudent.Email,
            schoolStudent.Age, schoolStudent.classInSchool, schoolStudent.schoolName, schoolStudent.city, schoolStudent.country) { }

    }
}