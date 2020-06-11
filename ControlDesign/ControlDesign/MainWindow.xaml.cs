using System;
using System.Collections.Generic;
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

namespace ControlDesign
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new List<object>() {
                new { Name ="1",UserName = "1"},
                new { Name ="11",UserName = "11"},
                new { Name ="111",UserName = "111"},
                new { Name ="1111",UserName = "1111"},
                new { Name ="11111",UserName = "11111"},
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //tb.Focus();
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
