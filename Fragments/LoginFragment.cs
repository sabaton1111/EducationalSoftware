﻿using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;
namespace EducationalSoftware.Fragments
{
    public class LoginFragment : Fragment
    {
        private Android.Widget.Button btnRegister, btnLogin;
        private EditText etEmail, etPassword;
        private Encryption encryption = new Encryption();
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_login, container, false);
            btnRegister = view.FindViewById<Button>(Resource.Id.btnRegister);
            btnLogin = view.FindViewById<Android.Widget.Button>(Resource.Id.btnLogin);
            etEmail = view.FindViewById<EditText>(Resource.Id.editTextEmail);
            etPassword = view.FindViewById<EditText>(Resource.Id.editTextPassword);
            btnRegister.Click += OnRegister_Click;
            btnLogin.Click += OnLogin_Click;
            return view;
        }
        private void OnRegister_Click(object sender, EventArgs e)
        {
            Fragment registerFragment = new RegistrationTypeFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerFragment).AddToBackStack(null).Commit();
        }
        private async void OnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Login login = firebaseHelper.GetLogin(etEmail.Text).ConfigureAwait(false).GetAwaiter().GetResult();
                //User user = firebaseHelper.GetUser(etEmail.Text).ConfigureAwait(false).GetAwaiter().GetResult();
                if (encryption.DecodeServerName(login.Password) == etPassword.Text)
                {
                    var item = firebaseHelper.GetLogin(etEmail.Text).ConfigureAwait(false).GetAwaiter().GetResult();
                    Fragment homeFragment = new HomeFragment(item.Email, item.Token);
                    FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, homeFragment).Commit();
                }
                else
                {
                    alertWindow.Alert("Error!", "Incorrect email or password!", Activity);
                }
            }
            catch
            {
                alertWindow.Alert("Error!", "Incorrect email or password!", Activity);
            }
        }
    }
}