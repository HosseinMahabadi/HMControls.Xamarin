using HMExtension.Xamarin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HMControls
{
    public class ImageButtonTinted : ImageButton
    {
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create("TintColor",
            typeof(Color),
            typeof(ImageButtonTinted),
            defaultValue: Color.Red,
            propertyChanged: OnTintColorChanged);

        private static void OnTintColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ImageButtonTinted current = (ImageButtonTinted)bindable;
            current.OnTintColorChanged((Color)newValue);
        }

        private void OnTintColorChanged(Color newValue)
        {
            AddTintEffect();
        }

        private void RemoveTintEffect()
        {
            Effect effect = Effects.FirstOrDefault(e => e is TintImageEffect);
            if (effect != null)
            {
                _ = Effects.Remove(effect);
            }
        }
        private void AddTintEffect()
        {
            RemoveTintEffect();
            TintImageEffect tintEffect = new TintImageEffect() { TintColor = TintColor };
            Effects.Add(tintEffect);
        }
    }
}
