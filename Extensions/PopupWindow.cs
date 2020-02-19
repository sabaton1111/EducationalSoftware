using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using MySql.Data.MySqlClient;
using Xamarin.Forms;

namespace EducationalSoftware.Extensions
{
    public class PopupWindow
    {

        public void Alert(string title, string message, Activity activity)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            //alert.SetIcon(Resource.Drawable.alert);
            alert.SetButton("OK", (c, ev) => { });
            alert.Show();
        }

       
        public void OnExitAlert(string title, string message, Activity activity)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            });
            alert.SetButton2("Cancel", (c, ev) => { });
            alert.Show();
        }
    }
}