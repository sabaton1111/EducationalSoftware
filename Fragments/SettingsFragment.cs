using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using EducationalSoftware.Extensions;
using static EducationalSoftware.Extensions.PopupWindow;

namespace EducationalSoftware.Fragments
{
    public class SettingsFragment : Fragment
    {
        private Function function = null;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private SessionHelper session = new SessionHelper();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        private Button btnLogOut, btnDarkTheme, btnDelete, btnPasswordChange;

        #region SettingsFragment datamembers
        private string emailAddress;
        private string token;
        #endregion

        #region Properties
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        #endregion

        #region Constructors
        public SettingsFragment(string emailAddress, string token)
        {
            EmailAddress = emailAddress;
            Token = token;
        }
        public SettingsFragment() : this("Empty", "Empty") { }
        public SettingsFragment(SettingsFragment settings) : this(settings.EmailAddress, settings.Token) { } 
        #endregion
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_settings, container, false);

            #region Initializing components
            btnDarkTheme = view.FindViewById<Button>(Resource.Id.btnDarkTheme);
            btnLogOut = view.FindViewById<Button>(Resource.Id.btnLogOut);
            btnDelete = view.FindViewById<Button>(Resource.Id.btnDeleteAccount);
            btnPasswordChange = view.FindViewById<Button>(Resource.Id.btnChangePassword);
            #endregion

            btnDarkTheme.Click += OnDarkTheme;
            btnLogOut.Click += OnLogOut;
            btnDelete.Click += OnDeleteAccount;
            btnPasswordChange.Click += OnChangePassword;
            return view;
        }

        private async void OnChangePassword(object sender, EventArgs e)
        {
            switch(Token)
            {
                case "Admin":
                    //await firebaseHelper.UpdateAdminPassword(EmailAddress,"asd");
                    break;
                case "Teacher":
                    await firebaseHelper.DeleteTeacher(EmailAddress);
                    break;
                case "UniversityStudent":
                    await firebaseHelper.DeleteUniversityStudent(EmailAddress);
                    break;
                case "SchoolStudent":
                    await firebaseHelper.DeleteSchoolStudent(EmailAddress);
                    break;
                case "User":
                    await firebaseHelper.DeletePerson(EmailAddress);
                    break;
            }
        }

        public async void DeleteAccount()
        {
            switch (Token)
            {
                case "Admin":
                    await firebaseHelper.DeleteAdmin(EmailAddress);
                    break;
                case "Teacher":
                    await firebaseHelper.DeleteTeacher(EmailAddress);
                    break;
                case "UniversityStudent":
                    await firebaseHelper.DeleteUniversityStudent(EmailAddress);
                    break;
                case "SchoolStudent":
                    await firebaseHelper.DeleteSchoolStudent(EmailAddress);
                    break;
                case "User":
                    await firebaseHelper.DeletePerson(EmailAddress);
                    break;
            }    
            await session.DeleteSession(EmailAddress);
            await firebaseHelper.DeleteLogin(EmailAddress);
            alertWindow.Alert("", "Account deleted!", Activity);
            Fragment loginFragment = new LoginFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();
        }
        private void OnDeleteAccount(object sender, EventArgs e)
        {
                    function += new Function(DeleteAccount);
                    alertWindow.OnAlert("", "Are you sure you want to delete your account?", function, Activity);
        }
        private async void OnLogOut(object sender, EventArgs e)
        {
            await session.DeleteSession(EmailAddress);
            LoginFragment login = new LoginFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, login).Commit();
            alertWindow.Alert("", "Logout successful!", Activity);
        }
        private void OnDarkTheme(object sender, EventArgs e)
        {
            alertWindow.Alert("", "Coming soon!", Activity);
        }
    }

}