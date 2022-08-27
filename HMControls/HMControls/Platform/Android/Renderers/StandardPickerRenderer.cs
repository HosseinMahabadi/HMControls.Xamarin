using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HMControls;
using HMControls.Platform.Android.Renderers;
using Android.Widget;

namespace HMControls.Platform.Android.Renderers
{
    public class StandardPickerRenderer : PickerRenderer
    {
        public StandardPicker ElementV2 => Element as StandardPicker;

        public StandardPickerRenderer(Context context) : base(context) { }

        protected override EditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StandardPicker.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == StandardPicker.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == StandardPicker.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }
        protected void UpdateBackground(EditText control)
        {
            if (control != null)
            {
                if (ElementV2.RenderMode == RenderModeType.Standard)
                {
                    var gd = new GradientDrawable();
                    gd.SetColor(Element.BackgroundColor.ToAndroid());
                    gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
                    gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
                    control.SetBackground(gd);

                    var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
                    var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
                    var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
                    var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

                    control.SetPadding(padLeft, padTop, padRight, padBottom);
                }
            }
        }
        protected void UpdateBackground()
        {
            UpdateBackground(Control);
        }
    }
}
