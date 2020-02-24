﻿using System;
using System.Security.Cryptography;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Verification;
using EducationalSoftware.Extensions;
using System.Text;
using EducationalSoftware.Models;
namespace EducationalSoftware.Fragments
{
    public class RegisterFragment : Fragment
    {
        #region Data members
        private VerifyRegistration verify = new VerifyRegistration();
        private Encryption encryption = new Encryption();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();
        private EditText etFirstName, etLastName, etEmail, etPassword, etRepeatPassword, etAge,
            etVerificationKey, etSchool, etCity, etCountry, etClass, etSubject;
        private Android.Widget.Button btnRegister;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        #endregion
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return SetView(inflater, container, savedInstanceState);
        }
        private View SetView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var androidID = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver,
          Android.Provider.Settings.Secure.AndroidId);

            RegistrationRequest req = firebaseHelper.GetRequest(androidID).ConfigureAwait(false).GetAwaiter().GetResult();

            if (req.Token == "User")
            {
                Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register, container, false);

                //Initializing editText boxes and register button

                etFirstName = view.FindViewById<EditText>(Resource.Id.editTextFirstName);
                etLastName = view.FindViewById<EditText>(Resource.Id.editTextLastName);
                etEmail = view.FindViewById<EditText>(Resource.Id.editTextEmail);
                etPassword = view.FindViewById<EditText>(Resource.Id.editTextPassword);
                etRepeatPassword = view.FindViewById<EditText>(Resource.Id.editTextRepeatPassword);
                etAge = view.FindViewById<EditText>(Resource.Id.editTextAge);

                btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnRegisterUser);
                btnRegister.Click += AddUser_Click;
                return view;
            }

            else if (req.Token == "Teacher")
            {
                Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register_teacher, container, false);
                etSubject = view.FindViewById<EditText>(Resource.Id.editTextTeacherSubject);
                etSchool = view.FindViewById<EditText>(Resource.Id.editTextTeacherInstitution);
                etFirstName = view.FindViewById<EditText>(Resource.Id.editTextTeacherFirstName);
                etLastName = view.FindViewById<EditText>(Resource.Id.editTextTeacherLastName);
                etEmail = view.FindViewById<EditText>(Resource.Id.editTextTeacherEmail);
                etPassword = view.FindViewById<EditText>(Resource.Id.editTextTeacherPassword);
                etRepeatPassword = view.FindViewById<EditText>(Resource.Id.editTextTeacherRepeatPassword);
                etAge = view.FindViewById<EditText>(Resource.Id.editTextTeacherAge);
                btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnTeacherRegisterUser);
                btnRegister.Click += BtnTeacherRegister_Click;


                return view;
            }
            else if (req.Token == "Admin")
            {
                Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register_admin, container, false);
                etVerificationKey = view.FindViewById<EditText>(Resource.Id.editTextAdminUniqueKey);

                etFirstName = view.FindViewById<EditText>(Resource.Id.editTextAdminFirstName);
                etLastName = view.FindViewById<EditText>(Resource.Id.editTextAdminLastName);
                etEmail = view.FindViewById<EditText>(Resource.Id.editTextAdminEmail);
                etPassword = view.FindViewById<EditText>(Resource.Id.editTextAdminPassword);
                etRepeatPassword = view.FindViewById<EditText>(Resource.Id.editTextAdminRepeatPassword);
                etAge = view.FindViewById<EditText>(Resource.Id.editTextAdminAge);
                btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnAdminRegisterUser);
                btnRegister.Click += BtnAdminRegister_Click;
                return view;
            }

            else if (req.Token == "UniversityStudent")
            {
                Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register_university_student, container, false);
                etSubject = view.FindViewById<EditText>(Resource.Id.editTextUStudentSubject);
                etSchool = view.FindViewById<EditText>(Resource.Id.editTextUStudentUniversity);
                etClass = view.FindViewById<EditText>(Resource.Id.editTextUStudentCourse);
                etCountry = view.FindViewById<EditText>(Resource.Id.editTextUStudentCountry);
                etCity = view.FindViewById<EditText>(Resource.Id.editTextUStudentCity);
                etFirstName = view.FindViewById<EditText>(Resource.Id.editTextUStudentFirstName);
                etLastName = view.FindViewById<EditText>(Resource.Id.editTextUStudentLastName);
                etEmail = view.FindViewById<EditText>(Resource.Id.editTextUStudentEmail);
                etPassword = view.FindViewById<EditText>(Resource.Id.editTextUStudentPassword);
                etRepeatPassword = view.FindViewById<EditText>(Resource.Id.editTextUStudentRepeatPassword);
                etAge = view.FindViewById<EditText>(Resource.Id.editTextUStudentAge);
                btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnUStudentRegisterUser);
                btnRegister.Click += BtnUStudentRegister_Click;
                return view;
            }
            else
            {
                Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register_school_student, container, false);
                etSchool = view.FindViewById<EditText>(Resource.Id.editTextStudentSchool);
                etClass = view.FindViewById<EditText>(Resource.Id.editTextStudentClass);
                etCountry = view.FindViewById<EditText>(Resource.Id.editTextStudentCountry);
                etCity = view.FindViewById<EditText>(Resource.Id.editTextStudentCity);
                etFirstName = view.FindViewById<EditText>(Resource.Id.editTextStudentFirstName);
                etLastName = view.FindViewById<EditText>(Resource.Id.editTextStudentLastName);
                etEmail = view.FindViewById<EditText>(Resource.Id.editTextStudentEmail);
                etPassword = view.FindViewById<EditText>(Resource.Id.editTextStudentPassword);
                etRepeatPassword = view.FindViewById<EditText>(Resource.Id.editTextStudentRepeatPassword);
                etAge = view.FindViewById<EditText>(Resource.Id.editTextStudentAge);
                btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnStudentRegisterUser);
                btnRegister.Click += BtnStudentRegister_Click;
                return view;
            }
        }
        public bool Verify()
        {
            #region Verify data
            if (verify.VerifyName(etFirstName.Text) == false)
            {
                alertWindow.Alert("Error!", "Please enter valid First name!", Activity);
                return false;
            }

            if (verify.VerifyName(etLastName.Text) == false)
            {
                alertWindow.Alert("Error!", "Please enter valid Last name!", Activity);
                return false;
            }
            if (verify.VerifyEmail(etEmail.Text) == false)
            {
                alertWindow.Alert("Error!", "Email is wrong or already taken!", Activity);
                return false;
            }
            if (verify.VerifyPassword(etPassword.Text, etRepeatPassword.Text) != 5)
            {
                #region Error types
                switch (verify.VerifyPassword(etPassword.Text, etRepeatPassword.Text))
                {
                    case 0:
                        {
                            alertWindow.Alert("Error!", "Password should be betweem 6 and 20 characters long!", Activity);
                            return false;
                        }
                    case 1:
                        {
                            alertWindow.Alert("Error!", "Password should contain Upper letter, Lower letter and digit", Activity);
                            return false;
                        }
                    case 2:
                        {
                            alertWindow.Alert("Error!", "Password contains forbidden character (<, >, ;, \\, {, }, [, ], +, ,, ?, \', \", `, : )!", Activity);
                            return false;
                        }
                    case 3:
                        {
                            alertWindow.Alert("Error!", "Password cannot contain spaces", Activity);
                            return false;
                        }
                    case 4:
                        {
                            alertWindow.Alert("Error!", "Password and Repeat Password not matching!", Activity);
                            return false;
                        }
                }
                #endregion
            }
            #region Verify Age
            try
            {
                verify.VerifyAge(Convert.ToInt16(etAge.Text));

            }
            catch
            {
                alertWindow.Alert("Error!", "Field cannot be empty!", Activity);
                return false;
            }
            if (verify.VerifyAge(Convert.ToInt16(etAge.Text)) == false)
            {
                alertWindow.Alert("Error!", "Age must be between 10 and 120!", Activity);
                return false;
            }
            #endregion

            return true;

            #endregion

        }
        public bool VerifyStudent()
        {
            if (verify.VerifyClass(Convert.ToInt16(etClass)) == false)
            {
                alertWindow.Alert("Error!", "Invalid class!", Activity);
                return false;
            }
            if (verify.VerifyName(etCountry.Text) == false)
            {
                alertWindow.Alert("Error!", "Invalid country name!", Activity);
                return false;
            }
            if (verify.VerifyName(etCity.Text) == false)
            {
                alertWindow.Alert("Error!", "Invalid city name!", Activity);
                return false;
            }
            return true;
        }
        private async void BtnUStudentRegister_Click(object sender, EventArgs e)
        {
            if (Verify() == true && VerifyStudent() == true)
            {
                await firebaseHelper.AddUniversityStudent(etFirstName.Text, etLastName.Text, etEmail.Text,
                etPassword.Text = encryption.EncodeServerName(etPassword.Text), etSchool.Text,
                etSubject.Text, Convert.ToInt16(etClass.Text), etCountry.Text, etCity.Text, Convert.ToInt16(etAge.Text));
                await firebaseHelper.AddEmailPass(etEmail.Text, etPassword.Text, "Teacher");
                alertWindow.Alert("Message", "Successful registration", Activity);
                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();

            }
        }
        private async void BtnTeacherRegister_Click(object sender, EventArgs e)
        {
            if (Verify() == true)
            {
                await firebaseHelper.AddTeacher(etFirstName.Text, etLastName.Text, etEmail.Text,
                   etPassword.Text = encryption.EncodeServerName(etPassword.Text), etSubject.Text, etSchool.Text, Convert.ToInt16(etAge.Text));
                await firebaseHelper.AddEmailPass(etEmail.Text, etPassword.Text, "Teacher");
                alertWindow.Alert("Message", "Successful registration", Activity);

                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();
            }
        }
        private async void BtnStudentRegister_Click(object sender, EventArgs e)
        {
            if (Verify() == true && VerifyStudent() == true)
            {
                await firebaseHelper.AddSchoolStudent(etFirstName.Text, etLastName.Text, etEmail.Text,
                etPassword.Text = encryption.EncodeServerName(etPassword.Text), etSchool.Text,
                Convert.ToInt16(etClass.Text), etCountry.Text, etCity.Text, Convert.ToInt16(etAge.Text));
                await firebaseHelper.AddEmailPass(etEmail.Text, etPassword.Text, "SchoolStudent");
                alertWindow.Alert("Message", "Successful registration", Activity);
                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();
            }
        }
        private async void BtnAdminRegister_Click(object sender, EventArgs e)
        {
            if(Verify()==true)
            {
                await firebaseHelper.AddAdmin(etVerificationKey.Text, etFirstName.Text, etLastName.Text, etEmail.Text, etPassword.Text = encryption.EncodeServerName(etPassword.Text), Convert.ToInt16(etAge.Text));
                await firebaseHelper.AddEmailPass(etEmail.Text, etPassword.Text, "Admin");
                alertWindow.Alert("Message", "Successful registration", Activity);
                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();
            }
        }
        private async void AddUser_Click(object sender, EventArgs e)
        {
            if (Verify() == true)
            {
                await firebaseHelper.AddUser(etFirstName.Text, etLastName.Text, etEmail.Text, etPassword.Text = encryption.EncodeServerName(etPassword.Text), Convert.ToInt16(etAge.Text));
                await firebaseHelper.AddEmailPass(etEmail.Text, etPassword.Text, "User");
                alertWindow.Alert("Message", "Successful registration", Activity);
                Fragment loginFragment = new LoginFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, loginFragment).Commit();
            }
        }
    }
}