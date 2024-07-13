using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HMExtension.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Runtime.CompilerServices;
using System.Diagnostics;
#if WINDOWS
using Microsoft.UI.Xaml.Controls;
#elif ANDROID
using Android.Views;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views.InputMethods;
using Android.Widget;
#endif
namespace HMControls;

public abstract class KeyboardlessEditor : StandardEditor
{
    public KeyboardlessEditor()
    {
        try
        {
            TapGestureRecognizer tapGesture = new()
            {
                Command = new Command(() =>
                {
                    if (ReadyForTap)
                    {
                        ActionOnFocused();
                    }
                }),
            };
            GestureRecognizers.Add(tapGesture);

            Focused += KeyboardlessEditor_Focused;
            Unfocused += (s, e) =>
            {
                ReadyForTap = false;
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    #region Properties

    private ContentPage MasterParent { get; set; } = null;

    private bool Focusable { get; set; } = true;

    private bool IsMasterParentAppear { get; set; } = true;

    private bool ReadyForTap { get; set; } = false;

    #endregion

    #region Events

    private void KeyboardlessEditor_Focused(object sender, FocusEventArgs e)
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

    protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
    {
        if (propertyName == IsFocusedProperty.PropertyName)
        {
#if ANDROID
        var context = Platform.AppContext;
        var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
        if (inputMethodManager != null)
        {
            var activity = Platform.CurrentActivity;
            var token = activity.CurrentFocus?.WindowToken;
            inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
            activity.Window.DecorView.ClearFocus();
        }
#endif
        }
        base.OnPropertyChanging(propertyName);
    }

    protected override void ModifyCustomControl()
    {
        try
        {
            base.ModifyCustomControl();
#if ANDROID
        var view = Handler.PlatformView as EditText;

        // Disable the Keyboard on Focus
        view.ShowSoftInputOnFocus = false;
        view.SetCursorVisible(false);
#elif WINDOWS
        var view = Handler.PlatformView as TextBox;
        view.IsReadOnly = true;
#endif
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("SelectableEditor --> " + ex.TargetSite + " " + ex.Message);
        }
    }

    public abstract void ActionOnFocused();
#endregion
}
