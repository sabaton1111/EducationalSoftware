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
    public class UniversityStudent : User
    {
        private string speciality;
        private int course;
        private string universityName;
        private string city;
        private string country;

        public string Speciality { get; set; }
        public int Course { get; set; }
        public string UniversityName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public UniversityStudent(string firstName, string lastName, string email, short age, string speciality, int course, string universityName,
            string city, string country) : base(firstName, lastName, email, age)
        {
            Speciality = speciality;
            Course = course;
            UniversityName = universityName;
            City = city;
            Country = country;
        }

        public UniversityStudent() : this("Empty", "Empty", "Empty", 0, "Empty", 0, "Empty", "Empty", "Empty") { }
        public UniversityStudent(UniversityStudent universityStudent) : this(universityStudent.FirstName, universityStudent.LastName,
            universityStudent.Email, universityStudent.Age, universityStudent.speciality, universityStudent.course, universityStudent.universityName,
            universityStudent.city, universityStudent.country) { }
    }
}