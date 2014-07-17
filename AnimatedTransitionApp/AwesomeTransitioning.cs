using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;

namespace AnimatedTransitionApp
{
	public class AwesomeTransitioning : UIViewControllerAnimatedTransitioning
	{
		private RectangleF startFrame;

		public bool IsPresentation {
			get;
			set;
		}

		public AwesomeTransitioning (RectangleF startFrame)
		{
			this.startFrame = startFrame;

		}

		#region IUIViewControllerAnimatedTransitioning implementation

		public override double TransitionDuration (IUIViewControllerContextTransitioning transitionContext)
		{
			return 0.5;
		}

		public override void AnimateTransition (IUIViewControllerContextTransitioning transitionContext)
		{
			var fromVC = transitionContext.GetViewControllerForKey (UITransitionContext.FromViewControllerKey);
			var fromView = fromVC.View;
			var toVC = transitionContext.GetViewControllerForKey (UITransitionContext.ToViewControllerKey);
			var toView = toVC.View;
			var containerView = transitionContext.ContainerView;

			var isPresentation = this.IsPresentation;

			if (isPresentation) {
				containerView.AddSubview (toView);
			}

			var animatingVC = isPresentation ? toVC : fromVC;
			var animatingView = animatingVC.View;

			var appearedFrame = transitionContext.GetFinalFrameForViewController (animatingVC);
			var dismissedFrame = this.startFrame;

			var initialFrame = isPresentation ? dismissedFrame : appearedFrame;
			var finalFrame = isPresentation ? appearedFrame : dismissedFrame;
			animatingView.Frame = initialFrame;

			UIView.AnimateNotify (0.5f, 
				0, 
				300.0f, 
				5.0f, 
				UIViewAnimationOptions.AllowUserInteraction | UIViewAnimationOptions.BeginFromCurrentState,
				() => {
					animatingView.Frame = finalFrame;
				},  new UICompletionHandler((bool finished) => {
					if(!isPresentation){
						fromView.RemoveFromSuperview();
					}
					transitionContext.CompleteTransition(true);
				}));
		}
		#endregion
	}
}

