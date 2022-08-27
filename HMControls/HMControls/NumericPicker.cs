using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using HMExtension.Xamarin;
using HMPopup;

namespace HMControls
{
    public class NumericPicker : KeyboardlessEntry
    {
        private Popup Popup { get; set; } = new Popup()
        {
            FlowDirection = FlowDirection.RightToLeft,
            OkTitle = "تایید",
            CancelTitle = "انصراف",
            YesTitle = "بله",
            NoTitle = "خیر",
            SelectTitle = "انتخاب",
        };

        public NumericPicker()
        {
            SetBinding(TextProperty, new Binding()
            {
                Source = this,
                Path = nameof(SelectedItem)
            });
        }

        public override async void ActionOnFocused()
        {
            int answer = await Popup.ShowSelectionAsync(Title,
                Message,
                Items,
                SelectedItem);

            SelectedItem = answer;
        }

        private List<int> Items { get; set; } = new List<int>() { 0 };

        public static BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(int), typeof(NumericPicker), 0, propertyChanged: OnRangeChanged);

        public static BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(NumericPicker), 0, propertyChanged: OnRangeChanged);

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(int), typeof(NumericPicker));

        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(NumericPicker), null);

        public static BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(string), typeof(NumericPicker), null);

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public int SelectedItem
        {
            get => (int)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        private static void OnRangeChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as NumericPicker;
                Object.OnRangeChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

        private void OnRangeChanged()
        {
            Items.Clear();
            for (int i = MinValue; i <= MaxValue; i++)
            {
                Items.Add(i);
            }
        }

    }
}
