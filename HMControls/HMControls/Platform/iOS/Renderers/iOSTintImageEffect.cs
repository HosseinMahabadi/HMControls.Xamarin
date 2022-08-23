using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HMExtension.Xamarin.Component;
using HMExtension.Xamarin.Platform.iOS.Renderers;
using CrossPlatformEffect = HMControls.TintImageEffect;

namespace HMControls.Platform.iOS.Renderers
{
    public class iOSTintImageEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (CrossPlatformEffect)Element.Effects.FirstOrDefault(e => e is CrossPlatformEffect);

                if (effect == null)
                    return;

                if (Control is UIImageView image)
                {
                    image.Image = image.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                    image.TintColor = effect.TintColor.ToUIColor();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred when setting the {typeof(iOSTintImageEffect)} effect: {ex.Message}\n{ex.StackTrace}");
            }
        }

        protected override void OnDetached() { }
    }
}