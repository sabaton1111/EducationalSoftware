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
        const int LENGTH = 5;
        private int numberOfQuestions;
        private string[] questionAnswers;
        private List<string[]> test;
        public int NumberOfQuestions
        {
            get
            {
                return numberOfQuestions;
            }
            set
            {
                numberOfQuestions = value;
            }
        }
        public string[] QuestionAnswers
        { 
            get 
            { 
                return questionAnswers; 
            } 
            set 
            {
                questionAnswers = new string[LENGTH];
                questionAnswers = value; 
            } 
        }
        public List<string[]> Test { get; set; }

        public MultipleChoiceTest(int numberOfQuestions, string[] questionAnswers, List<string[]> test)
        {
            NumberOfQuestions = numberOfQuestions;
            QuestionAnswers = questionAnswers;
            Test = test;
        }

        public MultipleChoiceTest() : this(0, null, null) { }

        public MultipleChoiceTest(MultipleChoiceTest choiceTest) : this (choiceTest.NumberOfQuestions, choiceTest.QuestionAnswers,
            choiceTest.Test) { }
    }
}