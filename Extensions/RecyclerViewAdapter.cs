using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Models;

namespace EducationalSoftware.Extensions
{
    public class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private FirebaseTests firebaseTest = new FirebaseTests();
       // private List<MultipleChoiceTest> test = new List<MultipleChoiceTest>();
        private ConfiguredTaskAwaitable<List<MultipleChoiceTest>> test;

        //public RecyclerViewAdapter(List<MultipleChoiceTest> tests)
        //{
        //    test = tests;
        //}

        public RecyclerViewAdapter(ConfiguredTaskAwaitable<List<MultipleChoiceTest>> test)
        {
            this.test = test;
        }

        public override int ItemCount
        {
            get { return firebaseTest.GetAllTests().ConfigureAwait(false).GetAwaiter().GetResult().Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolder viewHolder = holder as ViewHolder;
            viewHolder.TxtTestName.Text = firebaseTest.GetTest(test.GetAwaiter().GetResult().First().TestName).ConfigureAwait(false).GetAwaiter().GetResult().TestName;
            viewHolder.TxtNotation.Text = firebaseTest.GetTest(test.GetAwaiter().GetResult().First().TestName).ConfigureAwait(false).GetAwaiter().GetResult().TestNotation;
            viewHolder.TxtUserName.Text = firebaseTest.GetTest(test.GetAwaiter().GetResult().First().TestName).ConfigureAwait(false).GetAwaiter().GetResult().UserCreatedTest;
            viewHolder.TxtDate.Text = firebaseTest.GetTest(test.GetAwaiter().GetResult().First().TestName).ConfigureAwait(false).GetAwaiter().GetResult().DateCreated.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.card_view, parent, false);
            return new ViewHolder(view);
        }
      

    }
    public class ViewHolder : RecyclerView.ViewHolder
    {
     //   public ImageView ImgView { get; set; }
        public TextView TxtNotation { get; set; }
        public TextView TxtTestName { get; set; }
        public TextView TxtUserName { get; set; }
        public TextView TxtDate { get; set; }
        public Button BtnStartTest { get; set; }
        public ViewHolder(View itemView) : base(itemView)
        {
        //    ImgView = itemView.FindViewById<ImageView>(Resource.Id.imageViewCard);
            TxtTestName = itemView.FindViewById<TextView>(Resource.Id.txtViewCardTestName);
            TxtNotation = itemView.FindViewById<TextView>(Resource.Id.txtViewCardTestNotation);
            TxtUserName = itemView.FindViewById<TextView>(Resource.Id.txtViewCardUser);
            TxtDate = itemView.FindViewById<TextView>(Resource.Id.txtViewCardDate);
            BtnStartTest = itemView.FindViewById<Button>(Resource.Id.btnCardStartTest);
        }
    }

}