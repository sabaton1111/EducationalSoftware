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
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;

namespace EducationalSoftware.Fragments
{
    public class AddQuestions : AddTest
    {
        private string testName;
        private int numberOfQuestions;
        private int countQuestions = 0;
        private TextView txtTestName, txtQuestions;
        private EditText etQuestion, etAnswerOne, etAnswerTwo, etAnswerThree, etAnswerFour;
        private Button btnAdd;
        private FirebaseTests helper = new FirebaseTests();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();

        public string TestName { get; set; }
        public int NumberOfQuestions { get; set; }

        public AddQuestions(string user, string testName, int numberOfQuestions) : base(user)
        {
            TestName = testName;
            NumberOfQuestions = numberOfQuestions;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.add_question_fragment, container, false);
            txtTestName = view.FindViewById<TextView>(Resource.Id.textViewTestName);
            txtTestName.Text = TestName;
            txtQuestions = view.FindViewById<TextView>(Resource.Id.textViewQuestions);
            etQuestion = view.FindViewById<EditText>(Resource.Id.editTextQuestion);
            etAnswerOne = view.FindViewById<EditText>(Resource.Id.editTextAnswerOne);
            etAnswerTwo = view.FindViewById<EditText>(Resource.Id.editTextAnswerTwo);
            etAnswerThree = view.FindViewById<EditText>(Resource.Id.editTextAnswerThree);
            etAnswerFour = view.FindViewById<EditText>(Resource.Id.editTextAnswerFour);
            btnAdd = view.FindViewById<Button>(Resource.Id.btnAddQuestions);

            btnAdd.Click += OnAddQuestion;

            return view;
        }

        private async void OnAddQuestion(object sender, EventArgs e)
        {
            countQuestions++;
            if (countQuestions <= NumberOfQuestions)
            {
                string[] answers = new string[] { etAnswerOne.Text, etAnswerTwo.Text, etAnswerThree.Text, etAnswerFour.Text };
                TestQuestionPattern pattern = new TestQuestionPattern(txtTestName.Text, etQuestion.Text, answers);
                await helper.AddToFirebaseQuestions(pattern, "TestQuestions",txtTestName.Text);
                txtQuestions.Text = countQuestions.ToString();
                etQuestion.Text = "";
                etAnswerOne.Text = "";
                etAnswerTwo.Text = "";
                etAnswerThree.Text = "";
                etAnswerFour.Text = "";
                alertWindow.Alert("", ("Question" + countQuestions.ToString() + "added!"), Activity);
                
            }
            else
            {
                alertWindow.Alert("","Questions added successfully!", Activity);
                AddTest selectedFragment = new AddTest(User);
                FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, selectedFragment).Commit();
            }
        }
    }
}