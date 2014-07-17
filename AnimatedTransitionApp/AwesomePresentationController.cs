using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace AnimatedTransitionApp
{
	public class AwesomePresentationController : UIPresentationController
	{
		private UIView dimmingView;

		public AwesomePresentationController (UIViewController presentingViewController, UIViewController presentedViewController)
			: base(presentingViewController, presentedViewController)
		{
			SetUpDimmingView ();
		}

		public override System.Drawing.RectangleF FrameOfPresentedViewInContainerView {
			get {
				var containerBounds = this.ContainerView.Bounds;

				var presentedViewFrame = RectangleF.Empty;
				presentedViewFrame.Size = new SizeF (300, 300);
				presentedViewFrame.X = (containerBounds.Width / 2) - (presentedViewFrame.Width / 2);
				presentedViewFrame.Y = (containerBounds.Height / 2) - (presentedViewFrame.Height / 2);

				this.PresentedView.Layer.CornerRadius = presentedViewFrame.Size.Width / 2;
				this.PresentedView.ClipsToBounds = true;

				return presentedViewFrame;
			}
		}

		public override void PresentationTransitionWillBegin ()
		{
			this.dimmingView.Frame = this.ContainerView.Bounds;
			this.dimmingView.Alpha = 0;

			this.ContainerView.InsertSubview (this.dimmingView, 0);
			var coordinator = this.PresentedViewController.GetTransitionCoordinator ();
			if (coordinator != null) {
				coordinator.AnimateAlongsideTransition((context) => 
					{
						this.dimmingView.Alpha = 1;
					}, 
					(context) => 
					{

					});
			} else {
				this.dimmingView.Alpha = 1;
			}
		}

		public override void ContainerViewWillLayoutSubviews ()
		{
			dimmingView.Frame = this.ContainerView.Bounds;
		}

		public override void ContainerViewDidLayoutSubviews ()
		{
			base.ContainerViewDidLayoutSubviews ();
		}

		public override void DismissalTransitionWillBegin ()
		{
			var coordinator = this.PresentedViewController.GetTransitionCoordinator ();
			if (coordinator != null) {
				coordinator.AnimateAlongsideTransition((context) => 
					{
						this.dimmingView.Alpha = 0;

					}, 
					(context) => 
					{
					});
			} else {
				this.dimmingView.Alpha = 0;
			}
		}

		void SetUpDimmingView ()
		{
			this.dimmingView = new UIView ();
			this.dimmingView.BackgroundColor = UIColor.Black.ColorWithAlpha (0.4f);
			this.dimmingView.Alpha = 0;

			var dimmingViewTapGestureRecogniser = new UITapGestureRecognizer (this.DimmingViewTapped);

			this.dimmingView.AddGestureRecognizer (dimmingViewTapGestureRecogniser);
		}

		private void DimmingViewTapped(UIGestureRecognizer gesture)
		{
			if (gesture.State == UIGestureRecognizerState.Recognized) {
				this.PresentingViewController.DismissViewControllerAsync (true);
			}
		}
	}
}

