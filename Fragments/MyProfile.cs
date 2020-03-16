using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;

namespace EducationalSoftware.Fragments
{
    public class MyProfile : Android.Support.V4.App.Fragment
    {
        private RecyclerView recyclerView;
        private List<MultipleChoiceTest> lstData;
        private FirebaseRecyclerViewAdapter adapter;
        private FirebaseTests tests = new FirebaseTests();
        private FragmentManager fm;
        #region HomeFragment datamembers
        private string emailAddress;
        private string token;
        #endregion

        #region Properties
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        #endregion

        #region Constructors
        public MyProfile(string emailAddress, string token)
        {
            EmailAddress = emailAddress;
            Token = token;
        }

        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_profile, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewTestsProfile);
            LoadData();
            return view;
        }

        private async void LoadData()
        {
            var items = await tests.GetAll<MultipleChoiceTest>("Tests").ConfigureAwait(continueOnCapturedContext: false);
            lstData = new List<MultipleChoiceTest>();

            foreach (var item in items)
            {
                if (item.UserCreatedTest.Equals(EmailAddress) == true)
                {
                    lstData.Add(item);
                }
            }
            Activity.RunOnUiThread(() =>
            {
                //Setting up the adapter
                recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
                adapter = new FirebaseRecyclerViewAdapter(lstData, fm);
                recyclerView.SetAdapter(adapter);
                adapter.ButtonClick += Details;
            });
        }
        private void Details(object sender, FirebaseRecyclerViewAdapterClickEventArgs e)
        {
            MyProfile fragment = new MyProfile(EmailAddress, Token);
            FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).Commit();
        }
    }
}