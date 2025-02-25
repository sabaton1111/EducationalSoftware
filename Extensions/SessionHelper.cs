﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalSoftware.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace EducationalSoftware.Extensions
{
    public class SessionHelper
    {
        FirebaseClient client = new FirebaseClient("https://educationalsoftware-ba7e4.firebaseio.com/");
        public async Task<List<Session>> GetAllSessions()
        {
            return (await client
                .Child("Session")
                .OnceAsync<Session>())
                .Select(item => new Session
                {
                    SessionString = item.Object.SessionString,
                    Email = item.Object.Email,
                    Token = item.Object.Token
                }).ToList();
        }
        public async Task<Session> GetSession(string sessionString)
        {
            var allSessions = await Task.Run(() => GetAllSessions()).ConfigureAwait(continueOnCapturedContext: false);//GetAllUsers().ConfigureAwait(false);
            await client
                .Child("Session")
                .OnceAsync<User>();
            return allSessions.Where(a => a.SessionString == sessionString).First();
        }
        public async Task AddSession(string session, string email, string token)
        {
            await client
                .Child("Session")
                .PostAsync(new Session { SessionString = session, Email = email, Token = token });
        }
        public async Task DeleteSession(string email)
        {
            var toDeleteSession = (await client
              .Child("Session")
              .OnceAsync<Login>()).Where(a => a.Object.Email == email).First();
            await client.Child("Session").Child(toDeleteSession.Key).DeleteAsync();
        }
    }
}