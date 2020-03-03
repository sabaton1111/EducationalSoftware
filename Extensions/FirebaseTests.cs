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
using Firebase.Database;
using EducationalSoftware.Models;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace EducationalSoftware.Extensions
{
    public class FirebaseTests
    {
        FirebaseClient client = new FirebaseClient("https://educationalsoftware-ba7e4.firebaseio.com/");
        public async Task AddToFirebase<T>(T t, string token)
        {
            await client
                .Child(token)
                .PostAsync(t);
        }
        public async Task AddToFirebaseQuestions<T>(T t, string token, string testName)
        {
            await client
                .Child(token)
                .Child(testName)
                .PostAsync(t);
        }
        public async Task<List<T>> GetAll<T>(string token)
        {
            return (await client
                .Child(token)
                .OnceAsync<T>())
                .Select(item => item.Object).ToList();
        }            
        public async Task<MultipleChoiceTest> GetTest(string testName)
        {
            var allTests = await Task.Run(() => GetAll<MultipleChoiceTest>("Tests")).ConfigureAwait(continueOnCapturedContext: false);
            await client
                .Child("Tests")
                .OnceAsync<MultipleChoiceTest>();
            return allTests.Where(a => a.TestName == testName).First();
        }
        public async Task DeleteTest(string testName)
        {
            var toDeleteSession = (await client
              .Child("Tests")
              .OnceAsync<MultipleChoiceTest>()).Where(a => a.Object.TestName == testName).First();
            await client.Child("Tests").Child(toDeleteSession.Key).DeleteAsync();
        }
    }
}