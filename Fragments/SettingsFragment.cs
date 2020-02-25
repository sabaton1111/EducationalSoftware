using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using static EducationalSoftware.Extensions.PopupWindow;

namespace EducationalSoftware.Fragments
{
    public class SettingsFragment : Fragment
    {
        private Function function = null;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private SessionHelper session = new SessionHelper();
        private Button btnLogOut, btnDarkTheme, btnDelete;
        private string emailAddress;
        private string token;
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public SettingsFragment(string emailAddress, string token)
        {
            EmailAddress = emailAddress;
            Token = token;
        }
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_settings, container, false);
            btnDarkTheme = view.FindViewById<Button>(Resource.Id.btnDarkTheme);
            btnLogOut = view.FindViewById<Button>(Resource.Id.btnLogOut);
            btnDelete = view.FindViewById<Button>(Resource.Id.btnDeleteAccount);
            btnDarkTheme.Click += OnDarkTheme;
            btnLogOut.Click += OnLogOut;
            btnDelete.Click += OnDeleteAccount;
            return view;
        }

        public async void DeleteAccount()
        {
            await firebaseHelper.DeleteAdmin(EmailAddress);
            await session.DeleteSession(EmailAddress);
            await firebaseHelper.DeleteLogin(EmailAddress);
            alertWindow.Alert("", "Account deleted!", Activity);
            Fragment loginFragment = new LoginFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();
        }

        private void OnDeleteAccount(object sender, EventArgs e)
        {
            if (Token == "Admin")
            {
                function += new Function(DeleteAccount);
                alertWindow.OnAlert("", "Are you sure you want to delete your account?", function, Activity);
            }
        }
        private async void OnLogOut(object sender, EventArgs e)
        {

            await session.DeleteSession(EmailAddress);
            LoginFragment login = new LoginFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, login).Commit();
        }
        private void OnDarkTheme(object sender, EventArgs e)
        {


        }
    }

}