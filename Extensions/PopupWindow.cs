using Android.App;
using Android.Widget;
using Plugin.Messaging;
using System.Net.Mail;

using System.Net;

namespace EducationalSoftware.Extensions
{
    public class PopupWindow
    {
        public delegate void Function();
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
                        Credentials = new NetworkCredential("dimiter41@gmail.com", "sabaton1111")
                    };
                    var message = new MailMessage("dimiter41@gmail.com", "dimitar.d.velichkov@gmail.com", "Forgotten password", "Hello! It seems that you have forgotten your password. \n" + "Password:" +
                                encryption.DecodeServerName(helper.GetLogin(email.Text).ConfigureAwait(false).GetAwaiter().GetResult().Password)
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
    }
}