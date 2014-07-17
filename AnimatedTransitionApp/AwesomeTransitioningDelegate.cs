using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace AnimatedTransitionApp
{
	public class AwesomeTransitioningDelegate : UIViewControllerTransitioningDelegate
	{
		private RectangleF startFrame;
		private AwesomePresentationController awesomePresentationController;

		private AwesomeTransitioning animationTransitioning;
		public AwesomeTransitioning AnimationTransitioning {
			get 
			{
				if (animationTransitioning == null) {
					animationTransitioning = new AwesomeTransitioning (this.startFrame);
				}
				return animationTransitioning;
			}
		}


		public AwesomeTransitioningDelegate (RectangleF startFrame)
		{
			this.startFrame = startFrame;
		}
			
		public override UIPresentationController GetPresentationControllerForPresentedViewController (MonoTouch.UIKit.UIViewController presentedViewController, MonoTouch.UIKit.UIViewController presentingViewController, MonoTouch.UIKit.UIViewController sourceViewController)
		{
			if (this.awesomePresentationController == null) {
				this.awesomePresentationController = new AwesomePresentationController (presentedViewController, presentedViewController);
			}
			return this.awesomePresentationController;
		}
			
		public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController (MonoTouch.UIKit.UIViewController dismissed)
		{
			var transitioning = this.AnimationTransitioning;
			transitioning.IsPresentation = false;
			return transitioning;
		}

		public override IUIViewControllerAnimatedTransitioning PresentingController (UIViewController presented, UIViewController presenting, UIViewController source)
		{
			var transitioning = this.AnimationTransitioning;
			transitioning.IsPresentation = true;
			return transitioning;
		}
							
	}
}

