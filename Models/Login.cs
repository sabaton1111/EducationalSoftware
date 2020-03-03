using System;
namespace EducationalSoftware.Models
{
    public class Login
    {
        #region Datamembers
        private string email;
        private string password;
        private string token; //User type
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        #endregion

        #region Constructors
        //Constructor with parameters
        public Login(string email, string password, string token)
        {
            Email = email;
            Password = password;
            Token = token;
        }

        //Default constructor
        public Login() : this("Empty", "Empty", "Empty") { }

        //Copy constructor
        public Login(Login login) : this(login.Email, login.Password, login.Token) { } 
        #endregion
    }
}