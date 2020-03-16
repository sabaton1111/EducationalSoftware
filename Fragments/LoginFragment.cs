using System;
using System.Net;
using System.Net.Mail;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;
using EducationalSoftware.Verification;
using static Android.App.ActionBar;

namespace EducationalSoftware.Fragments
{
    public class LoginFragment : Android.Support.V4.App.Fragment
    {
        #region Fragment components
        private TextView txtForgottenPassword;
        private Android.Widget.Button btnRegister, btnLogin, btnPopupCancel, btnPopOk;
        private EditText etEmail, etPassword, etPopUp;
        private ImageView imageView;
        private Dialog popupDialog;
        #endregion

        #region Helpers
        private Encryption encryption = new Encryption();
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        private SessionHelper session = new SessionHelper();
        private VerifyRegistration verify = new VerifyRegistration();
        #endregion
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            CheckSession();
            View view = inflater.Inflate(Resource.Layout.fragment_login, container, false);

            #region Initialize fragment components
            btnRegister = view.FindViewById<Button>(Resource.Id.btnRegister);
            btnLogin = view.FindViewById<Android.Widget.Button>(Resource.Id.btnLogin);
            etEmail = view.FindViewById<EditText>(Resource.Id.editTextEmail);
            etPassword = view.FindViewById<EditText>(Resource.Id.editTextPassword);
            txtForgottenPassword = view.FindViewById<TextView>(Resource.Id.txtViewForgottenPass);
            imageView = view.FindViewById<ImageView>(Resource.Id.imgView);
            //Glide
            //   .With(Android.App.Application.Context)
            //   .Load("gs://educationalsoftware-ba7e4.appspot.com/dimiter41@gmail.coz/t")
            //   .Into(imageView);
            btnLogin.Enabled = true;
            #endregion

            btnRegister.Click += OnRegister_Click;
            btnLogin.Click += OnLogin_Click;
            txtForgottenPassword.Click += OnForgottenPassword;

            return view;
        }
        private void OnForgottenPassword(object sender, EventArgs e)
        {
            popupDialog = new Dialog(Activity);
            popupDialog.SetContentView(Resource.Layout.popup);
            popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupDialog.Show();
            popupDialog.Window.SetLayout(LayoutParams.MatchParent, LayoutParams.WrapContent);
            popupDialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);

            // Accessing Popup layout fields  
            btnPopupCancel = popupDialog.FindViewById<Button>(Resource.Id.btnCancel);
            btnPopOk = popupDialog.FindViewById<Button>(Resource.Id.btnOk);
            etPopUp = popupDialog.FindViewById<EditText>(Resource.Id.etEmailForgotPassword);

            // Events for buttons  
            btnPopupCancel.Click += BtnPopupCancel_Click;
            btnPopOk.Click += BtnPopOk_Click;
        }

        private void BtnPopOk_Click(object sender, EventArgs e)
        {
            try
            {
                verify.VerifyEmail(etPopUp.Text);
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("educational.software.help@gmail.com", "educationalSoftware")
                };
                var message = new MailMessage("educational.software.help@gmail.com", etPopUp.Text, "Forgotten password", "Hello! It seems that you have forgotten your password. \n" + "Password:" +
                            encryption.DecodePassword(firebaseHelper.GetLogin(etPopUp.Text).ConfigureAwait(false).GetAwaiter().GetResult().Password)
                            + "\n You can change your password anytime. Just go to Settings -> Change password\n" + "Have a nice day!");

                // smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                smtpClient.SendAsync(message, new object());
                alertWindow.Alert("", "Email sent!", Activity);

            }
            catch
            {
                alertWindow.Alert("", "Invalid email!", Activity);
            }
        }

        private void BtnPopupCancel_Click(object sender, EventArgs e)
        {
            popupDialog.Dismiss();
            popupDialog.Hide();
        }

        private void CheckSession()
        {
            string androidID = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver,
           Android.Provider.Settings.Secure.AndroidId);
            try
            {
                Session item = session.GetSession(androidID).ConfigureAwait(false).GetAwaiter().GetResult();
                Android.Support.V4.App.Fragment homeFragment = new HomeFragment(item.Email, item.Token);
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, homeFragment).Commit();
            }
            catch { }
        }
        private void OnRegister_Click(object sender, EventArgs e)
        {
            if (verify.CheckInternetConnection() == false)
            {
                alertWindow.Alert("", "No internet connection!", Activity);
            }
            else
            {
                Android.Support.V4.App.Fragment registerFragment = new RegistrationTypeFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerFragment).AddToBackStack(null).Commit();
            }
        }
        private async void OnLogin_Click(object sender, EventArgs e)
        {
            if (verify.CheckInternetConnection() == false)
            {
                alertWindow.Alert("", "No internet connection!", Activity);
            }
            else
            {
                //Preventing from multiple click
                btnLogin.Enabled = false;
                try
                {
                    //Getting id
                    var androidID = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver,
              Android.Provider.Settings.Secure.AndroidId);
                    Login login = firebaseHelper.GetLogin(etEmail.Text).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (encryption.DecodePassword(login.Password) == etPassword.Text)
                    {
                        //Successful login
                        await session.AddSession(androidID, login.Email, login.Token);
                        Android.Support.V4.App.Fragment homeFragment = new HomeFragment(login.Email, login.Token);
                        FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, homeFragment).Commit();
                    }
                    else
                    {
                        alertWindow.Alert("Error!", "Incorrect email or password!", Activity);
                        btnLogin.Enabled = true;
                    }
                }
                catch
                {
                    alertWindow.Alert("Error!", "Incorrect email or password!", Activity);
                    btnLogin.Enabled = true;
                }
            }
        }
    }
}