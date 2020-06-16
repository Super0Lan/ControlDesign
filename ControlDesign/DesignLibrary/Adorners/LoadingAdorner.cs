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
        private readonly Grid _grid;
        private Path _path;


        public LoadingAdorner(UIElement uIElement) : base(uIElement)
        {

            _grid = new Grid() {
                   Background = Brushes.Transparent,
                   Cursor = Cursors.Wait,
                   ForceCursor = true,
            };
            _path = new Path()
            {
                Width = 24,
                Height = 24,
                Fill = Brushes.Black,
                Stretch = Stretch.Uniform,
                RenderTransformOrigin = new Point(0.5,0.5),
            };
            Assists.SetIcon(_path, EnumIcon.Loading);
            _grid.Children.Add(_path);
            VisualCollection.Add(_grid);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _grid.Arrange(new Rect(finalSize));
            return base.ArrangeOverride(finalSize);

        }

        public void SetStatus(bool newValue)
        {
            if (!newValue)
            {
                _path.RenderTransform = null;
            }
            else
            {
                RotateTransform rtf = new RotateTransform();
                _path.RenderTransform = rtf;
                _path.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation(0, 360, new Duration(TimeSpan.FromMilliseconds(2000)))
                {
                    BeginTime = new TimeSpan(0),
                    RepeatBehavior = RepeatBehavior.Forever,
                });
            }
        }
    }
}
