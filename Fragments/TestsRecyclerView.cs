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
using Android.Text;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;
using Firebase;
using Firebase.Storage;

namespace EducationalSoftware.Fragments
{
    public class TestsRecyclerView : Android.Support.V4.App.Fragment
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
        private List<MultipleChoiceTest> lstData;
        private FirebaseRecyclerViewAdapter adapter;
        private FirebaseTests tests = new FirebaseTests();
        private EditText etSearch;
        private Button btnSearch;
        private FragmentManager fm;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var items = tests.GetAll<MultipleChoiceTest>("Tests").ConfigureAwait(continueOnCapturedContext: false);


            View view = inflater.Inflate(Resource.Layout.fragment_view_tests, container, false);
            FirebaseApp.InitializeApp(Activity);
            etSearch = view.FindViewById<EditText>(Resource.Id.searchView);
            btnSearch = view.FindViewById<Button>(Resource.Id.buttonSearch);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewTests);

            CreateData();
            btnSearch.Click += OnSearch;
            return view;
        }

        private void OnSearch(object sender, EventArgs e)
        {
            SearchResultsFragment searchResults = new SearchResultsFragment(etSearch.Text);
            FragmentManager.BeginTransaction().Replace(Resource.Id.parent_fragment,searchResults).AddToBackStack(null).Commit();

            //var items = await tests.GetAll<MultipleChoiceTest>("Tests").ConfigureAwait(continueOnCapturedContext: false);
            //lstData = new List<MultipleChoiceTest>();

            //foreach (var item in items)
            //{
            //    if(item.TestName.Contains(etSearch.Text))
            //    {
            //        lstData.Add(item);
            //    }
            //}
            //Activity.RunOnUiThread(() =>
            //{
            //    //Setting up the adapter
            //    recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
            //    adapter = new FirebaseRecyclerViewAdapter(lstData, fm);
            //    recyclerView.SetAdapter(adapter);
            //    adapter.ButtonClick += Details;
            //});
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
            TestFragment fragment = new TestFragment(e.TestName, EmailAddress, Token);
            FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).Commit();
        }
        
    }
}