using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace HMControls.Platform.iOS.Renderers
{
    public class UITextViewPadding : UITextView
    {
        private Thickness _padding = new Thickness(5);

        public Thickness Padding
        {
            get => _padding;
            set
            {
                if (_padding != value)
                {
                    _padding = value;
                    //InvalidateIntrinsicContentSize();
                }
            }
        }

        public UITextViewPadding()
        {
        }
        public UITextViewPadding(NSCoder coder) : base(coder)
        {
        }

        public UITextViewPadding(CGRect rect) : base(rect)
        {
        }

        public override CGRect AlignmentRectForFrame(CGRect frame)
        {
            var insets = new UIEdgeInsets((float)Padding.Top, (float)Padding.Left, (float)Padding.Bottom, (float)Padding.Right);
            return insets.InsetRect(frame);
        }
    }
}
