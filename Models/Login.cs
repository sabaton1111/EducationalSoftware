﻿using System;
namespace EducationalSoftware.Models
{
    public class Login
    {
        private string email;
        private string password;
        private string token;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Login(string email, string password, string token)
        {
            Email = email;
            Password = password;
            Token = token;
        }
        public Login() : this("Empty", "Empty", "Empty") { }
        public Login(Login login) : this(login.Email, login.Password, login.Token) { }
    }
}