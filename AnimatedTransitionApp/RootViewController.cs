
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AnimatedTransitionApp
{
	public partial class RootViewController : UIViewController
	{
		private AwesomeTransitioningDelegate transitioningDelegate;

		public RootViewController (IntPtr handle) : base (handle)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void TransitionPressed (MonoTouch.Foundation.NSObject sender)
		{
			var overlayVC = (OverlayViewController)this.Storyboard.InstantiateViewController("OverlayVC");

			this.transitioningDelegate = new AwesomeTransitioningDelegate (((UIButton)sender).Frame);

			overlayVC.ModalPresentationStyle = UIModalPresentationStyle.Custom;
			overlayVC.TransitioningDelegate = this.transitioningDelegate;

			this.PresentViewControllerAsync(overlayVC, true);
		}
	}
}

