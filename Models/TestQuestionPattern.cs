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
        private string testName;
        private string question;
        private string[] answers;

        public string TestName { get; private set; }
        public string Question { get; set; }
        public string[] Answers { get; set; }

        public TestQuestionPattern(string testName, string question, string[] answers)
        {
            TestName = testName;
            Question = question;
            Answers = answers;
        }

        public TestQuestionPattern() : this("Empty", "Empty", null)
        {

        }

        public TestQuestionPattern(TestQuestionPattern test) : this (test.TestName, test.Question, test.Answers)
        {

        }
    }
}