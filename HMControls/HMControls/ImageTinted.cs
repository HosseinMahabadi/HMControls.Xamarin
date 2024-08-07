﻿using HMExtension.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace HMControls
{
    public class ImageTinted : Image
    {
        public ImageTinted()
        {
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == SourceProperty.PropertyName)
                {
                    AddTintEffect();
                }
            };
        }

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor),
            typeof(Color),
            typeof(ImageTinted),
            defaultValue: Colors.Black,
            propertyChanged: OnTintColorChanged);

        private static void OnTintColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ImageTinted current = (ImageTinted)bindable;
            current.OnTintColorChanged((Color)newValue);
        }

        private void OnTintColorChanged(Color newValue)
        {
            RemoveTintEffect();
            if (newValue != Colors.Black)
            {
                AddTintEffect();
            }
        }

        private void RemoveTintEffect()
        {
            Effect effect = Effects.FirstOrDefault(e => e is TintImageEffect);
            if (effect != null)
            {
                Effects.Remove(effect);
            }
        }
        private void AddTintEffect()
        {
            if (Source != null)
            {
                RemoveTintEffect();
                TintImageEffect tintEffect = new TintImageEffect() { TintColor = TintColor };
                //TintImageEffect tintEffect = Effect.Resolve($"{TintImageEffect.GroupName}.{TintImageEffect.Name}") as TintImageEffect;
                Effects.Add(tintEffect);
            }
        }
    }
}
