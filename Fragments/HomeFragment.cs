using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using Firebase.Auth;

namespace EducationalSoftware.Fragments
{
    public class HomeFragment : Fragment
    {
        #region Home fragment data members
        private string emailAddress;
        private string token;
        #endregion

        #region Properties
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        #endregion

        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            GetToken();
            var bottomNavigation = view.FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigation.NavigationItemSelected += (s, e) =>
            {
                switch (e.Item.ItemId)
                {
                    case Resource.Id.action_home:
                        Toast.MakeText(Activity, "Home clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_statistics:
                        Toast.MakeText(Activity, "Stats clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_messages:
                        Toast.MakeText(Activity, "Msg clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_settings:
                        Toast.MakeText(Activity, "Settings clicked", ToastLength.Short).Show();
                        break;
                }
            };
            return view;
        }
        private void GetToken()
        {
            if (Token == "User")
            {
                alertWindow.Alert(EmailAddress, firebaseHelper.GetUser(EmailAddress).ConfigureAwait(false).GetAwaiter().GetResult().LastName, Activity);
            } else if(Token == "Admin")
            {
                alertWindow.Alert(EmailAddress, firebaseHelper.GetAdmin(EmailAddress).ConfigureAwait(false).GetAwaiter().GetResult().LastName, Activity);
            }
            else if(Token == "Teacher")
            {
                alertWindow.Alert(EmailAddress, firebaseHelper.GetTeacher(EmailAddress).ConfigureAwait(false).GetAwaiter().GetResult().LastName, Activity);
            }
            else if(Token == "UniversityStudent")
            {
                alertWindow.Alert(EmailAddress, firebaseHelper.GetUniversityStudent(EmailAddress).ConfigureAwait(false).GetAwaiter().GetResult().LastName, Activity);
            }
            else
            {
                alertWindow.Alert(EmailAddress, firebaseHelper.GetSchoolStudent(EmailAddress).ConfigureAwait(false).GetAwaiter().GetResult().LastName, Activity);
            }
        }

        #region Constructors
        public HomeFragment(string emailAddress, string token)
        {
            EmailAddress = emailAddress;
            Token = token;
        }
        public HomeFragment() : this("Empty", "Empty") { }
        public HomeFragment(HomeFragment fragment) : this(fragment.EmailAddress, fragment.Token) { }
        #endregion

    }
}