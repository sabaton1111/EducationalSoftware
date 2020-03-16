using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;

namespace EducationalSoftware.Fragments
{
    public class SearchResultsFragment : Android.Support.V4.App.Fragment
    {
        private string searchText;
        public string SearchText { get; set; }
        public SearchResultsFragment(string searchText)
        {
            SearchText = searchText;
        }
        private TabLayout tabLayout;
        private ViewPager viewPager;

        private int[] tabIcons = {
            Resource.Drawable.email_icon,
            Resource.Drawable.home_icon,
            Resource.Drawable.statistic_icon
        };
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_search_results, container, false);
            viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            setupViewPager(viewPager);
            
            tabLayout = view.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);
            setupTabIcons();
            return view;
        }

        private void setupTabIcons()
        {
            tabLayout.GetTabAt(0).SetIcon(tabIcons[0]);
            tabLayout.GetTabAt(1).SetIcon(tabIcons[1]);
            tabLayout.GetTabAt(2).SetIcon(tabIcons[2]);
        }
        private Android.Support.V4.App.Fragment testsTab;
        private Android.Support.V4.App.Fragment profilesTab;
        private Android.Support.V4.App.Fragment articlesTab;
        private void InditialFragment()
        {
            testsTab = new Android.Support.V4.App.Fragment();
            profilesTab = new SettingsFragment();
            articlesTab = new RegistrationTypeFragment();
        }
        public void setupViewPager(ViewPager viewPager)
        {
            InditialFragment();
            
            ViewPagerAdapter adapter = new ViewPagerAdapter(ChildFragmentManager);
            adapter.addFragment(testsTab, "Tests");
            adapter.addFragment(profilesTab, "Users");
            adapter.addFragment(articlesTab, "Articles");
            viewPager.Adapter = adapter;
        }
    }
        
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        
        private List<Android.Support.V4.App.Fragment> mFragmentList = new List<Android.Support.V4.App.Fragment>();
        private List<string> mFragmentTitleList = new List<string>();

        public ViewPagerAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
        {
            //base.OnCreate(manager);
        }

        public ViewPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            
        }

        public override int Count
        {
            get
            {
                return mFragmentList.Count;
            }
        }
        public override Android.Support.V4.App.Fragment GetItem(int postion)
        {
            return mFragmentList[postion];
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {

            return new Java.Lang.String(mFragmentTitleList[position].ToLower());// display the title
            //return null;// display only the icon
        }

        public void addFragment(Android.Support.V4.App.Fragment fragment, string title)
        {
            mFragmentList.Add(fragment);
            mFragmentTitleList.Add(title);
        }
    }

}