using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HMExtension.Xamarin;

namespace HMControls
{
    public class SelectableItem<T> : ViewModelBase
    {
        public SelectableItem()
        {
            Application.Current.RequestedThemeChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(TextColor));
            };
        }

        public Color DarkColor { get; set; } = HMControlsColors.MainLightColor;
        public Color LightColor { get; set; } = HMControlsColors.MainLightColor;
        public Color SelectedDarkColor { get; set; } = HMControlsColors.ThirdColor;
        public Color SelectedLightColor { get; set; } = HMControlsColors.SecondaryColor;
        private bool IsSelected { get; set; } = false;

        private T _title;
        public T Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public Color TextColor
        {
            get
            {
                if (IsSelected)
                {
                    return GetBasedOnTheme(SelectedDarkColor, SelectedLightColor);
                }
                else
                {
                    return GetBasedOnTheme(DarkColor, LightColor);
                }
            }
        }

        private double _scale = 1;
        public double Scale
        {
            get => _scale;
            set => SetProperty(ref _scale, value);
        }

        public void Select()
        {
            IsSelected = true;
            Scale = 1.5;
            OnPropertyChanged(nameof(TextColor));
        }

        public void UnSelect()
        {
            IsSelected = false;
            Scale = 1;
            OnPropertyChanged(nameof(TextColor));
        }
    }
}
