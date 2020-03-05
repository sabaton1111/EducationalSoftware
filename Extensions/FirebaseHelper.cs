using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using System.Threading.Tasks;
using EducationalSoftware.Models;
using Firebase.Database.Query;

namespace EducationalSoftware.Extensions
{
    public class FirebaseHelper
    {
        FirebaseClient client = new FirebaseClient("https://educationalsoftware-ba7e4.firebaseio.com/");
        public async Task<List<T>> GetAllUsers<T>(string token)
        {
            return (await client
                .Child(token)
                .OnceAsync<T>())
                .Select(item => item.Object).ToList();
        }
        public async Task AddToFirebase<T>(T t, string token)
        {
            await client
                .Child(token)
                .PostAsync(t);
        }
        public async Task DeleteAccount<T>(string token, string email)
        {
            var toDeleteUser = (await client
              .Child(token)
              .OnceAsync<User>()).Where(a => a.Object.Email == email).First();
            await client.Child(token).Child(toDeleteUser.Key).DeleteAsync();

        }
        public async Task<T> GetData<T>(string token, string email)
        {
            //TODO: Find another solution
            switch (token)
            {
                case "Admins":
                    var allAdmins = await Task.Run(() => GetAllUsers<Admin>(token)).ConfigureAwait(continueOnCapturedContext: false);
                    return (T)(object)allAdmins.Where(a => a.Email == email).First();
                case "Teachers":
                    var allTeachers = await Task.Run(() => GetAllUsers<Teacher>(token)).ConfigureAwait(continueOnCapturedContext: false);
                    return (T)(object)allTeachers.Where(a => a.Email == email).First();
                case "Users":
                    var allUsers = await Task.Run(() => GetAllUsers<User>(token)).ConfigureAwait(continueOnCapturedContext: false);
                    return (T)(object)allUsers.Where(a => a.Email == email).First();
                case "UniversityStudents":
                    var allUStudents = await Task.Run(() => GetAllUsers<UniversityStudent>(token)).ConfigureAwait(continueOnCapturedContext: false);
                    return (T)(object)allUStudents.Where(a => a.Email == email).First();
                case "SchoolStudents":
                    var allSchoolStudents = await Task.Run(() => GetAllUsers<SchoolStudent>(token)).ConfigureAwait(continueOnCapturedContext: false);
                    return (T)(object)allSchoolStudents.Where(a => a.Email == email).First();
                default:
                    User user = new User("asd", "asdd", "asw", "paa", 15);
                    return (T)(object)user;
            }
        }
       public async Task UpdatePassword<T>(T t, string token, string email)
        {
            await DeleteAccount<T>(token, email);
            await AddToFirebase(t, token);
        }
        public async Task<Login> GetLogin(string email)
        {
            var allUsers = await Task.Run(() => GetAllUsers<Login>("Login")).ConfigureAwait(continueOnCapturedContext: false);
            return allUsers.Where(a => a.Email == email).First();
        }

        #region Registration request
        public async Task<RegistrationRequest> GetRequest(string requestString)
        {
            var allRequests = await Task.Run(() => GetAllUsers<RegistrationRequest>("Request")).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("Request")
                .OnceAsync<RegistrationRequest>();
            return allRequests.Where(a => a.RequestString == requestString).First();
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

        //public async Task UpdatePerson(string firstName, string lastName, string email, string password, short age)
        //{
        //    var toUpdateUser = (await client
        //      .Child("Users")
        //      .OnceAsync<User>()).Where(a => a.Object.Email == email).First();

        //    await client
        //      .Child("Users")
        //      .Child(toUpdateUser.Key)
        //      .PutAsync(new User()
        //      {
        //          FirstName = firstName,
        //          LastName = lastName,
        //          Email = email,
        //          Password = password,
        //          Age = age
        //      });
        //}
    }
}