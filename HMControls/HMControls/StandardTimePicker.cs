using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Diagnostics;
using Microsoft.Maui.Platform;
using System.Runtime.CompilerServices;
#if ANDROID
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
#endif

namespace HMControls;

public class StandardTimePicker : TimePicker, ICustomControl
{
    public StandardTimePicker()
    {
        try
        {
            ModifyCustomControl();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"StandardTimePicker ERROR!!! => {ex.Message}");
        }
    }

    #region Bindables

    public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
        typeof(int), 
        typeof(StandardTimePicker), 
        0);

    public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness),
        typeof(double), 
        typeof(StandardTimePicker), 
        1d);

    public static BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), 
        typeof(Thickness), 
        typeof(StandardTimePicker), 
        new Thickness(5));

    public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), 
        typeof(Color),
        typeof(StandardTimePicker), 
        Colors.Black);

    #endregion

    #region Properties

    public RenderModeType RenderMode { get; set; } = RenderModeType.Standard;

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public double BorderThickness
    {
        get => (double)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }
    public Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    #endregion

    #region Methods

    private void ModifyCustomControl()
    {
        try
        {
#if ANDROID
            UpdateBackgroundAndroid(ref Handler.PlatformView as EditText); 
#endif
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (propertyName == CornerRadiusProperty.PropertyName)
        {
#if ANDROID
            UpdateBackgroundAndroid(ref Handler.PlatformView as EditText);
#endif
        }
        else if (propertyName == BorderThicknessProperty.PropertyName)
        {
#if ANDROID
            UpdateBackgroundAndroid(ref Handler.PlatformView as EditText);
#endif
        }
        else if (propertyName == BorderColorProperty.PropertyName)
        {
#if ANDROID
            UpdateBackgroundAndroid(ref Handler.PlatformView as EditText);
#endif
        }

        base.OnPropertyChanged(propertyName);
    }

#if ANDROID
    protected void UpdateBackgroundAndroid(ref EditText control)
    {
        if (control != null)
        {
            if (RenderMode == RenderModeType.Standard)
            {
                var gd = new GradientDrawable();
                gd.SetColor(BackgroundColor.ToPlatform());
                gd.SetCornerRadius(Context.ToPixels(CornerRadius));
                gd.SetStroke((int)Context.ToPixels(BorderThickness), BorderColor.ToPlatform());
                control.SetBackground(gd);

                var padTop = (int)Context.ToPixels(Padding.Top);
                var padBottom = (int)Context.ToPixels(Padding.Bottom);
                var padLeft = (int)Context.ToPixels(Padding.Left);
                var padRight = (int)Context.ToPixels(Padding.Right);

                control.SetPadding(padLeft, padTop, padRight, padBottom);
            }
        }
    }
#endif

#endregion
}
