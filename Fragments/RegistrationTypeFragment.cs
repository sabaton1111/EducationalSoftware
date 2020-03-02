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
                    RegistrationRequest request = new RegistrationRequest(androidID, "Admin");
                    await firebaseHelper.AddToFirebase<RegistrationRequest>(request,"Request");
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
                    RegistrationRequest request = new RegistrationRequest(androidID, "SchoolStudent");
                    await firebaseHelper.AddToFirebase<RegistrationRequest>(request, "Request");
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
                    RegistrationRequest request = new RegistrationRequest(androidID, "Teacher");
                    await firebaseHelper.AddToFirebase<RegistrationRequest>(request, "Request");
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
                    RegistrationRequest request = new RegistrationRequest(androidID, "UniversityStudent");
                    await firebaseHelper.AddToFirebase<RegistrationRequest>(request, "Request");
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
                    RegistrationRequest request = new RegistrationRequest(androidID, "User");
                    await firebaseHelper.AddToFirebase<RegistrationRequest>(request, "Request");
                }
            } 
            #endregion
            Fragment registerOtherFragment = new RegisterFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerOtherFragment).Commit();
        }
    }
}