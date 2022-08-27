using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HMControls;
using HMControls.Platform.iOS.Renderers;

[assembly: ExportRenderer(typeof(StandardEditor), typeof(iOSStandardEditorRenderer))]

namespace HMControls.Platform.iOS.Renderers
{
    public class iOSStandardEditorRenderer : EditorRenderer
    {
        public StandardEditor ElementV2 => Element as StandardEditor;
        public UITextViewPadding ControlV2 => Control as UITextViewPadding;

        protected override UITextView CreateNativeControl()
        {
            var control = new UITextViewPadding(RectangleF.Empty)
            {
                Padding = ElementV2.Padding,
                //BorderStyle = UITextBorderStyle.RoundedRect,
                ClipsToBounds = true
            };

            UpdateBackground(control);

            return control;
        }

        protected void UpdateBackground(UITextView control)
        {
            if (control != null)
            {
                if (ElementV2.RenderMode == RenderModeType.Standard)
                {
                    control.Layer.CornerRadius = ElementV2.CornerRadius;
                    control.Layer.BorderWidth = (System.nfloat)ElementV2.BorderThickness;
                    control.Layer.BorderColor = ElementV2.BorderColor.ToCGColor();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StandardEntry.PaddingProperty.PropertyName)
            {
                UpdatePadding();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected void UpdatePadding()
        {
            if (Control != null)
            {
                if (ElementV2.RenderMode == RenderModeType.Standard)
                {
                    ControlV2.Padding = ElementV2.Padding;
                }
            }
        }

    }
}
