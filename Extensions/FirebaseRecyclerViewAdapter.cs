using System;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Runtime;
using System.Collections.Generic;
using EducationalSoftware.Models;
using System.Globalization;
using EducationalSoftware.Fragments;
using Android.App;
using System.Net;
using Android.Graphics;
using Firebase.Storage;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using Firebase;
using static Android.App.DownloadManager;
using System.Threading.Tasks;
using System.IO;
using Plugin.Media.Abstractions;
using Android.Gms.Extensions;
using Firebase.Database;
using Android.Gms.Tasks;

namespace EducationalSoftware.Extensions
{
    public class FirebaseRecyclerViewAdapter : RecyclerView.Adapter, IFilterable
    {
        public event EventHandler<FirebaseRecyclerViewAdapterClickEventArgs> ItemClick;
        public event EventHandler<FirebaseRecyclerViewAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<FirebaseRecyclerViewAdapterClickEventArgs> ButtonClick;
        private List<MultipleChoiceTest> Items;
        private FirebaseStorage storage = FirebaseStorage.Instance;
        private StorageReference reference;
        private FragmentManager fm;
        private string getUrl;
        private Android.Net.Uri filePath;
        public FirebaseRecyclerViewAdapter(List<MultipleChoiceTest> Data, FragmentManager fm)
        {
            Items = Data;
            this.fm = fm;
        }
        // Create new view
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Layout setup
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.card_view, parent, false);
            var vh = new FirebaseRecyclerViewAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }
        // Replace the contents of a view (invoked by the layout manager)

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            FirebaseApp.InitializeApp(Application.Context);
            var item = Items[position];
            // Replace the contents of the view with that element
            var holder = viewHolder as FirebaseRecyclerViewAdapterViewHolder;
            holder.ImageURL = Items[position].ImageURL;
         
          //  Toast.MakeText(Android.App.Application.Context, holder.ImageURL, ToastLength.Long).Show();

            Glide
                .With(Application.Context)
                .Load(Items[position].ImageURL)
                .Into(holder.ImgView);
            holder.TxtDate.Text = Items[position].DateCreated.ToString("d", new CultureInfo("pt-BR"));
            holder.TxtNumberOfQuestions.Text = Items[position].NumberOfQuestions.ToString();
            holder.TxtTestName.Text = Items[position].TestName;
            holder.TxtNotation.Text = Items[position].TestNotation;
            holder.TxtUserName.Text = Items[position].UserCreatedTest;
            //On button click
            holder.BtnStartTest.Click += (o, e) =>
            {
                //TODO: Create fragment to load 
                OnButtonClick(new FirebaseRecyclerViewAdapterClickEventArgs(holder.TxtTestName.Text));
                Toast.MakeText(Android.App.Application.Context, holder.TxtTestName.Text, ToastLength.Short).Show();
            };
           
        }
      
        public override int ItemCount => Items.Count;
        
        public Filter Filter => throw new NotImplementedException();

        void OnButtonClick(FirebaseRecyclerViewAdapterClickEventArgs args) => ButtonClick?.Invoke(this, args);
        void OnClick(FirebaseRecyclerViewAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(FirebaseRecyclerViewAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);


    }
    public class FirebaseRecyclerViewAdapterViewHolder : RecyclerView.ViewHolder
    {
        public string ImageURL { get; set; }

        public ImageView ImgView { get; set; }
        public TextView TxtNotation { get; set; }
        public TextView TxtTestName { get; set; }
        public TextView TxtUserName { get; set; }
        public TextView TxtDate { get; set; }
        public Button BtnStartTest { get; set; }

        public TextView TxtNumberOfQuestions { get; set; }
        public FirebaseRecyclerViewAdapterViewHolder(View itemView, Action<FirebaseRecyclerViewAdapterClickEventArgs> clickListener,
                            Action<FirebaseRecyclerViewAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            ImageURL = "gs://educationalsoftware-ba7e4.appspot.com/" + TxtUserName + "/" + TxtTestName;
            ImgView = itemView.FindViewById<ImageView>(Resource.Id.imageViewCard);
            TxtDate = itemView.FindViewById<TextView>(Resource.Id.txtViewCardDate);
            TxtNumberOfQuestions = itemView.FindViewById<TextView>(Resource.Id.txtViewNumberOfQuestions);
            TxtTestName = itemView.FindViewById<TextView>(Resource.Id.txtViewCardTestName);
            TxtNotation = itemView.FindViewById<TextView>(Resource.Id.txtViewCardTestNotation);
            TxtUserName = itemView.FindViewById<TextView>(Resource.Id.txtViewCardUser);
            BtnStartTest = itemView.FindViewById<Button>(Resource.Id.btnCardStartTest);
        }
    }
    public class FirebaseRecyclerViewAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public string TestName { get; set; }

        public FirebaseRecyclerViewAdapterClickEventArgs(string testName)
        {
            TestName = testName;
        }
    }
}