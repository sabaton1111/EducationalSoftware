using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class TestFragment : Android.Support.V4.App.Fragment
    {
        public event EventHandler<ButtonClickEventArgs> ButtonClick;
        List<TestQuestionPattern> lst;
        private Button btnAnswerOne, btnAnswerTwo, btnAnswerThree, btnAnswerFour;
        private TextView txtTestName, txtQuestion;
        private FirebaseTests tests = new FirebaseTests();

        #region HomeFragment datamembers
        private string emailAddress;
        private string token;
        #endregion

        #region Properties
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        #endregion

        private string testName;
        public string TestName { get; set; }
        public TestFragment(string testName, string emailAddress, string token)
        {
            TestName = testName;
            EmailAddress = emailAddress;
            Token = token;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_start_test, container, false);
            btnAnswerOne = view.FindViewById<Button>(Resource.Id.btnAnswerOne);
            btnAnswerTwo = view.FindViewById<Button>(Resource.Id.btnAnswerTwo);
            btnAnswerThree = view.FindViewById<Button>(Resource.Id.btnAnswerThree);
            btnAnswerFour = view.FindViewById<Button>(Resource.Id.btnAnswerFour);
            txtTestName = view.FindViewById<TextView>(Resource.Id.textViewStartTestName);
            txtQuestion = view.FindViewById<TextView>(Resource.Id.txtViewStartQuestion);
            GetData();
            //Toast.MakeText(Android.App.Application.Context, TestName, ToastLength.Short).Show();
            return view;
        }

        private async void GetData()
        {
            var items = await tests.GetAllQuestions<TestQuestionPattern>(TestName).ConfigureAwait(continueOnCapturedContext: false);
            lst = new List<TestQuestionPattern>(items);
            var shuffled = lst.OrderBy(a => Guid.NewGuid()).ToList();
            TestFunction(shuffled);
           
        }

        private void TestFunction(List<TestQuestionPattern> shuffled)
        {

            txtTestName.Text = TestName;
            txtQuestion.Text = shuffled.First().Question;
            btnAnswerOne.Text = shuffled.First().Answers[0];
            btnAnswerTwo.Text = shuffled.First().Answers[1];
            btnAnswerThree.Text = shuffled.First().Answers[2];
            btnAnswerFour.Text = shuffled.First().Answers[3];

            btnAnswerOne.Click -= delegate { };
            btnAnswerOne.Click += delegate
            {
                OnButtonClick(new ButtonClickEventArgs(btnAnswerOne, shuffled));
            };
            btnAnswerTwo.Click -= delegate { };
            btnAnswerTwo.Click += delegate
            {
                OnButtonClick(new ButtonClickEventArgs(btnAnswerTwo, shuffled));
            };
            btnAnswerThree.Click -= delegate { };
            btnAnswerThree.Click += delegate
            {
                OnButtonClick(new ButtonClickEventArgs(btnAnswerThree, shuffled));
            };
            btnAnswerFour.Click -= delegate { };
            btnAnswerFour.Click += delegate
            {
                OnButtonClick(new ButtonClickEventArgs(btnAnswerFour, shuffled));
            };

        }
        private void OnButtonClick(ButtonClickEventArgs buttonClickEventArgs)
        {
            try
            {
                if (buttonClickEventArgs.ButtonClicked.Text == buttonClickEventArgs.Lst.First().CorrectAnswer)
                {
                    Toast.MakeText(Android.App.Application.Context, "Correct", ToastLength.Short).Show();
                    buttonClickEventArgs.Lst.RemoveAt(0);

                    TestFunction(buttonClickEventArgs.Lst);
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context, "Incorrect", ToastLength.Short).Show();
                    buttonClickEventArgs.Lst.RemoveAt(0);

                    TestFunction(buttonClickEventArgs.Lst);
                }
            }
            catch
            {
                //TODO: Email address is empty
                FragmentManager.PopBackStack();
                Android.Support.V4.App.Fragment fragment = new HomeFragment(EmailAddress, Token);
                FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).Commit();

                return;
            }

        }
    }
    public class ButtonClickEventArgs : EventArgs
    {
        public Button ButtonClicked { get; set; }
        public List<TestQuestionPattern> Lst { get; set; }

        public ButtonClickEventArgs(Button buttonClicked, List<TestQuestionPattern> lst)
        {
            ButtonClicked = buttonClicked;
            Lst = lst;
        }
    }

}