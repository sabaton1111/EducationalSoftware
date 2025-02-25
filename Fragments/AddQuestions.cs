﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;

namespace EducationalSoftware.Fragments
{
    public class AddQuestions : AddTest
    {
        #region Datamembers
        private string testName;
        private int numberOfQuestions;
        #endregion
        private string correctAnswer;
        private int countQuestions = 0;
        private TextView txtTestName, txtQuestions;
        private EditText etQuestion, etAnswerOne, etAnswerTwo, etAnswerThree, etAnswerFour;
        private RadioGroup answerGroup;
        private Button btnAdd;
        private FirebaseTests firebase = new FirebaseTests();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();

        #region Properties
        public string TestName { get; set; }
        public int NumberOfQuestions { get; set; } 
        #endregion

        #region Constructor
        public AddQuestions(string user, string testName, int numberOfQuestions) : base(user)
        {
            TestName = testName;
            NumberOfQuestions = numberOfQuestions;
        } 
        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.add_question_fragment, container, false);

            #region Initialize fragment components
            answerGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroupRegCorrectAnswer);
            txtTestName = view.FindViewById<TextView>(Resource.Id.textViewTestName);
            txtTestName.Text = TestName;
            txtQuestions = view.FindViewById<TextView>(Resource.Id.textViewQuestions);
            etQuestion = view.FindViewById<EditText>(Resource.Id.editTextQuestion);
            etAnswerOne = view.FindViewById<EditText>(Resource.Id.editTextAnswerOne);
            etAnswerTwo = view.FindViewById<EditText>(Resource.Id.editTextAnswerTwo);
            etAnswerThree = view.FindViewById<EditText>(Resource.Id.editTextAnswerThree);
            etAnswerFour = view.FindViewById<EditText>(Resource.Id.editTextAnswerFour);
            btnAdd = view.FindViewById<Button>(Resource.Id.btnAddQuestions); 
            #endregion

            btnAdd.Click += OnAddQuestion;
            return view;
        }
        private async void OnAddQuestion(object sender, EventArgs e)
        {
            //TODO: Fix counter
            countQuestions++;
            if (countQuestions < NumberOfQuestions)
            {
                string[] answers = new string[] { etAnswerOne.Text, etAnswerTwo.Text, etAnswerThree.Text, etAnswerFour.Text };
                int id = answerGroup.CheckedRadioButtonId;
                switch (id)
                {
                    case Resource.Id.rbtnAnswerOne:
                        correctAnswer = etAnswerOne.Text;
                        break;
                    case Resource.Id.rbtnAnswerTwo:
                        correctAnswer = etAnswerTwo.Text;
                        break;
                    case Resource.Id.rbtnAnswerThree:
                        correctAnswer = etAnswerThree.Text;
                        break;
                    case Resource.Id.rbtnAnswerFour:
                        correctAnswer = etAnswerFour.Text;
                        break;
                }
                TestQuestionPattern pattern = new TestQuestionPattern(txtTestName.Text, etQuestion.Text, answers, correctAnswer);
                await firebase.AddToFirebaseQuestions(pattern, "TestQuestions",txtTestName.Text);
                txtQuestions.Text = countQuestions.ToString();
                etQuestion.Text = "";
                etAnswerOne.Text = "";
                etAnswerTwo.Text = "";
                etAnswerThree.Text = "";
                etAnswerFour.Text = "";
                correctAnswer = "";
                Toast.MakeText(Activity, ("Question " + countQuestions.ToString() + " added!"), ToastLength.Short).Show();

            }
            else
            {
                string[] answers = new string[] { etAnswerOne.Text, etAnswerTwo.Text, etAnswerThree.Text, etAnswerFour.Text };
                int id = answerGroup.CheckedRadioButtonId;
                switch (id)
                {
                    case Resource.Id.rbtnAnswerOne:
                        correctAnswer = etAnswerOne.Text;
                        break;
                    case Resource.Id.rbtnAnswerTwo:
                        correctAnswer = etAnswerTwo.Text;
                        break;
                    case Resource.Id.rbtnAnswerThree:
                        correctAnswer = etAnswerThree.Text;
                        break;
                    case Resource.Id.rbtnAnswerFour:
                        correctAnswer = etAnswerFour.Text;
                        break;
                }
                TestQuestionPattern pattern = new TestQuestionPattern(txtTestName.Text, etQuestion.Text, answers, correctAnswer);
                await firebase.AddToFirebaseQuestions(pattern, "TestQuestions", txtTestName.Text);

                alertWindow.Alert("","Questions added successfully!", Activity);
                AddTest selectedFragment = new AddTest(User);
                FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, selectedFragment).Commit();
            }
        }
    }
}