using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HMExtension.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using sun.awt.im;
using System.Runtime.CompilerServices;
#if ANDROID
using Android.Views;
using Android.Content;
using Android.Runtime;
using Android.Views.InputMethods;
using Android.Graphics.Drawables;
#endif
namespace HMControls
{
    public abstract class KeyboardlessEditor : StandardEditor, ICustomControl
    {
        public KeyboardlessEditor()
        {
            Focused += KeyboardlessEditor_Focused;
        }

        #region Properties

        private ContentPage MasterParent { get; set; } = null;

        private bool Focusable { get; set; } = true;

        private bool IsMasterParentAppear { get; set; } = true;

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

        public abstract void ActionOnFocused();

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
            if (propertyName == IsFocusedProperty.PropertyName)
            {
#if ANDROID
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(Control.WindowToken, HideSoftInputFlags.None);
#endif
            }
        }

#endregion
    }
}
