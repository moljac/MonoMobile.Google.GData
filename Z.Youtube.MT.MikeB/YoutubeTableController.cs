using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Google.YouTube;
using Google.GData.YouTube;
using Google.GData.Client;
using System.Threading;

namespace YoutubeApp
{
    public partial class YoutubeTableController : UITableViewController
    {
        static NSString cellId = new NSString ("YoutubeResultCell");
        List<string> data;
       
        public YoutubeTableController ()
        {
            data = new List<string> ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            Title = "Youtube Demo";
            TableView.Source = new TableSource (this);

            var t = new Thread (CallYoutube);
            t.Start ();
            //CallYoutube();
            
        }

        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }

        void CallYoutube ()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

            var yreq = new YouTubeRequest (new YouTubeRequestSettings ("MonoTouchSample", "AI39si4v3E6oIYiI60ndCNDqnPP5lCqO28DSvvDPnQt-Mqia5uPz2e4E-gMSBVwHXwyn_LF1tWox4LyM-0YQd2o4i_3GcXxa2Q"));

            var feed = yreq.GetVideoFeed ("xamarinhq");

            feed.Entries.ToList ().ForEach ((video) => {
                data.Add (video.Title);
            }
            );

            InvokeOnMainThread (delegate {
                    
                TableView.ReloadData ();
                UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
            }
            );
        }

        class TableSource : UITableViewSource
        {
            YoutubeTableController controller;

            public TableSource (YoutubeTableController controller)
            {
                this.controller = controller;
            }

            public override int RowsInSection (UITableView tableView, int section)
            {
                return controller.data.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell (cellId);
                
                if (cell == null)
                    cell = new UITableViewCell (
                        UITableViewCellStyle.Value1,
                        cellId
                    );
                
                cell.TextLabel.Text = controller.data [indexPath.Row];
                
                return cell;
            }

        }
        
    }
}


