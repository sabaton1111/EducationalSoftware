using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Verification;
using EducationalSoftware.Extensions;

namespace EducationalSoftware.Fragments
{
    public class RegisterFragment : Fragment
    {
        private VerifyRegistration verify = new VerifyRegistration();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        private EditText etFirstName, etLastName, etEmail, etAge; 
        private Android.Widget.Button btnRegister;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register, container, false);

            //Initializing editText boxes and register button
            etFirstName = view.FindViewById<EditText>(Resource.Id.editTextFirstName);
            etLastName = view.FindViewById<EditText>(Resource.Id.editTextLastName);
            etEmail = view.FindViewById<EditText>(Resource.Id.editTextEmail);
            etAge = view.FindViewById<EditText>(Resource.Id.editTextAge);
         
            btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnRegisterUser);
            btnRegister.Click += AddUser_Click;
            return view;
        }

        private async void AddUser_Click(object sender, EventArgs e)
        {
            #region Verify data
            if (verify.VerifyName(etFirstName.Text) == false)
            {
                alertWindow.Alert("Error!", "Please enter valid First name!", Activity);
                return;
            }

            if (verify.VerifyName(etLastName.Text) == false)
            {
                alertWindow.Alert("Error!", "Please enter valid Last name!", Activity);
                return;
            }

            if (verify.VerifyEmail(etEmail.Text) == false)
            {
                alertWindow.Alert("Error!", "Email is wrong or already taken!", Activity);
                return;
            } 
            #endregion

            await firebaseHelper.AddUser(etFirstName.Text, etLastName.Text, etEmail.Text, Convert.ToInt16(etAge.Text));
            alertWindow.Alert("Message", "Successful registration", Activity);
        }

    }
}