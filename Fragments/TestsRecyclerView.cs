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
        private RecyclerView recyclerView;
        List<MultipleChoiceTest> lstData;
        private FirebaseTests tests = new FirebaseTests();
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
            var items =  await tests.GetAllTests().ConfigureAwait(continueOnCapturedContext: false); 
            lstData = new List<MultipleChoiceTest>(items);
           var p = lstData.Count;
            Activity.RunOnUiThread(() => {
                //Setting up the adapter
                recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
                FirebaseRecyclerViewAdapter adapter = new FirebaseRecyclerViewAdapter(lstData);
                recyclerView.SetAdapter(adapter);
            });
        }
    }


}