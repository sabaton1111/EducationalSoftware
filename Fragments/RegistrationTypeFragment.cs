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

        #region Fragment components
        private RadioGroup group;
        private RadioButton rbtnAdmin, rbtnSchoolStudent, rbtnTeacher, rbtnUniversityStudent, rbtnOther;
        private Button btnNext;
        #endregion

        #region Helpers
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        private RegistrationRequest regRequest = new RegistrationRequest();
        #endregion

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
        private void BtnNext_Click(object sender, EventArgs e)
        {
            #region Tokens to db
            int id = group.CheckedRadioButtonId;
            switch (id)
            {
                case Resource.Id.radioButtonAdmin:
                    MakeRequest("Admin");
                    break;
                case Resource.Id.radioButtonScoolStudent:
                    MakeRequest("SchoolStudent");
                    break;
                case Resource.Id.radioButtonTeacher:
                    MakeRequest("Teacher");
                    break;
                case Resource.Id.radioButtonUniversityStudent:
                    MakeRequest("UniversityStudent");
                    break;
                case Resource.Id.radioButtonOther:
                    MakeRequest("User");
                    break;
            }
            #endregion
        }
        public async void MakeRequest(string token)
        {
            //Getting device ID
            var androidID = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver,
              Android.Provider.Settings.Secure.AndroidId);
            try
            {
                //If there is previous request from the same device, updating request
                regRequest = firebaseHelper.GetRequest(androidID).ConfigureAwait(false).GetAwaiter().GetResult();
                await firebaseHelper.UpdateRequest(androidID, token);
            }
            catch
            {
                //Create request
                RegistrationRequest request = new RegistrationRequest(androidID, token);
                await firebaseHelper.AddToFirebase<RegistrationRequest>(request, "Request");
            }
            //Loading registration form
            Fragment registerOtherFragment = new RegisterFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerOtherFragment).AddToBackStack(null).Commit();
        }
    }
}
