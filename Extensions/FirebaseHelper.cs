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
                .PostAsync(new User { FirstName = firstName, LastName = lastName, Email = email, Password = password,
                    Age = age });
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
              .PutAsync(new User() { FirstName = firstName, LastName = lastName, Email = email, Password = password,
                  Age = age });
        }

        public async Task DeletePerson(string email)
        {
            var toDeleteUser = (await client
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Email == email).First();
            await client.Child("Users").Child(toDeleteUser.Key).DeleteAsync();

        }
    }
}