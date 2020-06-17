using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace DesignLibrary.Extensions
{
    public static class UIElementExtensions
    {
        /// <summary>
        /// 添加并获取对应类型的装饰器
        /// </summary>
        /// <param name="uIElement">需要添加装饰器的控件</param>
        /// <param name="type">装饰器类型</param>
        /// <returns></returns>
        public static Adorner GetOrAddAdorner(this UIElement uIElement,Type type) {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(uIElement);
            if (adornerLayer == null)
            {
                throw new Exception("VisualParents Must have AdornerDecorator!");
            }
            var adorner = adornerLayer.GetAdorners(uIElement)?.FirstOrDefault(x => x?.GetType() == type);
            if (adorner == null)
            {
                lock (uIElement)
                {
                    if (adorner == null)
                    {
                        adorner = (Adorner)Activator.CreateInstance(type, new object[] { uIElement });
                        adornerLayer.Add(adorner);
                    }
                }
            }
            return adorner;
        }

        /// <summary>
        /// 只获取对应类型的装饰器
        /// </summary>
        /// <param name="uIElement">需要添加装饰器的控件</param>
        /// <param name="type">装饰器类型</param>
        /// <returns></returns>
        public static Adorner GetAdorner(this UIElement uIElement, Type type)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(uIElement);
            if (adornerLayer == null)
            {
                return null;
            }
            return adornerLayer.GetAdorners(uIElement)?.FirstOrDefault(x => x?.GetType() == type);
        }
    }
}
