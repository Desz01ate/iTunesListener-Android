using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;

namespace iTunesListener
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
        Android.Support.V7.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

            var contentFragment = new MainPage();
            var fm = FragmentManager.BeginTransaction();
            fm.Add(Resource.Id.fragment_container, contentFragment);
            fm.Commit();

			toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Main Menu";
            SetSupportActionBar(toolbar);
            toolbar.MenuItemClick += MenuClick;
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

		}

        private void MenuClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            var ft = FragmentManager.BeginTransaction();
            switch (e.Item.Order)
            {
                case 0:
                    var mp = new MainPage();
                    ft.Replace(Resource.Id.fragment_container, mp);
                    toolbar.Title = mp.GetType().Name;
                    break;
                case 1:
                    var settings = new Settings();
                    ft.Replace(Resource.Id.fragment_container, settings);
                    toolbar.Title = settings.GetType().Name;
                    break;
                case 2:
                    Finish();
                    break;
            }
            ft.Commit();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //View view = (View) sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

