using DesignLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesignLibrary
{
    public class Assists
    {
        #region 控件IsEnable 属性,用于重写IsEnable样式
        public static bool GetIsEnable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnableProperty);
        }

        public static void SetIsEnable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnableProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnableProperty =
            DependencyProperty.RegisterAttached("IsEnable", typeof(bool), typeof(Assists), new PropertyMetadata(true, IsEnablePropertyChanged));

        private static void IsEnablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uIElement)
            {
                var adorner = (UnableAdorner)uIElement.GetOrAddAdorner(typeof(UnableAdorner));
                adorner.SetEnable((bool)e.NewValue);
            }
        }
        #endregion

        #region 表示矩形的角的半径。
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(Assists), new PropertyMetadata(new CornerRadius(0)));
        #endregion

        #region 是否是圆形控件
        public static bool GetIsCircle(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCircleProperty);
        }

        public static void SetIsCircle(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCircleProperty, value);
        }

        // Using a DependencyProperty as the backing store for Circle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCircleProperty =
            DependencyProperty.RegisterAttached("IsCircle", typeof(bool), typeof(Assists), new PropertyMetadata(false, CirclePropertyChanged));

        private static void CirclePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement uI)
            {
                if ((bool)e.NewValue)
                {
                    uI.Loaded += UI_Loaded;
                }
                else
                {
                    uI.Loaded -= UI_Loaded;
                    uI.Clip = null;
                }
            }

        }

        private static void UI_Loaded(object sender, RoutedEventArgs e)
        {
            var uI = sender as FrameworkElement;
            var size = Math.Min(uI.ActualWidth, uI.ActualHeight) / 2;
            uI.Clip = new EllipseGeometry()
            {
                RadiusX = size,
                RadiusY = size,
                Center = new Point(uI.ActualWidth / 2, uI.ActualHeight / 2)
            };
        }
        #endregion

        #region Badge 标记
        #region 标记内容
            public static string GetBadge(DependencyObject obj)
            {
                return (string)obj.GetValue(BadgeProperty);
            }

            public static void SetBadge(DependencyObject obj, string value)
            {
                obj.SetValue(BadgeProperty, value);
            }

            // Using a DependencyProperty as the backing store for Badge.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty BadgeProperty =
                DependencyProperty.RegisterAttached("Badge", typeof(string), typeof(Assists), new PropertyMetadata(null, BadgePropertyChanged));

            private static void BadgePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is UIElement uIElement)
                {
                    var adorner = (BadgeAdorber)uIElement.GetOrAddAdorner(typeof(BadgeAdorber));
                    adorner.SetContext(e.NewValue?.ToString());
                }
            }
        #endregion

        #region 标记位置
        public static EnumAlignment GetBadgeAlignment(DependencyObject obj)
        {
            return (EnumAlignment)obj.GetValue(BadgeAlignmentProperty);
        }

        public static void SetBadgeAlignment(DependencyObject obj, EnumAlignment value)
        {
            obj.SetValue(BadgeAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for BadgeAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BadgeAlignmentProperty =
            DependencyProperty.RegisterAttached("BadgeAlignment", typeof(EnumAlignment), typeof(Assists), new PropertyMetadata(EnumAlignment.TopRight, BadgeAlignmentPropertyChanged));

        private static void BadgeAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uIElement)
            {
                var adorner = (BadgeAdorber)uIElement.GetOrAddAdorner(typeof(BadgeAdorber));
                adorner.SetAlignment((EnumAlignment)e.NewValue);
            }
        }
        #endregion

        #region 是否隐藏标记
        public static bool GetHideBadge(DependencyObject obj)
        {
            return (bool)obj.GetValue(HideBadgeProperty);
        }

        public static void SetHideBadge(DependencyObject obj, bool value)
        {
            obj.SetValue(HideBadgeProperty, value);
        }

        // Using a DependencyProperty as the backing store for HideBadge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HideBadgeProperty =
            DependencyProperty.RegisterAttached("HideBadge", typeof(bool), typeof(Assists), new PropertyMetadata(false, HideBadgePropertyChanged));

        private static void HideBadgePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uIElement)
            {
                var adorner = (BadgeAdorber)uIElement.GetOrAddAdorner(typeof(BadgeAdorber));
                adorner.Visibility = (bool)e.NewValue ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        #endregion

        #endregion

        #region 图标Icon


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
            DependencyProperty.RegisterAttached("Icon", typeof(EnumIcon), typeof(Assists), new PropertyMetadata(default(EnumIcon), IconPropertyChanged));

        private static void IconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Path path) {
                if (Enum.TryParse(e.NewValue.ToString(), out EnumIcon icon))
                {
                    var source = Geometry.Parse(IconDataFactory.IconDic[icon]);
                    path.Data = source;
                }
            }
        }

        #endregion
    }


}
