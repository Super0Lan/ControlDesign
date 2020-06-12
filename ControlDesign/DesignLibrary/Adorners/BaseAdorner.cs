using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DesignLibrary
{
    public class BaseAdorner : Adorner
    {
        /// <summary>
        /// 装饰器上的Visual集合
        /// </summary>
        protected virtual VisualCollection VisualCollection { get; set; }

        protected override int VisualChildrenCount => VisualCollection.Count;

        protected override Visual GetVisualChild(int index) => VisualCollection[index];

        public BaseAdorner(UIElement element) : base(element) {
            VisualCollection = new VisualCollection(this);
        }
    }
}
