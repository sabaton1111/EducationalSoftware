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
    public class MultipleChoiceTest
    {
        #region Data members
        private string imageURL;
        private string userCreatedTest;
        private int numberOfQuestions;
        private string testName;
        private string testNotation;
        private DateTime dateCreated;
        #endregion

        #region Properties
        public string ImageURL { get; set; }
        public string UserCreatedTest { get; set; }
        public int NumberOfQuestions { get; set; }
        public string TestName { get; set; }
        public string TestNotation { get; set; }
        public DateTime DateCreated { get; set; }

        #endregion

        #region Constructors
        //Constructor with paramenters
        public MultipleChoiceTest(string imageURL, string userCreatedTest, int numberOfQuestions, string testName, string testNotation)
        {
            ImageURL = imageURL;
            UserCreatedTest = userCreatedTest;
            NumberOfQuestions = numberOfQuestions;
            TestName = testName;
            TestNotation = testNotation;
            DateCreated = DateTime.Now;
        }
        //Default constructor
        public MultipleChoiceTest() : this("Empty","Empty", 0, "Empty", "Empty") { }
        //Copy constructor
        public MultipleChoiceTest(MultipleChoiceTest choiceTest) : this(choiceTest.ImageURL, choiceTest.UserCreatedTest, choiceTest.NumberOfQuestions, choiceTest.TestName,
            choiceTest.TestNotation)
        { }
        #endregion
    }
}