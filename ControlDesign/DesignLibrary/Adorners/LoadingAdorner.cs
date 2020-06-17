using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DesignLibrary
{
    public class LoadingAdorner:BaseAdorner
    {
        #region 是否Loading

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingAdorner), new PropertyMetadata(false));

        #endregion

        #region 显示在加载图标下方的加载文案


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LoadingAdorner), new PropertyMetadata(string.Empty));


        #endregion

        #region 加载图标类名


        public EnumIcon Icon
        {
            get { return (EnumIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(EnumIcon), typeof(LoadingAdorner), new PropertyMetadata(EnumIcon.Loading));


        #endregion

        private Control _control;

        public LoadingAdorner(UIElement uIElement) : base(uIElement)
        {
            _control = new Control() {
                Style = (Style)FindResource("Loading")
            };
            VisualCollection.Add(_control);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _control.Arrange(new Rect(finalSize));
            return base.ArrangeOverride(finalSize);
        }
    }

}
