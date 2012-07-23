using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Z.YT.MT
{
	public partial class MainViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		UIPopoverController flipsidePopoverController;
		
		public MainViewController ()
			: base (UserInterfaceIdiomIsPhone ? "MainViewController_iPhone" : "MainViewController_iPad" , null)
		{
			// Custom initialization
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		partial void showInfo (NSObject sender)
		{
			if (UserInterfaceIdiomIsPhone) {
				var controller = new FlipsideViewController () {
					ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal,
				};
				
				controller.Done += delegate {
					this.DismissModalViewControllerAnimated (true);
				};
				
				this.PresentModalViewController (controller, true);
			} else {
				if (flipsidePopoverController == null) {
					var controller = new FlipsideViewController ();
					flipsidePopoverController = new UIPopoverController (controller);
					controller.Done += delegate {
						flipsidePopoverController.Dismiss (true);
					};
				}
				
				if (flipsidePopoverController.PopoverVisible) {
					flipsidePopoverController.Dismiss (true);
				} else {
					flipsidePopoverController.PresentFromBarButtonItem ((UIBarButtonItem)sender, UIPopoverArrowDirection.Any, true);
				}
			}
		}
	}
}

