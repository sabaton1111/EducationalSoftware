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
    public class TestQuestionPattern
    {
        #region Datamembers
        private string testName;
        private string question;
        private string[] answers;
        #endregion

        #region Properties
        public string TestName { get; private set; }
        public string Question { get; set; }
        public string[] Answers { get; set; }
        #endregion

        #region Constructors
        //Constructor with parameters
        public TestQuestionPattern(string testName, string question, string[] answers)
        {
            TestName = testName;
            Question = question;
            Answers = answers;
        }
        //Default constructor
        public TestQuestionPattern() : this("Empty", "Empty", null) { }
        //Copy constructor
        public TestQuestionPattern(TestQuestionPattern test) : this(test.TestName, test.Question, test.Answers) { } 
        #endregion
    }
}