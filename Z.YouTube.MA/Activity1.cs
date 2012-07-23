using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using System.Collections.Generic;
using GYT=Google.YouTube;

namespace Z.YouTube.MA
{
	[Activity(Label = "Z.YouTube.MA", MainLauncher = true, Icon = "@drawable/icon")]
	public class Activity1 : ListActivity
	{
		int count = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			//SetContentView(Resource.Layout.Main);

			data = new List<string>();

			var t = new System.Threading.Thread(CallYoutube);
			t.Start();
			//CallYoutube();

			ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, data);

			ListView.TextFilterEnabled = true;

			ListView.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(ListView_ItemClick);
					// delegate(object sender, ItemEventArgs args)
		}

		
		void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			// When clicked, show a toast with the TextView text
			string msg = ((TextView)e.View).Text;
			Toast.MakeText(Application, msg, ToastLength.Short).Show();

			return;
		}

		List<string> data;

		void CallYoutube()
		{
			// Android?
			// UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			// YoutTube Activity confilcts with Android Activity!!! thus prefix!!
			var yreq = new GYT.YouTubeRequest(new GYT.YouTubeRequestSettings("MonoTouchSample", "AI39si4v3E6oIYiI60ndCNDqnPP5lCqO28DSvvDPnQt-Mqia5uPz2e4E-gMSBVwHXwyn_LF1tWox4LyM-0YQd2o4i_3GcXxa2Q"));

			var feed = yreq.GetVideoFeed("xamarinhq");

			// feed.Entries.ToList().ForEach((video) // MikeB's code
			// =>
			foreach (GYT.Video v in feed.Entries)
			{
				data.Add(v.Title);
			}

			int i = 1;
			RunOnUiThread(
			//InvokeOnMainThread(
				delegate
				{
					ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, data);

					ListView.TextFilterEnabled = true;

					ListView.ItemClick +=new EventHandler<AdapterView.ItemClickEventArgs>(ListView_ItemClick);
							//delegate(object sender, ItemEventArgs args)
					{
						// When clicked, show a toast with the TextView text
						//Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
					};

				}
			);
		}

	}
}

