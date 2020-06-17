using DesignLibrary.Extensions;
using System;
using System.Windows;

namespace DesignLibrary
{
    public class LoadingAssists
    {
        #region 是否Loading
        public static bool GetLoading(DependencyObject obj)
        {
            return (bool)obj.GetValue(LoadingProperty);
        }

        public static void SetLoading(DependencyObject obj, bool value)
        {
            obj.SetValue(LoadingProperty, value);
        }

        // Using a DependencyProperty as the backing store for Loading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadingProperty =
            DependencyProperty.RegisterAttached("Loading", typeof(bool), typeof(LoadingAssists), new PropertyMetadata(false, LoadingPropertyChanged));

        private static void LoadingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uIElement)
            {
                var adorner = (LoadingAdorner)uIElement.GetOrAddAdorner(typeof(LoadingAdorner));
                adorner.SetCurrentValue(LoadingAdorner.IsLoadingProperty, e.NewValue);
            }
        }
        #endregion

        #region 显示在加载图标下方的加载文案
        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(LoadingAssists), new PropertyMetadata(default,TextPropertyChanged));

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uIElement)
            {
                var adorner = uIElement.GetOrAddAdorner(typeof(LoadingAdorner));
                adorner.SetCurrentValue(LoadingAdorner.TextProperty, e.NewValue);
            }
        }
        #endregion

        #region 加载图标类名
        public static EnumIcon GetIcon(DependencyObject obj)
        {
            return (EnumIcon)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, EnumIcon value)
        {
            obj.SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached("Icon", typeof(EnumIcon), typeof(LoadingAssists), new PropertyMetadata(EnumIcon.Loading,IconPropertyChanged));

        private static void IconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uIElement)
            {
                var adorner = uIElement.GetOrAddAdorner(typeof(LoadingAdorner));
                adorner.SetCurrentValue(LoadingAdorner.IconProperty, e.NewValue);
            }
        }
        #endregion
    }
}
