using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class TestsRecyclerView : Fragment
    {
        #region HomeFragment datamembers
        private string emailAddress;
        private string token;
        #endregion

        #region Properties
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        #endregion

        #region Constructors
        public TestsRecyclerView(string emailAddress, string token)
        {
            EmailAddress = emailAddress;
            Token = token;
        }
        public TestsRecyclerView() : this("Empty", "Empty") { }
        public TestsRecyclerView(HomeFragment fragment) : this(fragment.EmailAddress, fragment.Token) { }
        #endregion
        private RecyclerView recyclerView;
        List<MultipleChoiceTest> lstData;
        private FirebaseTests tests = new FirebaseTests();
        private FragmentManager fm;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.fragment_view_tests, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewTests);

            CreateData();
            return view;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public async void CreateData()
        {

            //Loading data to list
            var items = await tests.GetAll<MultipleChoiceTest>("Tests").ConfigureAwait(continueOnCapturedContext: false);
            lstData = new List<MultipleChoiceTest>(items);
            var p = lstData.Count;
            Activity.RunOnUiThread(() =>
            {
                //Setting up the adapter
                recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
                FirebaseRecyclerViewAdapter adapter = new FirebaseRecyclerViewAdapter(lstData, fm);
                recyclerView.SetAdapter(adapter);
                adapter.ButtonClick += Details;
            });
        }

        private void Details(object sender, FirebaseRecyclerViewAdapterClickEventArgs e)
        {
            TestFragment fragment = new TestFragment(e.TestName, EmailAddress, Token);
            FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).AddToBackStack(null).Commit();
        }
    }
}