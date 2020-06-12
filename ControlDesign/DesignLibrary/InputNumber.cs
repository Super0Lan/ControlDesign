using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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

namespace InputNumber
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:InputNumber"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:InputNumber;assembly=InputNumber"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = DecreaseButtonTemplateName, Type = typeof(Button))]
    [TemplatePart(Name = IncreaseButtonTemplateName, Type = typeof(Button))]
    [TemplatePart(Name = NumberTextBoxTemplateName, Type = typeof(TextBox))]
    public class InputNumber : Control
    {
        static InputNumber()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputNumber), new FrameworkPropertyMetadata(typeof(InputNumber)));
        }

        private const string DecreaseButtonTemplateName = "PART_DecreaseButton";
        private const string IncreaseButtonTemplateName = "PART_IncreaseButton";
        private const string NumberTextBoxTemplateName = "PART_NumberTextBox";

        private Button _decreaseButton;
        private Button _increaseButton;
        private TextBox _numberTextBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _decreaseButton = GetTemplateChild(DecreaseButtonTemplateName) as Button;
            _increaseButton = GetTemplateChild(IncreaseButtonTemplateName) as Button;
            _numberTextBox = GetTemplateChild(NumberTextBoxTemplateName) as TextBox;
            if (_decreaseButton != null)
            {
                _decreaseButton.Click += _decreaseButton_Click;
            }
            if (_increaseButton != null)
            {
                _increaseButton.Click += _increaseButton_Click;
            }


        }

        private void _increaseButton_Click(object sender, RoutedEventArgs e)
        {
            Number = (Number ?? 0) + Step;
        }

        private void _decreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Number = (Number ?? 0) - Step;
        }

        protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
        {
            base.OnTemplateChanged(oldTemplate, newTemplate);
        }

        public new bool Focus()
        {
            return _numberTextBox.Focus();
        }

        #region 设置计数器允许的最小值



        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(double), typeof(InputNumber), new PropertyMetadata(double.MinValue));
        #endregion

        #region  设置计数器允许的最大值
        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(double), typeof(InputNumber), new PropertyMetadata(double.MaxValue));
        #endregion

        #region 计数器步长
        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(InputNumber), new PropertyMetadata(1.0));
        #endregion

        #region 是否只能输入 step 的倍数


        public bool StepStrictly
        {
            get { return (bool)GetValue(StepStrictlyProperty); }
            set { SetValue(StepStrictlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StepStrictly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepStrictlyProperty =
            DependencyProperty.Register("StepStrictly", typeof(bool), typeof(InputNumber), new PropertyMetadata(false));


        #endregion

        #region 数值精度 --  precision 的值必须是一个非负整数，并且不能小于 step 的小数位数。


        public int Precision
        {
            get { return (int)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Precision.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.Register("Precision", typeof(int), typeof(InputNumber), new PropertyMetadata(0));


        #endregion

        #region 是否使用控制按钮


        public bool Controls
        {
            get { return (bool)GetValue(ControlsProperty); }
            set { SetValue(ControlsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Controls.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlsProperty =
            DependencyProperty.Register("Controls", typeof(bool), typeof(InputNumber), new PropertyMetadata(true));


        #endregion

        #region 控制按钮位置



        public EnumPosition ControlsPosition
        {
            get { return (EnumPosition)GetValue(ControlsPositionProperty); }
            set { SetValue(ControlsPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlsPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlsPositionProperty =
            DependencyProperty.Register("ControlsPosition", typeof(EnumPosition), typeof(InputNumber), new PropertyMetadata(EnumPosition.Default));


        #endregion

        #region 输入框默认 placeholder


        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string), typeof(InputNumber), new PropertyMetadata(string.Empty));


        #endregion

        #region 计数器的值
        public double? Number
        {
            get { return (double?)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Number.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(double?), typeof(InputNumber), new PropertyMetadata(null,NumberPropertyChanged));

        private static void NumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InputNumber number) {
                double max = number.Max;
                double min = number.Min;
                number._decreaseButton.IsEnabled = (double)e.NewValue > min;
                number._increaseButton.IsEnabled = (double)e.NewValue < max;
            };
        }
        #endregion

        #region 表示矩形的角的半径。


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(InputNumber), new PropertyMetadata(new CornerRadius(0)));


        #endregion
    }

    public enum EnumPosition
    {
        Default,
        Left,
        Right,
    }

    public class NumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
