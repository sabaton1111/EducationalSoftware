using Android.App;
using Android.Widget;
using Plugin.Messaging;
using System.Net.Mail;

using System.Net;
using EducationalSoftware.Models;
using Android.Views;
using System.Collections.Generic;

namespace EducationalSoftware.Extensions
{
    public class PopupWindow
    {
        public delegate void Function();
        public delegate void ChangePassword(EditText editText);
        private Verification.VerifyRegistration verify = new Verification.VerifyRegistration();
        private FirebaseHelper helper = new FirebaseHelper();
        private Encryption encryption = new Encryption();
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

        /// <summary>
        /// Creating popup window with OK and Cancel buttons
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="function"></param>
        /// <param name="activity"></param>
        public void OnAlert(string title, string message, Function function, Activity activity)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {
                function();
            });
            alert.SetButton2("Cancel", (c, ev) => { });
            alert.Show();
        }
        public void OnForgottenPassword(string message, EditText email, Activity activity)
        {
            email = new EditText(activity);
            AlertDialog.Builder dialog = new AlertDialog.Builder(activity);
            AlertDialog alert = dialog.Create();
            alert.SetMessage(message);
            alert.SetView(email);
            alert.SetButton("OK", (c, ev) =>
            {
                try
                {
                    verify.VerifyEmail(email.Text);
                    var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                    {
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("educational.software.help@gmail.com", "educationalSoftware")
                    };
                    var message = new MailMessage("educational.software.help@gmail.com", email.Text, "Forgotten password", "Hello! It seems that you have forgotten your password. \n" + "Password:" +
                                encryption.DecodePassword(helper.GetLogin(email.Text).ConfigureAwait(false).GetAwaiter().GetResult().Password)
                                + "\n You can change your password anytime. Just go to Settings -> Change password\n" + "Have a nice day!");

                    // smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                    smtpClient.SendAsync(message, new object());
                    Alert("", "Email sent!", activity);

                }
                catch
                {
                    Alert("", "Invalid email!", activity);
                }

            });
            alert.SetButton2("Cancel", (c, ev) => { });
            alert.Show();
        }

        public void OnChangePassword(ChangePassword function, EditText newPassword, EditText confirmPassword, Activity activity)
        {
            newPassword = new EditText(activity);
            confirmPassword = new EditText(activity);
            newPassword.Hint = "Enter new password";
            confirmPassword.Hint = "Confirm password";
            newPassword.InputType = Android.Text.InputTypes.NumberVariationPassword;
            confirmPassword.InputType = Android.Text.InputTypes.NumberVariationPassword;
            AlertDialog.Builder dialog = new AlertDialog.Builder(activity);
            AlertDialog newPasswordAlert = dialog.Create();
            newPasswordAlert.SetMessage("Change password:");
            newPasswordAlert.SetView(newPassword);
            newPasswordAlert.SetButton2("Cancel", (c, ev) => { });
 
            newPasswordAlert.SetButton("OK", (c, ev) =>
            {
                AlertDialog.Builder dialogg = new AlertDialog.Builder(activity);
                AlertDialog confirmAlert = dialogg.Create();
                confirmAlert.SetMessage("Confirm password");
                confirmAlert.SetView(confirmPassword);
                confirmAlert.SetButton("OK", (c, ev) =>
                {
                    if(newPassword.Text == confirmPassword.Text)
                    {
                        function(newPassword);
                    }
                });
                confirmAlert.Show();

            });
            newPasswordAlert.Show();
        }
    }
}