using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
//using Firebase.Database.Query;
using System.Threading.Tasks;
using EducationalSoftware.Models;
using Firebase.Database.Query;

namespace EducationalSoftware.Extensions
{
    public class FirebaseHelper
    {
        FirebaseClient client = new FirebaseClient("https://educationalsoftware-ba7e4.firebaseio.com/");

        #region Registration helpers

        public async Task<List<User>> GetAllUsers()
        {
            return (await client
                .Child("Users")
                .OnceAsync<User>())
                .Select(item => new User
                {
                    FirstName = item.Object.FirstName,
                    LastName = item.Object.LastName,
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    Age = item.Object.Age
                }).ToList();
        }
        public async Task AddUser(string firstName, string lastName, string email, string password, short age)
        {
            await client
                .Child("Users")
                .PostAsync(new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Age = age
                });
        }
        public async Task<User> GetUser(string email)
        {
            var allUsers = await Task.Run(() => GetAllUsers()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("Users")
                .OnceAsync<User>();
            return allUsers.Where(a => a.Email == email).First();
        }
        public async Task<bool> GetEmail(string email)
        {
            try
            {
                var user = await Task.Run(() => GetUser(email)).ConfigureAwait(continueOnCapturedContext: false);//GetUser(email).ConfigureAwait(false);
                if (user != null)
                {
                    if (user.Email == email)
                    {
                        return await Task.FromResult(false);
                    }
                    else
                    {
                        return await Task.FromResult(true);
                    }
                }
            }
            catch
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        public async Task UpdatePerson(string firstName, string lastName, string email, string password, short age)
        {
            var toUpdateUser = (await client
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Email == email).First();

            await client
              .Child("Users")
              .Child(toUpdateUser.Key)
              .PutAsync(new User()
              {
                  FirstName = firstName,
                  LastName = lastName,
                  Email = email,
                  Password = password,
                  Age = age
              });
        }
        public async Task DeletePerson(string email)
        {
            var toDeleteUser = (await client
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Email == email).First();
            await client.Child("Users").Child(toDeleteUser.Key).DeleteAsync();

        } 
        #endregion

        #region Login helpers
        public async Task<List<Login>> GetAllUsersLogin()
        {
            return (await client
                .Child("Login")
                .OnceAsync<Login>())
                .Select(item => new Login
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    Token = item.Object.Token
                }).ToList();
        }

        public async Task<Login> GetLogin(string email)
        {
            var allUsers = await Task.Run(() => GetAllUsersLogin()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("Login")
                .OnceAsync<User>();
            return allUsers.Where(a => a.Email == email).First();
        }

        public async Task AddEmailPass(string email, string password, string token)
        {
            await client
                .Child("Login")
                .PostAsync(new Login { Email = email, Password = password, Token = token });
        }
        public async Task DeleteLogin(string email)
        {
            var toDeleteAdmin = (await client
              .Child("Login")
              .OnceAsync<Login>()).Where(a => a.Object.Email == email).First();
            await client.Child("Login").Child(toDeleteAdmin.Key).DeleteAsync();

        }

        #endregion

        #region Registration request
        public async Task<List<RegistrationRequest>> GetAllRequests()
        {
            return (await client
                .Child("Request")
                .OnceAsync<RegistrationRequest>())
                .Select(item => new RegistrationRequest
                {
                    RequestString = item.Object.RequestString,
                    Token = item.Object.Token
                }).ToList();
        }

        public async Task<RegistrationRequest> GetRequest(string requestString)
        {
            var allRequests = await Task.Run(() => GetAllRequests()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("Request")
                .OnceAsync<RegistrationRequest>();
            return allRequests.Where(a => a.RequestString == requestString).First();
        }

        public async Task AddRequest(string requestString, string token)
        {
            await client
                .Child("Request")
                .PostAsync(new RegistrationRequest { RequestString = requestString, Token = token });
        }

        public async Task UpdateRequest(string requestString, string token)
        {
            var toUpdateRequest = (await client
              .Child("Request")
              .OnceAsync<RegistrationRequest>()).Where(a => a.Object.RequestString == requestString).First();

            await client
              .Child("Request")
              .Child(toUpdateRequest.Key)
              .PutAsync(new RegistrationRequest()
              {
                  RequestString = requestString,
                  Token = token
              });
        }
        #endregion

        #region Teacher registration
        public async Task<List<Teacher>> GetTeacherUsers()
        {
            return (await client
                .Child("Teachers")
                .OnceAsync<Teacher>())
                .Select(item => new Teacher
                {
                    FirstName = item.Object.FirstName,
                    LastName = item.Object.LastName,
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    Institution = item.Object.Institution,
                    Subject = item.Object.Subject,
                    Age = item.Object.Age
                }).ToList();
        }
        public async Task AddTeacher(string firstName, string lastName, string email, string password, string subject, string institution, short age)
        {
            await client
                .Child("Teachers")
                .PostAsync(new Teacher
                {
                    Subject = subject,
                    Institution = institution,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Age = age
                });
        }
        public async Task<Teacher> GetTeacher(string email)
        {
            var allTeachers = await Task.Run(() => GetTeacherUsers()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("Teachers")
                .OnceAsync<Teacher>();
            return allTeachers.Where(a => a.Email == email).First();
        }

        public async Task UpdateTeacher(string firstName, string lastName, string email, string password, string subject, string institution, short age)
        {
            var toUpdateTeacher = (await client
              .Child("Teachers")
              .OnceAsync<Teacher>()).Where(a => a.Object.Email == email).First();

            await client
              .Child("Teachers")
              .Child(toUpdateTeacher.Key)
              .PutAsync(new Teacher()
              {
                  Subject = subject,
                  Institution = institution,
                  FirstName = firstName,
                  LastName = lastName,
                  Email = email,
                  Password = password,
                  Age = age
              });
        }
        public async Task DeleteTeacher(string email)
        {
            var toDeleteTeacher = (await client
              .Child("Teachers")
              .OnceAsync<Teacher>()).Where(a => a.Object.Email == email).First();
            await client.Child("Teachers").Child(toDeleteTeacher.Key).DeleteAsync();

        }

        #endregion

        #region University student
        public async Task<List<UniversityStudent>> GetUniversityStudentUsers()
        {
            return (await client
                .Child("UniversityStudents")
                .OnceAsync<UniversityStudent>())
                .Select(item => new UniversityStudent
                {
                    FirstName = item.Object.FirstName,
                    LastName = item.Object.LastName,
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    UniversityName = item.Object.UniversityName,
                    Speciality = item.Object.Speciality,
                    Course = item.Object.Course,
                    Country = item.Object.Country,
                    City = item.Object.City,
                    Age = item.Object.Age
                }).ToList();
        }
        public async Task AddUniversityStudent(string firstName, string lastName, string email, string password, 
            string universityName, string speciality, short course, string country, string city, short age)
        {
            await client
                .Child("UniversityStudents")
                .PostAsync(new UniversityStudent
                {
                   
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    UniversityName = universityName,
                    Speciality = speciality,
                    Course = course,
                    Country = country,
                    City = city,
                    Age = age
                });
        }
        public async Task<UniversityStudent> GetUniversityStudent(string email)
        {
            var allUniversityStudents = await Task.Run(() => GetUniversityStudentUsers()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("UniversityStudents")
                .OnceAsync<UniversityStudent>();
            return allUniversityStudents.Where(a => a.Email == email).First();
        }

        public async Task UpdateUniversityStudent(string firstName, string lastName, string email, string password, 
            string universityName, string speciality, short course, string country, string city, short age)
        {
            var toUpdateUniversityStudent = (await client
              .Child("UniversityStudents")
              .OnceAsync<UniversityStudent>()).Where(a => a.Object.Email == email).First();

            await client
              .Child("UniversityStudents")
              .Child(toUpdateUniversityStudent.Key)
              .PutAsync(new UniversityStudent()
              {
                  FirstName = firstName,
                  LastName = lastName,
                  Email = email,
                  Password = password,
                  UniversityName = universityName,
                  Speciality = speciality,
                  Course = course,
                  Country = country,
                  City = city,
                  Age = age
              });
        }
        public async Task DeleteUniversityStudent(string email)
        {
            var toDeleteUniversityStudent = (await client
              .Child("UniversityStudents")
              .OnceAsync<UniversityStudent>()).Where(a => a.Object.Email == email).First();
            await client.Child("UniversityStudents").Child(toDeleteUniversityStudent.Key).DeleteAsync();

        }
        #endregion

        #region School student
        public async Task<List<SchoolStudent>> GetSchoolStudentUsers()
        {
            return (await client
                .Child("SchoolStudents")
                .OnceAsync<SchoolStudent>())
                .Select(item => new SchoolStudent
                {
                    FirstName = item.Object.FirstName,
                    LastName = item.Object.LastName,
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    SchoolName = item.Object.SchoolName,
                    ClassInSchool = item.Object.ClassInSchool,
                    Country = item.Object.Country,
                    City = item.Object.City,
                    Age = item.Object.Age
                }).ToList();
        }
        public async Task AddSchoolStudent(string firstName, string lastName, string email, string password,
            string schoolName, short classInSchool, string country, string city, short age)
        {
            await client
                .Child("SchoolStudents")
                .PostAsync(new SchoolStudent
                {

                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    SchoolName = schoolName,
                    ClassInSchool = classInSchool,
                    Country = country,
                    City = city,
                    Age = age
                });
        }
        public async Task<SchoolStudent> GetSchoolStudent(string email)
        {
            var allSchoolStudents = await Task.Run(() => GetSchoolStudentUsers()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("SchoolStudents")
                .OnceAsync<SchoolStudent>();
            return allSchoolStudents.Where(a => a.Email == email).First();
        }

        public async Task UpdateSchoolStudent(string firstName, string lastName, string email, string password,
            string schoolName, short classInSchool, string country, string city, short age)
        {
            var toUpdateSchoolStudent = (await client
              .Child("SchoolStudents")
              .OnceAsync<SchoolStudent>()).Where(a => a.Object.Email == email).First();

            await client
              .Child("SchoolStudents")
              .Child(toUpdateSchoolStudent.Key)
              .PutAsync(new SchoolStudent()
              {
                  FirstName = firstName,
                  LastName = lastName,
                  Email = email,
                  Password = password,
                  SchoolName = schoolName,
                  ClassInSchool = classInSchool,
                  Country = country,
                  City = city,
                  Age = age
              });
        }
        public async Task DeleteSchoolStudent(string email)
        {
            var toDeleteSchoolStudent = (await client
              .Child("SchoolStudents")
              .OnceAsync<SchoolStudent>()).Where(a => a.Object.Email == email).First();
            await client.Child("SchoolStudents").Child(toDeleteSchoolStudent.Key).DeleteAsync();

        }
        #endregion

        #region Admin registration
        public async Task<List<Admin>> GetAdminUsers()
        {
            return (await client
                .Child("Admins")
                .OnceAsync<Admin>())
                .Select(item => new Admin
                {
                    VerificationCode = item.Object.VerificationCode,
                    FirstName = item.Object.FirstName,
                    LastName = item.Object.LastName,
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    Age = item.Object.Age
                }).ToList();
        }
        public async Task AddAdmin(string verificationCode,string firstName, string lastName, string email, string password, short age)
        {
            await client
                .Child("Admins")
                .PostAsync(new Admin
                {
                    VerificationCode = verificationCode,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Age = age
                });
        }
        public async Task<Admin> GetAdmin(string verificationCode)
        {
            var allAdmins = await Task.Run(() => GetAdminUsers()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("Admins")
                .OnceAsync<Admin>();
            return allAdmins.Where(a => a.VerificationCode == verificationCode).First();
        }
       
        public async Task UpdateAdmin(string verificationCode, string firstName, string lastName, string email, string password, short age)
        {
            var toUpdateAdmin = (await client
              .Child("Admins")
              .OnceAsync<Admin>()).Where(a => a.Object.VerificationCode == verificationCode).First();

            await client
              .Child("Admins")
              .Child(toUpdateAdmin.Key)
              .PutAsync(new Admin()
              {
                  VerificationCode = verificationCode,
                  FirstName = firstName,
                  LastName = lastName,
                  Email = email,
                  Password = password,
                  Age = age
              });
        }
        public async Task DeleteAdmin(string email)
        {
            var toDeleteAdmin = (await client
              .Child("Admins")
              .OnceAsync<Admin>()).Where(a => a.Object.Email == email).First();
            await client.Child("Admins").Child(toDeleteAdmin.Key).DeleteAsync();

        }

        #endregion
    }
}