﻿using System;
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
    public class User
    {
        #region Datamembers

        private string firstName;
        private string lastName;
        private string email;
        private short age;

        #endregion

        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short Age { get; set; }

        #endregion

        #region Constructors

        public User(string firstName, string lastName, string email, short age)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
        }

        public User() : this("Empty", "Empty", "Empty", 0) { }

        public User(User user) : this(user.FirstName, user.LastName, user.Email, user.Age) { }

        #endregion
    }
}