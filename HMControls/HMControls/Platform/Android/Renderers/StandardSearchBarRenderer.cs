using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HMControls;
using HMControls.Platform.Android.Renderers;
using Android.Widget;
using AV = Android.Views;

[assembly: ExportRenderer(typeof(StandardSearchBar), typeof(StandardSearchBarRenderer))]

namespace HMControls.Platform.Android.Renderers
{
    public class StandardSearchBarRenderer : SearchBarRenderer
    {
        public StandardSearchBarRenderer(Context context) : base(context) { }
        public StandardSearchBar ElementV2 => Element as StandardSearchBar;
        protected override SearchView CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StandardSearchBar.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == StandardSearchBar.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == StandardSearchBar.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }
        protected void UpdateBackground(SearchView control)
        {
            if (control == null) return;

            int searchPlateId = control.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
            AV.View searchPlateView = control.FindViewById(searchPlateId);

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var tgd = new GradientDrawable();
            tgd.SetStroke(0, ElementV2.BorderColor.ToAndroid());
            searchPlateView.SetBackground(tgd);

            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
        protected void UpdateBackground()
        {
            UpdateBackground(Control);
        }

    }
}
