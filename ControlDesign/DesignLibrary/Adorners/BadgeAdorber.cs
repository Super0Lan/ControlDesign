using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DesignLibrary
{
    public class BadgeAdorber : BaseAdorner
    {
        private Grid _grid;
        private Label _label;


        public BadgeAdorber(UIElement element) : base(element)
        {
            _grid = new Grid();
            _label = new Label();
            _label.Style = (Style)TryFindResource("BadgeLabel");
            _label.Loaded += _label_Loaded;
            _label.Unloaded += _label_Unloaded;
            _grid.Children.Add(_label);
            VisualCollection.Add(_grid);
        }

        private void _label_Unloaded(object sender, RoutedEventArgs e)
        {
            _label.SizeChanged -= _label_SizeChanged;
        }

        private void _label_Loaded(object sender, RoutedEventArgs e)
        {
            ArrangeOverride(AdornedElement.RenderSize);
            _label.SizeChanged += _label_SizeChanged;
        }

        private void _label_SizeChanged(object sender, SizeChangedEventArgs e)
        {
             ArrangeOverride(AdornedElement.RenderSize);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_label.IsLoaded)
            {
                _grid.Arrange(new Rect(-_label.ActualWidth / 2, -_label.ActualHeight / 2, finalSize.Width + _label.ActualWidth, finalSize.Height + _label.ActualHeight));
            }
            else
            {
                _grid.Arrange(new Rect(finalSize));
            }
            return base.ArrangeOverride(finalSize);
        }

        public void SetContext(string v)
        {
            _label.Content = v;
        }


        public void SetAlignment(EnumAlignment newValue)
        {
            switch (newValue) {
                case EnumAlignment.TopLeft: {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Top);
                        break;
                    }
                case EnumAlignment.TopCenter:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Top);
                        break;
                    }
                case EnumAlignment.TopRight:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Right);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Top);
                        break;
                    }
                case EnumAlignment.CenterLeft:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Center);
                        break;
                    }
                case EnumAlignment.Middle:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Center);
                        break;
                    }
                case EnumAlignment.CenterRight:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Right);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Center);
                        break;
                    }
                case EnumAlignment.BottomLeft:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                        break;
                    }
                case EnumAlignment.BottomCenter:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                        break;
                    }
                case EnumAlignment.BottomRight:
                    {
                        _label.SetCurrentValue(HorizontalAlignmentProperty, HorizontalAlignment.Right);
                        _label.SetCurrentValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                        break;
                    }
            }
        }
    }
}
