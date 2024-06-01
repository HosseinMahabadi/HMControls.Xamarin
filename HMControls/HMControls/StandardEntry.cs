using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Platform;
using System.Diagnostics;
using HMExtension.Maui;

#if NET8_0_WINDOWS10_0_19041_0
using Microsoft.UI.Xaml.Controls;
#elif NET8_0_ANDROID
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using AndroidX.AppCompat.Widget;
#endif

namespace HMControls;

public class StandardEntry : Entry, ICustomControl
{
    public StandardEntry() 
    {
        try
        {
            HandlerChanged += (s, e) => 
            { 
                ModifyCustomControl(); 
            };
            ModifyCustomControl();
        }
        catch(Exception ex) 
        {
            Debug.WriteLine($"Standard Entry ERROR!!! => {ex.Message}");
        }
    }

    #region Bindables

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius), 
        typeof(int), 
        typeof(StandardEntry),
        default,
        propertyChanged: OnPropertyChanged);

    public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(
        nameof(BorderThickness), 
        typeof(double), 
        typeof(StandardEntry), 
        1d,
        propertyChanged: OnPropertyChanged);

    public static readonly BindableProperty PaddingProperty = BindableProperty.Create(
        nameof(Padding), 
        typeof(Thickness), 
        typeof(StandardEntry), 
        new Thickness(5),
        propertyChanged: OnPropertyChanged);

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor), 
        typeof(Color), 
        typeof(StandardEntry), 
        Colors.Black, 
        propertyChanged: OnPropertyChanged);

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
#if NET8_0_ANDROID
            UpdateBackgroundAndroid(Handler.PlatformView);
#elif NET8_0_WINDOWS10_0_19041_0
            UpdateBackgroundWindows(Handler.PlatformView);
#endif
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var owner = (StandardEntry)bindable;
#if NET8_0_ANDROID
            owner.UpdateBackgroundAndroid(owner.Handler.PlatformView);
#elif NET8_0_WINDOWS10_0_19041_0
            owner.UpdateBackgroundWindows(owner.Handler.PlatformView);
#endif
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

#if NET8_0_ANDROID
    private void UpdateBackgroundAndroid(object? platformView)
    {
        try
        {
            var control = platformView as AppCompatEditText;
            if (control != null)
            {
                Debug.WriteLine(control);
                if (RenderMode == RenderModeType.Standard)
                {
                    var gd = new GradientDrawable();
                    Debug.WriteLine("var gd = new GradientDrawable();");
                    
                    gd.SetColor(BackgroundColor.ToInt());
                    Debug.WriteLine("gd.SetColor(BackgroundColor.ToPlatform());");

                    gd.SetCornerRadius((float)CornerRadius);
                    Debug.WriteLine("gd.SetCornerRadius(CornerRadius);");

                    gd.SetStroke((int)BorderThickness, BorderColor.ToPlatform());
                    Debug.WriteLine("gd.SetStroke((int)BorderThickness, BorderColor.ToPlatform());");

                    control.SetBackgroundDrawable(gd);
                    Debug.WriteLine("control.SetBackgroundDrawable(gd);");

                    var padTop = (int)Padding.Top;
                    var padBottom = (int)Padding.Bottom;
                    var padLeft = (int)Padding.Left;
                    var padRight = (int)Padding.Right;

                    control.SetPadding(padLeft, padTop, padRight, padBottom);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
#endif

#if NET8_0_WINDOWS10_0_19041_0
    private void UpdateBackgroundWindows(object platformView)
    {
        try
        {
            TextBox control = platformView as TextBox;
            if (control != null)
            {
                Debug.WriteLine(control);
                if (RenderMode == RenderModeType.Standard)
                {
                    control.BorderThickness = new Microsoft.UI.Xaml.Thickness(BorderThickness);
                    control.BorderBrush = BorderColor.ToPlatform();
                    control.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(CornerRadius);
                    control.Padding = new Microsoft.UI.Xaml.Thickness(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
                }
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

#endif

    #endregion
}
