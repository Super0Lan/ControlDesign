using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace DesignLibrary
{
    public class UnableAdorner : BaseAdorner
    {
        private readonly Grid _grid;
        private Border _border;


        public UnableAdorner(UIElement uIElement) : base(uIElement) {

            _grid = new Grid();
            _border = new Border()
            {
                Background = Brushes.Transparent,
                Cursor = (Cursor)TryFindResource("UnableCursor") ?? Cursors.No,
            };

            _grid.Children.Add(_border);
            VisualCollection.Add(_grid);
        }

        public void SetEnable(bool isEnable) {
            _border.IsHitTestVisible = !isEnable;
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
    }
}
