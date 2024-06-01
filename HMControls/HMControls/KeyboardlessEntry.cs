using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HMExtension.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using sun.awt.im;
using Microsoft.Maui.Controls.PlatformConfiguration;
#if ANDROID
using Android.Views;
using Android.Content;
using Android.Graphics.Drawables;
#endif

namespace HMControls;

public abstract class KeyboardlessEntry : StandardEntry
{
    public KeyboardlessEntry()
    {
        try
        {
            Focused += KeyboardlessEntry_Focused;
            HandlerChanged += KeyboardlessEntry_HandlerChanged;
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"KeyboardlessEntry ERROR!!! => {ex.Message}");
        }
    }

    private void KeyboardlessEntry_HandlerChanged(object sender, EventArgs e)
    {
        try
        {
            if (sender is KeyboardlessEntry control)
            {
                control.PropertyChanging += Control_PropertyChanging;
#if ANDROID
                var view = control.Handler.PlatformView as Android.Widget.EditText;

                // Disable the Keyboard on Focus
                view.ShowSoftInputOnFocus = false;
                view.SetCursorVisible(false);
#endif
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("SelectableEditor --> " + ex.TargetSite + " " + ex.Message);
        }
    }

    private void Control_PropertyChanging(object sender, PropertyChangingEventArgs e)
    {
#if ANDROID
            try
            {
                // Check if the view is about to get Focus
                if (e.PropertyName == Maui.VisualElement.IsFocusedProperty.PropertyName)
                {
                    // incase if the focus was moved from another Entry
                    // Forcefully dismiss the Keyboard 
                    InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                    var view = control.Handler.PlatformView as Android.Widget.EditText;
                    imm.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SelectableEditor --> " + ex.TargetSite + " " + ex.Message);
            }
#endif
    }

    #region Properties

    private ContentPage MasterParent { get; set; } = null;

    private bool Focusable { get; set; } = true;

    private bool IsMasterParentAppear { get; set; } = true;

    #endregion

    #region Events

    private void KeyboardlessEntry_Focused(object sender, FocusEventArgs e)
    {
        if (MasterParent == null)
        {
            MasterParent = this.GetParent<ContentPage>();
            if (MasterParent != null)
            {
                MasterParent.Appearing -= MasterParent_Appearing;
                MasterParent.Disappearing -= MasterParent_Disappearing;

                MasterParent.Appearing += MasterParent_Appearing;
                MasterParent.Disappearing += MasterParent_Disappearing;
            }
        }

        if (Focusable && IsMasterParentAppear)
        {
            ActionOnFocused();
        }
        else
        {
            Unfocus();
        }
    }

    private async void MasterParent_Appearing(object sender, EventArgs e)
    {
        Focusable = false;
        await Task.Delay(200);
        Focusable = true;
        IsMasterParentAppear = true;
    }

    private void MasterParent_Disappearing(object sender, EventArgs e)
    {
        IsMasterParentAppear = false;
    }

    #endregion

    #region Methods

    private void ThisOnPropertyChanging()
    { }
    public abstract void ActionOnFocused();

    protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanging(propertyName);

        try
        {
            if (propertyName == IsFocusedProperty.PropertyName)
            {
#if ANDROID
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard
                var view = (Handler.PlatformView as Android.Widget.EditText);
                InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
#endif
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
#endregion
}
