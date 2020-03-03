using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using EducationalSoftware.Extensions;
using static EducationalSoftware.Extensions.PopupWindow;
using EducationalSoftware.Models;

namespace EducationalSoftware.Fragments
{
    public class SettingsFragment : Fragment
    {
        private EditText newPass, confirm;
        private Function function = null;
        private ChangePassword changePassword = null;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private SessionHelper session = new SessionHelper();
        private Encryption encryption = new Encryption();
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

        public async void ChangePassword(EditText newPass)
        {
            //TODO: Change token signature
            switch (Token)
            {
                case "Admin":
                    //Update in Admins
                    Admin admin = await firebaseHelper.GetData<Admin>(Token + "s", EmailAddress);
                    admin.Password = encryption.EncodePassword(newPass.Text);
                    await firebaseHelper.UpdatePassword(admin, Token + "s", EmailAddress);
                    break;
                case "User":
                    //Update in Users
                    User user = await firebaseHelper.GetData<User>(Token + "s", EmailAddress);
                    user.Password = encryption.EncodePassword(newPass.Text);
                    await firebaseHelper.UpdatePassword(user, Token + "s", EmailAddress);
                    break;
                case "Teacher":
                    //Update in Teachers
                    Teacher teacher = await firebaseHelper.GetData<Teacher>(Token + "s", EmailAddress);
                    teacher.Password = encryption.EncodePassword(newPass.Text);
                    await firebaseHelper.UpdatePassword(teacher, Token + "s", EmailAddress);
                    break;
                case "SchoolStudent":
                    //Update in SchoolStudents
                    SchoolStudent schoolStudent = await firebaseHelper.GetData<SchoolStudent>(Token + "s", EmailAddress);
                    schoolStudent.Password = encryption.EncodePassword(newPass.Text);
                    await firebaseHelper.UpdatePassword(schoolStudent, Token + "s", EmailAddress);
                    break;
                case "UniversityStudent":
                    //Update in UniversityStudents
                    UniversityStudent universityStudent = await firebaseHelper.GetData<UniversityStudent>(Token + "s", EmailAddress);
                    universityStudent.Password = encryption.EncodePassword(newPass.Text);
                    await firebaseHelper.UpdatePassword(universityStudent, Token + "s", EmailAddress);
                    break;
            }

            //Update in Login
            Login login = await firebaseHelper.GetLogin(EmailAddress);
            login.Password = encryption.EncodePassword(newPass.Text);
            await firebaseHelper.UpdatePassword<Login>(login, "Login", EmailAddress);
        }
        private async void OnChangePassword(object sender, EventArgs e)
        {
            changePassword += new ChangePassword(ChangePassword);
            alertWindow.OnChangePassword(changePassword, newPass, confirm, Activity);
        }

        public async void DeleteAccount()
        {
            switch (Token)
            {
                case "Admin":
                    await firebaseHelper.DeleteAccount<Admin>("Admins", EmailAddress);
                    break;
                case "Teacher":
                    await firebaseHelper.DeleteAccount<Teacher>("Teachers", EmailAddress);
                    break;
                case "UniversityStudent":
                    await firebaseHelper.DeleteAccount<UniversityStudent>("UniversityStudents", EmailAddress);
                    break;
                case "SchoolStudent":
                    await firebaseHelper.DeleteAccount<SchoolStudent>("SchoolStudents", EmailAddress);
                    break;
                case "User":
                    await firebaseHelper.DeleteAccount<User>("Users", EmailAddress);
                    break;
            }
            await session.DeleteSession(EmailAddress);
            await firebaseHelper.DeleteAccount<Login>("Login", EmailAddress);
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