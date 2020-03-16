using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EducationalSoftware.Extensions;
using EducationalSoftware.Models;
using Firebase;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Android.Gms.Tasks;
using System.Threading;

namespace EducationalSoftware.Fragments
{
    public class AddTest : Android.Support.V4.App.Fragment, IOnProgressListener, IOnSuccessListener, IOnFailureListener
    {
        private string user;
        public string User { get; }
        public string TestName { get; set; }
        public AddTest(string user)
        {
            User = user;
        }
        public AddTest()
        {

        }
        private ImageView imgViewAddTest;
        private Android.Widget.Button btnAddQuestion, btnUpload, btnChoose;
        private EditText etTestName, etTestNotation, etNumberOfQuestions;
        private FirebaseTests helper = new FirebaseTests();
        private Android.Net.Uri filePath;
        private ProgressDialog progressDialog;
        private FirebaseStorage storage;
        private StorageReference reference;
        private const int PICK_IMAGE_REQUEST = 71;
        private string url;
        private Android.Gms.Tasks.Task path;
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(Activity, savedInstanceState);
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_add_test, container, false);

            storage = FirebaseStorage.GetInstance(FirebaseApp.InitializeApp(Activity));
            reference = storage.GetReferenceFromUrl("gs://educationalsoftware-ba7e4.appspot.com");
            #region Initialize fragment components
            btnAddQuestion = view.FindViewById<Android.Widget.Button>(Resource.Id.btnAddQuestions);
            etTestName = view.FindViewById<EditText>(Resource.Id.editTextTestName);
            etTestNotation = view.FindViewById<EditText>(Resource.Id.editTextTestNotation);
            etNumberOfQuestions = view.FindViewById<EditText>(Resource.Id.editTextQuestionNumber);
            imgViewAddTest = view.FindViewById<ImageView>(Resource.Id.imgViewCreateTest);
            btnUpload = view.FindViewById<Android.Widget.Button>(Resource.Id.btnAddTestUploadPhoto);

            btnChoose = view.FindViewById<Android.Widget.Button>(Resource.Id.btnAddTestChoosePhoto);
            #endregion
            btnChoose.Click += OnChooseClicked;
            btnUpload.Click += OnUploadClicked;
            btnAddQuestion.Click += OnAddQuestion;
            return view;
        }

        private void OnUploadClicked(object sender, EventArgs e)
        {
            if (filePath != null)
            {

                progressDialog = new ProgressDialog(Activity);
                progressDialog.SetTitle("Uploading...");
                // progressDialog.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
                progressDialog.Show();
                string testName = etTestName.Text;
                var images = reference.Child(User).Child(testName);
               
                images.PutFile(filePath)
                    .AddOnProgressListener(this)
                    .AddOnSuccessListener(this)
                    .AddOnFailureListener(this);

               
            }
        }

        private void OnChooseClicked(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select image"), PICK_IMAGE_REQUEST);
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] int resultCode, Intent data)
        {
            if (requestCode == PICK_IMAGE_REQUEST && data != null && data.Data != null)
            {
                filePath = data.Data;
                try
                {
                    Bitmap bitmap = MediaStore.Images.Media.GetBitmap(Activity.ContentResolver, filePath);
                    imgViewAddTest.SetImageBitmap(bitmap);
                }
                catch
                {

                }
            }
        }
        private async void OnAddQuestion(object sender, EventArgs e)
        {
            url = path.Result.ToString();

            //string str = "gs://educationalsoftware-ba7e4.appspot.com/" + User + "/" + etTestName.Text;
            MultipleChoiceTest test = new MultipleChoiceTest(url, User, Int32.Parse(etNumberOfQuestions.Text), etTestName.Text, etTestNotation.Text);
            await helper.AddToFirebase(test, "Tests").ConfigureAwait(false);
            AddQuestions questions = new AddQuestions(user, etTestName.Text, Int32.Parse(etNumberOfQuestions.Text));
            FragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, questions).Commit();

        }

        public void snapshot(Java.Lang.Object p0)
        {

            var taskSnapshot = (UploadTask.TaskSnapshot)p0;
            double progress = (100.0 * taskSnapshot.BytesTransferred / taskSnapshot.TotalByteCount);
            progressDialog.SetMessage("Uploaded " + (int)progress + " %");
         


        }

        public void OnSuccess(Java.Lang.Object result)
        {
          
            progressDialog.Dismiss();
            path = reference.Child(User).Child(etTestName.Text).GetDownloadUrl();

        }

        public void OnFailure(Java.Lang.Exception e)
        {
            throw new NotImplementedException();
        }
    }
}