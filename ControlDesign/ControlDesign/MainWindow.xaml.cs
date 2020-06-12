using DesignLibrary;
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
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = new Button()
            {
                Content = DateTime.Now.Second,
                Width = 100,
                Height = 20,
            };
            Assists.SetIsCircle(button,true);
            panel.Children.Add(button);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DesignLibrary.Assists.SetBadge(panel.Children[panel.Children.Count - 1],Guid.NewGuid().ToString());

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            panel.Children.RemoveAt(panel.Children.Count - 1);
        }
    }
}
