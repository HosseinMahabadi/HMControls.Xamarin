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

#if WINDOWS
using Microsoft.UI.Xaml.Controls;
#elif ANDROID
using Android.Content;
using Android.Graphics.Drawables;
//using Android.Widget;
using AndroidX.AppCompat.Widget;
#endif

namespace HMControls;

public class StandardSearchBar : SearchBar
{
    public StandardSearchBar()
    {
        try
        {
            HandlerChanged += (s, e) =>
            {
                ModifyCustomControl();
                PropertyChanged += StandardSearchBar_PropertyChanged;
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    #region Bidables

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius), 
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

    protected virtual void ModifyCustomControl()
    {
        try
        {
            if (Handler != null)
            {
                UpdateBackground(Handler.PlatformView);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    private void UpdateBackground(object platformView)
    {
        try
        {
#if ANDROID
            var control = platformView as SearchView;
            
            if (control != null)
            {
                if (RenderMode == RenderModeType.Standard)
                {
                    int searchPlateId = control.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                    Android.Views.View searchPlateView = control.FindViewById(searchPlateId);

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

                    var tgd = new GradientDrawable();
                    tgd.SetStroke(0, BorderColor.ToPlatform());
                    searchPlateView.SetBackgroundDrawable(tgd);
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void StandardSearchBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

    #endregion
}
