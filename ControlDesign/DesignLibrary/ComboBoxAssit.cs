using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesignLibrary
{
    public class ComboBoxAssit
    {


        public static bool GetIsEditable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEditableProperty);
        }

        public static void SetIsEditable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEditableProperty, value);
        }

        // Using a DependencyProperty as the backing store for Editable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.RegisterAttached("IsEditable", typeof(bool), typeof(ComboBoxAssit), new PropertyMetadata(false, IsEditablePropertyChanged));

        private static void IsEditablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ComboBox comboBox) {
                comboBox.ApplyTemplate();
                if (comboBox.GetType().GetProperty("EditableTextBoxSite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(comboBox) is TextBox textbox)
                {
                    if ((bool)e.NewValue)
                    {
                        textbox.PreviewMouseLeftButtonDown += Textbox_PreviewMouseLeftButtonDown;
                        comboBox.GotFocus += ComboBox_GotFocus;
                        comboBox.DropDownClosed += ComboBox_DropDownClosed;
                        comboBox.IsKeyboardFocusWithinChanged += ComboBox_IsKeyboardFocusWithinChanged;                    }
                    else
                    {
                        textbox.PreviewMouseLeftButtonDown -= Textbox_PreviewMouseLeftButtonDown; ;
                        comboBox.GotFocus -= ComboBox_GotFocus;
                        comboBox.DropDownClosed -= ComboBox_DropDownClosed;
                    }
                }

            }
        }

        private static void ComboBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if ((bool)e.NewValue) {
                comboBox.Focus();
            }
        }

        private static void Textbox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox) {
                textBox.Focusable = true;
            }
        }

        private static void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.GetType().GetProperty("EditableTextBoxSite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(comboBox) is TextBox textBox)
                {
                    textBox.Focusable = false;
                }
            }
        }

        private static void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                comboBox.SetCurrentValue(ComboBox.IsDropDownOpenProperty, true);
            }
        }
    }
}

