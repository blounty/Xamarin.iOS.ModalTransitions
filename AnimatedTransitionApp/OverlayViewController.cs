
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AnimatedTransitionApp
{
	public partial class OverlayViewController : UIViewController
	{
		public OverlayViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		partial void GoBackPressed (MonoTouch.Foundation.NSObject sender)
		{
			this.DismissViewControllerAsync(true);
		}

	}
}

