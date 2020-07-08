using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesignLibrary
{
    public class PaginationButton : Button
    {
        #region 是否为当前页码
        public bool IsCurrentPage
        {
            get { return (bool)GetValue(IsCurrentPageProperty); }
            set { SetValue(IsCurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCurrentPageProperty =
            DependencyProperty.Register("IsCurrentPage", typeof(bool), typeof(PaginationButton), new PropertyMetadata(false));
        #endregion

        #region 按钮类型 PagintionButtonType


        public PagintionButtonType PagintionButtonType
        {
            get { return (PagintionButtonType)GetValue(PagintionButtonTypeProperty); }
            set { SetValue(PagintionButtonTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagintionButtonTypeProperty =
            DependencyProperty.Register("PagintionButtonType", typeof(PagintionButtonType), typeof(PaginationButton), new PropertyMetadata(PagintionButtonType.Normal));


        #endregion
    }

    public enum PagintionButtonType
    {
        /// <summary>
        /// 上一页
        /// </summary>
        Prev,
        /// <summary>
        /// 正常页码
        /// </summary>
        Normal,
        /// <summary>
        /// 下一页
        /// </summary>
        Next,
    }
}
