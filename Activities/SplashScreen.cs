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

namespace EducationalSoftware.Activities
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, Icon ="@mipmap/logo")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

                    StartActivity(typeof(MainActivity));

         //   StartActivity(typeof(MainActivity));
        }
    }
}