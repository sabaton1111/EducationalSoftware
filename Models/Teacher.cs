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
        private List<string> subjects;
        private string institution;

        public List<string> Subjects { get; set; }
        public string Institution { get; set; }

        public Teacher(string firstName, string lastName, string email, short age, List<string> subjects, string institution) : base(firstName,
            lastName, email, age)
        {
            Subjects = subjects;
            Institution = institution;
        }

        public Teacher() : this("Empty", "Empty", "Empty", 0, null, "Empty") { }

        public Teacher(Teacher teacher) : this(teacher.FirstName, teacher.LastName, teacher.Email, teacher.Age, teacher.subjects, teacher.institution) { }
    }
}