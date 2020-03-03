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
    public class AddTest : Fragment
    {
        private string user;
        public string User { get; }
        public string TestName { get; set; }
        public AddTest(string user)
        {
            User = user;
        }
        public AddTest()
        {

        }
        private Button btnAddQuestion;
        private EditText etTestName, etTestNotation, etNumberOfQuestions;
        private FirebaseTests helper = new FirebaseTests();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_add_test, container, false);

            #region Initialize fragment components
            btnAddQuestion = view.FindViewById<Button>(Resource.Id.btnAddQuestions);
            etTestName = view.FindViewById<EditText>(Resource.Id.editTextTestName);
            etTestNotation = view.FindViewById<EditText>(Resource.Id.editTextTestNotation);
            etNumberOfQuestions = view.FindViewById<EditText>(Resource.Id.editTextQuestionNumber); 
            #endregion

            btnAddQuestion.Click += OnAddQuestion;
            return view;
        }
        private async void OnAddQuestion(object sender, EventArgs e)
        {
            MultipleChoiceTest test = new MultipleChoiceTest(User, Int32.Parse(etNumberOfQuestions.Text), etTestName.Text, etTestNotation.Text);
            await helper.AddToFirebase(test,"Tests").ConfigureAwait(false);
            AddQuestions questions = new AddQuestions(user,etTestName.Text, Int32.Parse(etNumberOfQuestions.Text));
            FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, questions).Commit();

        }
    }
}