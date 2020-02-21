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
            RegistrationRequest request = new RegistrationRequest();
            bool check = true;
            try
            {
                request = firebaseHelper.GetRequest(androidID).ConfigureAwait(false).GetAwaiter().GetResult();

            }
            catch
            {
                check = false;
            }

            if (rbtnAdmin.Checked == true)
            {
                if(check == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "Admin");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "Admin");

                }
                
                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();

            }
            else if(rbtnSchoolStudent.Checked)
            {
                if (check == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "SchoolStudent");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "SchoolStudent");

                }
                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();

            }
            else if(rbtnTeacher.Checked)
            {
                if (check == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "Teacher");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "Teacher");

                }
                //Fragment registerTeacher = new RegisterTeacherFragment();
                //FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerTeacher).Commit();

            }
            else if(rbtnUniversityStudent.Checked)
            {
                if (check == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "UniversityStudent");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "UniversityStudent");

                }
                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();

            }
            else
            {
                if (check == true)
                {
                    await firebaseHelper.UpdateRequest(androidID, "User");
                }
                else
                {
                    await firebaseHelper.AddRequest(androidID, "User");

                }
                Fragment registerOtherFragment = new RegisterOtherFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerOtherFragment).Commit();

            }

        }

    }
}