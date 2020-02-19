using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

using Xamarin.Forms;

using EducationalSoftware.Verification;
namespace EducationalSoftware.Fragments
{
    [Obsolete]
    public class RegisterFragment : Fragment
    {
        private EditText etFirstName, etLastName, etEmail;
        private Android.Widget.Button btnRegister;

       // public Task<bool> DisplayAlert(string title, string message, string accept, string cancel) { };
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_register, container, false);
            etFirstName = view.FindViewById<EditText>(Resource.Id.editTextFirstName);
            etLastName = view.FindViewById<EditText>(Resource.Id.editTextLastName);
            etEmail = view.FindViewById<EditText>(Resource.Id.editTextEmail);
            btnRegister = view.FindViewById<Android.Widget.Button>(Resource.Id.btnRegisterUser);

            btnRegister.Click += RegisterUser_Click;
            return view;
        }

        public void Alert(string title, string message)
        {
            Android.App.AlertDialog.Builder dial = new AlertDialog.Builder(Activity);
            AlertDialog al = dial.Create();
            al.SetTitle(title);
            al.SetMessage(message);
            //alert.SetIcon(Resource.Drawable.alert);
            al.SetButton("OK", (c, ev) => { });
            al.Show();
        }

        private void RegisterUser_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection("server=sql2.freemysqlhosting.net;port=3306;database=sql2323477;user=sql2323477;password=zS1!nT9!;");
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
               
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO user(firstName,lastName,email) VALUES(@firstname,@lastname,@email)", con);
                    cmd.Connection = con;
                   // MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO user(name,email) ", con);
                    cmd.Parameters.AddWithValue("@firstname", etFirstName.Text);
                    cmd.Parameters.AddWithValue("@lastname", etLastName.Text);

                    VerifyRegistration verify = new VerifyRegistration();
                    if (verify.VerifyEmail(etEmail.Text)==false)
                    {
                        Alert("Error!", "Email is wrong or already taken!");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@email", etEmail.Text);
                        cmd.ExecuteNonQuery();

                        Alert("Congrats", "Registration successful");

                    }
                    
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