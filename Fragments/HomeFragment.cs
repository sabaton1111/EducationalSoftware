﻿using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;

namespace EducationalSoftware.Fragments
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        #region HomeFragment datamembers
        private string emailAddress;
        private string token;
        #endregion

        #region Properties
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        #endregion

        #region Constructors
        public HomeFragment(string emailAddress, string token)
        {
            EmailAddress = emailAddress;
            Token = token;
        }
        public HomeFragment() : this("Empty", "Empty") { }
        public HomeFragment(HomeFragment fragment) : this(fragment.EmailAddress, fragment.Token) { }
        #endregion

        private BottomNavigationView bottomNavigation;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            bottomNavigation = view.FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            if (Token == "Teacher")
            {
                LoadTeacherNavigation();
            }
            else
            {
                LoadNavigation();
            }
            Android.Support.V4.App.Fragment fragment = new TestsRecyclerView(EmailAddress, Token);
            FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).Commit();

            return view;
        }
        private void LoadTeacherNavigation()
        {
            bottomNavigation.NavigationItemSelected += (s, e) =>
            {
                Android.Support.V4.App.Fragment selectedFragment = null;
                switch (e.Item.ItemId)
                {
                    case Resource.Id.action_home:
                        selectedFragment = new TestsRecyclerView(EmailAddress, Token);
                        Toast.MakeText(Activity, "Home clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_statistics:
                        selectedFragment = new MyProfile(EmailAddress,Token);
                        Toast.MakeText(Activity, "Stats clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_test:
                        selectedFragment = new AddTest(EmailAddress);
                        Toast.MakeText(Activity, "Test clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_messages:
                        selectedFragment = new Android.Support.V4.App.Fragment();
                        Toast.MakeText(Activity, "Msg clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_settings:
                        Toast.MakeText(Activity, "Settings clicked", ToastLength.Short).Show();
                        selectedFragment = new SettingsFragment(EmailAddress, Token);
                        break;
                }
                FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, selectedFragment).Commit();
            };
        }
        private void LoadNavigation()
        {
            //Deleting default navigation(teacher navigation) and setting up the new one
            bottomNavigation.Menu.Clear();
            bottomNavigation.Menu.Add(1, Resource.Id.action_home, 0, "Home");
            bottomNavigation.Menu.Add(1, Resource.Id.action_statistics, 1, "Statistics");
            bottomNavigation.Menu.Add(1, Resource.Id.action_messages, 2, "Message");
            bottomNavigation.Menu.Add(1, Resource.Id.action_settings, 3, "Settings");
            bottomNavigation.Menu.GetItem(0).SetIcon(EducationalSoftware.Resource.Drawable.home_icon);
            bottomNavigation.Menu.GetItem(1).SetIcon(EducationalSoftware.Resource.Drawable.statistic_icon);
            bottomNavigation.Menu.GetItem(2).SetIcon(EducationalSoftware.Resource.Drawable.message_icon);
            bottomNavigation.Menu.GetItem(3).SetIcon(EducationalSoftware.Resource.Drawable.settings_icon);
            bottomNavigation.Menu.SetGroupEnabled(1, true);
            bottomNavigation.NavigationItemSelected += (s, e) =>
            {
                Android.Support.V4.App.Fragment selectedFragment = null;
                switch (e.Item.ItemId)
                {
                    case Resource.Id.action_home:
                        selectedFragment = new TestsRecyclerView(EmailAddress, Token);
                        Toast.MakeText(Activity, "Home clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_statistics:
                        selectedFragment = new MyProfile(EmailAddress, Token);
                        Toast.MakeText(Activity, "Stats clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_messages:
                        selectedFragment = new Android.Support.V4.App.Fragment();
                        Toast.MakeText(Activity, "Msg clicked", ToastLength.Short).Show();
                        break;
                    case Resource.Id.action_settings:
                        Toast.MakeText(Activity, "Settings clicked", ToastLength.Short).Show();
                        selectedFragment = new SettingsFragment(EmailAddress, Token);
                        break;
                }
                FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, selectedFragment).Commit();
            };
        }
    }
}