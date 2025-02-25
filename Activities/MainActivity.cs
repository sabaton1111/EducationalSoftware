﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using EducationalSoftware.Fragments;
using EducationalSoftware.Extensions;
using Android.Content.PM;
using static EducationalSoftware.Extensions.PopupWindow;

namespace EducationalSoftware
{
    
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/logo")]
    public class MainActivity : AppCompatActivity//, NavigationView.IOnNavigationItemSelectedListener
    {
     
        private Function function = null;
        private Android.Support.V4.App.FragmentManager fm;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V4.App.Fragment fragmentLogin = new LoginFragment();
            //Android.Support.V4.App.FragmentManager fm;
            fm = SupportFragmentManager;
            fm.BeginTransaction().Replace(Resource.Id.parent_fragment, fragmentLogin).Commit();
            //FragmentManager.BeginTransaction()
        }
        public override void OnBackPressed()
        {
            PopupWindow popup = new PopupWindow();
            if (fm.BackStackEntryCount < 1)
            {
                function += new Function(OnExit);
                popup.OnAlert("", "Are you sure you want to exit?", function, this);
            }
            else 
            {
                base.OnBackPressed();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void OnExit()
        {
            Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}

