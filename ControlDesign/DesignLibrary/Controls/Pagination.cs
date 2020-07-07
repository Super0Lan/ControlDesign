using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignLibrary
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DesignLibrary.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DesignLibrary.Controls;assembly=DesignLibrary.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:Pagination/>
    ///
    /// </summary>
    [TemplatePart(Name = "PrevButtonTemplateName", Type = typeof(Button))]
    [TemplatePart(Name = "NextButtonTemplateName", Type = typeof(Button))]
    [TemplatePart(Name = "PageItemsTemplateName", Type = typeof(ListBox))]
    public class Pagination : ContentControl
    {
        private const string PrevButtonTemplateName = "PART_PREV";
        private const string NextButtonTemplateName = "PART_Next";
        private const string PageItemsTemplateName = "PART_PAGE";

        private Button _prevButton;
        private Button _nextButton;
        private ListBox _pageItems;

        public override void OnApplyTemplate()
        {
            if (_prevButton != null)
            {
                _prevButton.Click -= _prevButton_Click;
            }
            if (_nextButton != null)
            {
                _nextButton.Click -= _nextButton_Click;
            }
            if (_pageItems != null)
            {
                _pageItems.SelectionChanged -= _pageItems_SelectionChanged;
            }
            base.OnApplyTemplate();
            _prevButton = GetTemplateChild(PrevButtonTemplateName) as Button;
            _nextButton = GetTemplateChild(NextButtonTemplateName) as Button;
            _pageItems = GetTemplateChild(PageItemsTemplateName) as ListBox;
            if (_prevButton != null)
            {
                _prevButton.Click += _prevButton_Click;
            }
            if (_nextButton != null)
            {
                _nextButton.Click += _nextButton_Click;
            }
            if (_pageItems != null)
            {
                _pageItems.ItemsSource = Pages;
                _pageItems.SelectionChanged += _pageItems_SelectionChanged;
            }
            InitPager();
        }

        private void _pageItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is PaginationButton pagination)
            {
                switch (pagination.Value)
                {
                    case "Pre":
                        {
                            SetCurrentValue(CurrentPageProperty, Math.Max(1, CurrentPage - PagerCount - 2));
                            break;
                        }
                    case "Next":
                        {
                            SetCurrentValue(CurrentPageProperty, Math.Min(PageCount, CurrentPage + PagerCount - 2));
                            break;
                        }
                    default:
                        {
                            if (int.TryParse(pagination.Value, out int num))
                            {
                                SetCurrentValue(CurrentPageProperty, num);
                            }
                            break;
                        }
                }
            }
        }

        private void _nextButton_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(CurrentPageProperty, CurrentPage + 1);
        }

        private void _prevButton_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(CurrentPageProperty, CurrentPage - 1);
        }

        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }

        #region 每页显示条目个数
        /// <summary>
        /// 每页显示条目个数
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pagination), new PropertyMetadata(10, InitPagination));

        #endregion

        #region 总页数
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(Pagination), new PropertyMetadata(0, InitPagination));
        #endregion

        #region 总条目数
        /// <summary>
        /// 总条目数
        /// </summary>
        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Total.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(Pagination), new PropertyMetadata(0));
        #endregion

        #region 当前页数
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pagination), new PropertyMetadata(1, InitPagination));
        #endregion

        #region 页码按钮的数量，当总页数超过该值时会折叠  大于等于 5 且小于等于 21 的奇数


        public int PagerCount
        {
            get { return (int)GetValue(PagerCountProperty); }
            set { SetValue(PagerCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PagerCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagerCountProperty =
            DependencyProperty.Register("PagerCount", typeof(int), typeof(Pagination), new PropertyMetadata(7, InitPagination), OnPagerCountPropertyValidate);

        private static bool OnPagerCountPropertyValidate(object value)
        {
            return int.TryParse(value?.ToString(), out int num) && (num & 1) != 0 && num >= 5 && num <= 21;
        }


        #endregion

        #region 页码按钮


        protected ObservableCollection<PaginationButton> Pages { get; set; } = new ObservableCollection<PaginationButton>();


        #endregion

        private void InitPager()
        {
            var pageCount = PageCount;
            var currentPage = CurrentPage;
            var pagerCount = PagerCount;

            var startPageIndex = currentPage - pagerCount / 2;
            startPageIndex = startPageIndex < 1 ? 1 : startPageIndex;

            var endPageIndex = startPageIndex + pagerCount - 1;
            endPageIndex = endPageIndex > pageCount ? pageCount : endPageIndex;

            startPageIndex = endPageIndex - pagerCount + 1;
            startPageIndex = startPageIndex < 1 ? 1 : startPageIndex;

            Pages.Clear();

            if (startPageIndex > 1)
            {
                Pages.Add(new PaginationButton("1"));
            }

            if (currentPage > 4 && pagerCount > 6)
            {
                Pages.Add(new PaginationButton() { Text = "...", Value = "Pre" });
                startPageIndex++;
            }

            for (int index = startPageIndex; index <= endPageIndex; index++)
            {
                Pages.Add(new PaginationButton(index.ToString()));
                if (currentPage == index && _pageItems != null)
                {
                    _pageItems.SelectedItem = Pages.FirstOrDefault(x => x.Value == currentPage.ToString());
                    if (currentPage == pageCount) {
                        Thread.Sleep(150);
                    }
                }
            }

            if (currentPage < pageCount - 3 && pageCount > 6)
            {
                Pages.Add(new PaginationButton() { Text = "...", Value = "Next" });
            }

            if (endPageIndex < pageCount)
            {
                Pages.Add(new PaginationButton(pageCount.ToString()));
            }
            _prevButton?.SetCurrentValue(IsEnabledProperty, currentPage != 1);
            _nextButton?.SetCurrentValue(IsEnabledProperty, currentPage != pageCount);
        }

        private static void InitPagination(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination pagination)
            {
                pagination.InitPager();
            }
        }
    }

    public class PaginationButton
    {

        public PaginationButton() { }

        public PaginationButton(string str)
        {
            Text = str;
            Value = str;
        }

        public string Text { get; set; }

        public string Value { get; set; }
    }
}
