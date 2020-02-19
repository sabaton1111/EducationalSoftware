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
namespace EducationalSoftware.Fragments
{
    [Obsolete]
    public class RegisterFragment : Fragment
    {
        private EditText etName, etEmail;
        private Button btnRegister;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_register, container, false);
            etName = view.FindViewById<EditText>(Resource.Id.editTextFirstName);
            etEmail = view.FindViewById<EditText>(Resource.Id.editTextEmail);
            btnRegister = view.FindViewById<Button>(Resource.Id.btnRegisterUser);

            btnRegister.Click += RegisterUser_Click;
            return view;
        }

        private void RegisterUser_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection("server=sql2.freemysqlhosting.net;port=3306;database=sql2323477;user=sql2323477;password=zS1!nT9!;");
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO user(name,email) VALUES(@name,@email)", con);
                   // MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO user(name,email) ", con);
                    cmd.Parameters.AddWithValue("@name", etName.Text);
                    cmd.Parameters.AddWithValue("@email", etEmail.Text);
                    cmd.ExecuteNonQuery();

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