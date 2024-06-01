using System.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using HMControls;
using HMControls.Platform.Android.Renderers;
using Microsoft.Maui.Controls.Compatibility;
using System.Runtime.CompilerServices;
#if ANDROID
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
#endif

namespace HMControls.Platform.Android.Renderers;

public partial class StandardEntry
{
    //public StandardEntry ElementV2 => Element as StandardEntry;

    //public StandardEntryRenderer(Context context) : base(context) { }
    protected override AppCompatEditText CreateNativeControl()
    {
        var control = this.Handler.PlatformView as AppCompatEditText;
        //var control = base.CreateNativeControl();
        UpdateBackground(control);
        return control;
    }
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (propertyName == CornerRadiusProperty.PropertyName)
        {
            UpdateBackground();
        }
        else if (propertyName == BorderThicknessProperty.PropertyName)
        {
            UpdateBackground();
        }
        else if (propertyName == BorderColorProperty.PropertyName)
        {
            UpdateBackground();
        }

        base.OnPropertyChanged(propertyName);
    }

    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == StandardEntry.CornerRadiusProperty.PropertyName)
        {
            UpdateBackground();
        }
        else if (e.PropertyName == StandardEntry.BorderThicknessProperty.PropertyName)
        {
            UpdateBackground();
        }
        else if (e.PropertyName == StandardEntry.BorderColorProperty.PropertyName)
        {
            UpdateBackground();
        }

        base.OnElementPropertyChanged(sender, e);
    }

    protected override void UpdateBackgroundColor()
    {
        UpdateBackground();
    }
    protected void UpdateBackground(FormsEditText control)
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
