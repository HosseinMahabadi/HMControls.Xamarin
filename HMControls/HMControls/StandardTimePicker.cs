using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Diagnostics;
using Microsoft.Maui.Platform;
using System.Runtime.CompilerServices;
using HMExtension.Maui;
#if ANDROID
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
#elif WINDOWS
using Microsoft.UI.Xaml.Controls;
#endif

namespace HMControls;

public class StandardTimePicker : Microsoft.Maui.Controls.TimePicker
{
    public StandardTimePicker()
    {
        try
        {
            HandlerChanged += (s, e) =>
            {
                ModifyCustomControl();
                PropertyChanged += StandardTimePicker_PropertyChanged;
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
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

    private void StandardTimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        try
        {
            if (e.PropertyName == BackgroundColorProperty.PropertyName ||
                e.PropertyName == CornerRadiusProperty.PropertyName ||
                e.PropertyName == BorderColorProperty.PropertyName ||
                e.PropertyName == BorderThicknessProperty.PropertyName ||
                e.PropertyName == PaddingProperty.PropertyName)
            {
                if (Handler != null)
                {
                    UpdateBackground(Handler.PlatformView);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }

    }

    protected virtual void ModifyCustomControl()
    {
        try
        {
#if ANDROID_
            UpdateBackgroundAndroid(Handler.PlatformView as EditText); 
#endif
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void UpdateBackground(object platformView)
    {
#if ANDROID
        var control = platformView as EditText;

        if (control != null)
        {
            if (RenderMode == RenderModeType.Standard)
            {
                var bd = new BorderDrawable(control.Context);
                bd.SetBackgroundColor(BackgroundColor.ToPlatform());
                bd.SetCornerRadius(new Microsoft.Maui.CornerRadius(CornerRadius, CornerRadius, CornerRadius, CornerRadius));
                bd.SetBorderWidth(BorderThickness);
                bd.SetBorderColor(BorderColor.ToPlatform());
                var density = DeviceDisplay.MainDisplayInfo.Density;
                int padTop = (int)(Padding.Top * density);
                int padBottom = (int)(Padding.Bottom * density);
                int padLeft = (int)(Padding.Left * density);
                int padRight = (int)(Padding.Right * density);
                bd.SetPadding(new Android.Graphics.Rect(padLeft, padTop, padRight, padBottom));
                control.SetBackgroundDrawable(bd);
            }
        }
#elif WINDOWS
        var control = platformView as TextBox;
        if (control != null)
        {
            if (RenderMode == RenderModeType.Standard)
            {
                control.BorderThickness = new Microsoft.UI.Xaml.Thickness(BorderThickness);
                control.BorderBrush = BorderColor.ToPlatform();
                control.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(CornerRadius);
                control.Padding = new Microsoft.UI.Xaml.Thickness(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            }
        }
#endif
    }

    #endregion
}
