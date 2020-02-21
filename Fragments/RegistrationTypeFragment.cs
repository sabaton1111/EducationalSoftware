using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;

namespace EducationalSoftware.Fragments
{
    public class RegistrationTypeFragment : Fragment
    {
        #region Data members
        private RadioGroup group;
        private RadioButton rbtnAdmin, rbtnSchoolStudent, rbtnTeacher, rbtnUniversityStudent, rbtnOther;
        private Button btnNext;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        private RegistrationRequest request = new RegistrationRequest();
        private bool exceptionCheck = true;
        #endregion
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_registration_type, container, false);

            #region Initializing fragment_registration_type objects
            group = view.FindViewById<RadioGroup>(Resource.Id.radioGroupRegTypes);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            rbtnAdmin = view.FindViewById<RadioButton>(Resource.Id.radioButtonAdmin);
            rbtnSchoolStudent = view.FindViewById<RadioButton>(Resource.Id.radioButtonScoolStudent);
            rbtnTeacher = view.FindViewById<RadioButton>(Resource.Id.radioButtonTeacher);
            rbtnUniversityStudent = view.FindViewById<RadioButton>(Resource.Id.radioButtonUniversityStudent);
            rbtnOther = view.FindViewById<RadioButton>(Resource.Id.radioButtonOther);
            #endregion

            btnNext.Click += BtnNext_Click;
            return view;
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            var androidID = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver,
            Android.Provider.Settings.Secure.AndroidId);
            try
            {
                request = firebaseHelper.GetRequest(androidID).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch
            {
                exceptionCheck = false;
            }
            #region Tokens to db

            if (rbtnAdmin.Checked == true)
            {
                if (exceptionCheck == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "Admin");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "Admin");
                }
            }
            else if (rbtnSchoolStudent.Checked)
            {
                if (exceptionCheck == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "SchoolStudent");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "SchoolStudent");
                }
            }
            else if (rbtnTeacher.Checked)
            {
                if (exceptionCheck == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "Teacher");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "Teacher");
                }
            }
            else if (rbtnUniversityStudent.Checked)
            {
                if (exceptionCheck == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "UniversityStudent");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "UniversityStudent");
                }
            }
            else
            {
                if (exceptionCheck == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "User");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "User");
                }
            } 
            #endregion

            Fragment registerOtherFragment = new RegisterFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerOtherFragment).Commit();

        }

    }
}