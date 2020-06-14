using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace DesignLibrary
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
    public class InputNumber : TextBox
    {

        static InputNumber()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputNumber), new FrameworkPropertyMetadata(typeof(InputNumber)));
        }

        public InputNumber() : base() {
            ///禁用输入法
            //System.Windows.Input.InputMethod.SetIsInputMethodEnabled(this,false);
        }

        private const string DecreaseButtonTemplateName = "PART_DecreaseButton";
        private const string IncreaseButtonTemplateName = "PART_IncreaseButton";

        private Button _decreaseButton;
        private Button _increaseButton;

        public override void OnApplyTemplate()
        {
            if (_decreaseButton != null)
            {
                _decreaseButton.Click -= _decreaseButton_Click;
            }
            if (_increaseButton != null)
            {
                _increaseButton.Click -= _increaseButton_Click;
            }

            base.OnApplyTemplate();
            _decreaseButton = GetTemplateChild(DecreaseButtonTemplateName) as Button;
            _increaseButton = GetTemplateChild(IncreaseButtonTemplateName) as Button;
            if (_decreaseButton != null)
            {
                _decreaseButton.Click += _decreaseButton_Click;
            }
            if (_increaseButton != null)
            {
                _increaseButton.Click += _increaseButton_Click;
            }
            UpdateContext(Text);
        }


        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            UpdateContext(Text);
        }

        private void UpdateContext(string value)
        {
            if (double.TryParse(value, out double res))
            {
                double number;
                if (StepStrictly)
                {
                    number = (Math.Max(Math.Min((int)(res / Step), (int)(Max / Step)), (int)(Min / Step) + ((Min % Step == 0) ? 0 : 1))) * Step;
                }
                else
                {
                    number = Math.Max(Math.Min(res, Max), Min);
                }
                SetCurrentValue(NumberProperty, number);
            }
            else if (string.IsNullOrEmpty(value))
            {
                SetCurrentValue(NumberProperty, null);
            }
            UpdateButtonStatus();
            SetCurrentValue(TextProperty, Number?.ToString("f" + Precision));
        }

        private void UpdateButtonStatus() {
            if (_increaseButton != null && _decreaseButton != null)
            {
                if (Number == null)
                {
                    _increaseButton.IsEnabled = _decreaseButton.IsEnabled = true;
                }
                else
                {
                    _decreaseButton.IsEnabled = Number - Min >= Step;
                    _increaseButton.IsEnabled = Number <= Max - Step;
                    SelectionStart = (Number?.ToString("f" + Precision)).Length;
                }
            }
        }

        private void _increaseButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateContext(((Number ?? Math.Min(0, Max - Step)) + Step).ToString());
        }

        private void _decreaseButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateContext(((Number ?? Math.Max(0, Min + Step)) - Step).ToString());
        }

        #region 设置计数器允许的最小值



        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(double), typeof(InputNumber), new PropertyMetadata(double.MinValue,OnSectionChanged));

        private static void OnSectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InputNumber number)
            {
                number.UpdateButtonStatus();
            };
        }
        #endregion

        #region  设置计数器允许的最大值
        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(double), typeof(InputNumber), new PropertyMetadata(double.MaxValue, OnSectionChanged));
        #endregion

        #region 计数器步长
        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(InputNumber), new PropertyMetadata(1.0),StepPropertyValidate);

        private static bool StepPropertyValidate(object value)
        {
            return double.TryParse(value?.ToString(), out double res) && res != 0;
        }
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

        #region 数值精度 --  precision 的值必须是一个非负且小于100的整数


        public uint Precision
        {
            get { return (uint)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Precision.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.Register("Precision", typeof(uint), typeof(InputNumber), new PropertyMetadata(default),PrecisionPropertyValidate);

        private static bool PrecisionPropertyValidate(object value)
        {
            return uint.TryParse(value?.ToString(), out uint res) && res <= 99;
        }


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
            DependencyProperty.Register("ControlsPosition", typeof(EnumPosition), typeof(InputNumber), new PropertyMetadata(EnumPosition.Strech));


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
            DependencyProperty.Register("Number", typeof(double?), typeof(InputNumber), new PropertyMetadata(null, NumberPropertyChanged));

        private static void NumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InputNumber number)
            {
                number.UpdateContext(e.NewValue?.ToString());
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
}
