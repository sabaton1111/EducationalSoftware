using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;
using EducationalSoftware.Verification;

namespace EducationalSoftware.Fragments
{
    public class LoginFragment : Fragment
    {
        #region Fragment components
        private TextView txtForgottenPassword;
        private Android.Widget.Button btnRegister, btnLogin;
        private EditText etEmail, etPassword, etPopUp; 
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
            btnLogin.Enabled = true; 
            #endregion

            btnRegister.Click += OnRegister_Click;
            btnLogin.Click += OnLogin_Click;
            txtForgottenPassword.Click += OnForgottenPassword;

            return view;
        }
        private void OnForgottenPassword(object sender, EventArgs e)
        {
            alertWindow.OnForgottenPassword("Type your email", etPopUp, Activity);
        }
        private void CheckSession()
        {
            string androidID = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver,
           Android.Provider.Settings.Secure.AndroidId);
            try
            {
                Session item = session.GetSession(androidID).ConfigureAwait(false).GetAwaiter().GetResult();
                Fragment homeFragment = new HomeFragment(item.Email, item.Token);
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, homeFragment).Commit();
            }
            catch { }
        }
        private void OnRegister_Click(object sender, EventArgs e)
        {
            if(verify.CheckInternetConnection() == false)
            {
                alertWindow.Alert("", "No internet connection!", Activity);
            }
            else
            {
                Fragment registerFragment = new RegistrationTypeFragment();
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
                            Fragment homeFragment = new HomeFragment(login.Email, login.Token);
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