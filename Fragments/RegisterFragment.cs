using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Data;
using MySql.Data.MySqlClient;
using EducationalSoftware.Verification;
using EducationalSoftware.Extensions;

namespace EducationalSoftware.Fragments
{
    [Obsolete]
    public class RegisterFragment : Fragment
    {
        private VerifyRegistration verify = new VerifyRegistration();
        private Extensions.PopupWindow alertWindow = new Extensions.PopupWindow();

        private EditText etFirstName, etLastName, etEmail;
        private Android.Widget.Button btnRegister;
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register, container, false);

            //Initializing editText boxes and register button
            etFirstName = view.FindViewById<EditText>(Resource.Id.editTextFirstName);
            etLastName = view.FindViewById<EditText>(Resource.Id.editTextLastName);
            etEmail = view.FindViewById<EditText>(Resource.Id.editTextEmail);
            btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnRegisterUser);
            btnRegister.Click += RegisterUser_Click;

            return view;
        }
        private void RegisterUser_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=sql2.freemysqlhosting.net;port=3306;database=sql2323477;user=sql2323477;password=zS1!nT9!;");
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO user(firstName,lastName,email) VALUES(@firstname,@lastname,@email)", con);
                    cmd.Connection = con;

                    #region Verify data
                    if (verify.VerifyName(etFirstName.Text) == false)
                    {
                        alertWindow.Alert("Error!", "Please enter valid First name!", Activity);
                        return;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@firstname", etFirstName.Text);
                    }
                    if (verify.VerifyName(etLastName.Text) == false)
                    {
                        alertWindow.Alert("Error!", "Please enter valid Last name!", Activity);
                        return;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@lastname", etLastName.Text);
                    }
                    if (verify.VerifyEmail(etEmail.Text) == false)
                    {
                        alertWindow.Alert("Error!", "Email is wrong or already taken!", Activity);
                        return;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@email", etEmail.Text);
                    }
                    #endregion

                        cmd.ExecuteNonQuery();
                        alertWindow.Alert("Congrats", "Registration successful", Activity);
                }
            }
            catch (MySqlException exception)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }
}