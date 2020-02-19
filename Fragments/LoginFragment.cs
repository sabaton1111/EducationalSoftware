using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EducationalSoftware.Fragments
{
    [Obsolete]
    public class LoginFragment : Fragment
    {


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_login, container, false);


            Button btnRegister = view.FindViewById<Button>(Resource.Id.btnRegister);
            btnRegister.Click += delegate
            {
                Fragment registerFragment = new RegisterFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment, registerFragment).AddToBackStack(null).Commit();
                
            };
            return view;
        }

    }
}