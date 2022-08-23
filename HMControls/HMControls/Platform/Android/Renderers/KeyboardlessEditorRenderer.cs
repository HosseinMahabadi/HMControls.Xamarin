using Xamarin.Forms.Platform.Android;
using XF = Xamarin.Forms;
using Android.Views;
using Android.Content;
using HMControls.Platform.Android.Renderers;
using Android.Runtime;
using Android.Views.InputMethods;
using System;
using System.Diagnostics;
using Android.Graphics.Drawables;
using System.ComponentModel;
using HMControls;

[assembly: XF.ExportRenderer(typeof(KeyboardlessEditor), typeof(KeyboardlessEditorRenderer))]

namespace HMControls.Platform.Android.Renderers
{
    class KeyboardlessEditorRenderer : StandardEditorRenderer
    {
        public SelectableEditor MainElement => Element as SelectableEditor;

        public KeyboardlessEditorRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<XF.Editor> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (e.NewElement != null)
                {
                    MainElement.PropertyChanging += OnPropertyChanging;
                }

                if (e.OldElement != null)
                {
                    MainElement.PropertyChanging -= OnPropertyChanging;
                }

                // Disable the Keyboard on Focus
                Control.ShowSoftInputOnFocus = false;
                Control.SetCursorVisible(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SelectableEditor --> " + ex.TargetSite + " " + ex.Message);
            }
        }

        private void OnPropertyChanging(object sender, XF.PropertyChangingEventArgs e)
        {
            try
            {
                // Check if the view is about to get Focus
                if (e.PropertyName == XF.VisualElement.IsFocusedProperty.PropertyName)
                {
                    // incase if the focus was moved from another Entry
                    // Forcefully dismiss the Keyboard 
                    InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(Control.WindowToken, HideSoftInputFlags.None);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SelectableEditor --> " + ex.TargetSite + " " + ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            MainElement.PropertyChanging -= OnPropertyChanging;
            base.Dispose(disposing);
        }
    }
}
