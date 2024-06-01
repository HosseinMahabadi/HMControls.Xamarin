using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Platform;
#if ANDROID
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using AV = Android.Views;
#endif

namespace HMControls;

public class StandardSearchBar : SearchBar
{
    #region Bidables

    public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), 
        typeof(int), 
        typeof(StandardSearchBar), 
        0);

    public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), 
        typeof(int), 
        typeof(StandardSearchBar), 
        0);

    public static BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), 
        typeof(Thickness), 
        typeof(StandardSearchBar), 
        new Thickness(5));

    public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), 
        typeof(Color), 
        typeof(StandardSearchBar), 
        Colors.Transparent);

    #endregion

    #region Properties

    public RenderModeType RenderMode { get; set; } = RenderModeType.Standard;

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public int BorderThickness
    {
        get => (int)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }
    /// <summary>
    /// This property cannot be changed at runtime in iOS.
    /// </summary>
    public Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    #endregion

    #region Methods

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (propertyName == CornerRadiusProperty.PropertyName)
        {
#if ANDROID
            UpdateBackgroundAndroid(ref Handler.PlatformView as SearchView);
#endif
        }
        else if (propertyName == BorderThicknessProperty.PropertyName)
        {
#if ANDROID
            UpdateBackgroundAndroid(ref Handler.PlatformView as SearchView);
#endif
        }
        else if (propertyName == BorderColorProperty.PropertyName)
        {
#if ANDROID
            UpdateBackgroundAndroid(ref Handler.PlatformView as SearchView);
#endif
        }

        base.OnPropertyChanged(propertyName);
    }

#if ANDROID
    protected void UpdateBackgroundAndroid(ref SearchView control)
    {
        if (control != null)
        {
            if (RenderMode == RenderModeType.Standard)
            {
                int searchPlateId = control.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                AV.View searchPlateView = control.FindViewById(searchPlateId);

                var gd = new GradientDrawable();
                gd.SetColor(BackgroundColor.ToPlatform());
                gd.SetCornerRadius(Context.ToPixels(CornerRadius));
                gd.SetStroke((int)Context.ToPixels(BorderThickness), BorderColor.ToAndroid());
                control.SetBackground(gd);

                var tgd = new GradientDrawable();
                tgd.SetStroke(0, BorderColor.ToPlatform());
                searchPlateView.SetBackground(tgd);

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
