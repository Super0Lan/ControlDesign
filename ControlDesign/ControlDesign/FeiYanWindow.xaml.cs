using ControlDesign.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControlDesign
{
    /// <summary>
    /// FeiYanWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FeiYanWindow : Window
    {
        public Rootobject Data { get; set; }

        public FeiYanWindow()
        {
            InitializeComponent();
            Loaded += FeiYanWindow_Loaded;

            ListLevel.ItemsSource = new Dictionary<object, string>()
            {
                {new BrushConverter().ConvertFrom("#DE1F05"),"10000人及以上"},
                {new BrushConverter().ConvertFrom("#FF2736"),"1000-9999人"},
                {new BrushConverter().ConvertFrom("#FF6341"),"500-999人"},
                {new BrushConverter().ConvertFrom("#FFA577"),"100-499人"},
                {new BrushConverter().ConvertFrom("#FFCEA0"),"10-99人"},
                {new BrushConverter().ConvertFrom("#FFE7B2"),"1-9人"},
                {new BrushConverter().ConvertFrom("#E2EBF4"),"0人"},
            };
        }

        private void FeiYanWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8,zh-TW;q=0.7,zh-HK;q=0.5,en-US;q=0.3,en;q=0.2");
                client.DefaultRequestHeaders.Add("Host", "view.inews.qq.com");
                client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0");
                var res = client.GetAsync("https://view.inews.qq.com/g2/getOnsInfo?name=disease_h5").Result;
                if (res.IsSuccessStatusCode)
                {
                    Stream stream = res.Content.ReadAsStreamAsync().Result;
                    GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress);//解压缩
                    using (StreamReader reader = new StreamReader(gzip))//中文编码处理
                    {
                        string data = reader.ReadToEnd();
                        var responseData = JsonConvert.DeserializeObject<ResponseMsg>(data);
                        Data = JsonConvert.DeserializeObject<Rootobject>(responseData.Data);
                        DataContext = Data;
                        Properties.Settings.Default.DiseaseInfo = responseData.Data;
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }
    }

    public class NumberToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value?.ToString(), out int confirm)) {
                if (confirm < 1)
                {
                    return new BrushConverter().ConvertFrom("#E2EBF4");
                }
                else if (confirm < 10) {
                    return new BrushConverter().ConvertFrom("#FFE7B2");
                }
                else if (confirm < 100)
                {
                    return new BrushConverter().ConvertFrom("#FFCEA0");
                }
                else if (confirm < 500)
                {
                    return new BrushConverter().ConvertFrom("#FFA577");
                }
                else if (confirm < 1000)
                {
                    return new BrushConverter().ConvertFrom("#FF6341");
                }
                else if (confirm < 10000)
                {
                    return new BrushConverter().ConvertFrom("#FF2736");
                }
                else
                {
                    return new BrushConverter().ConvertFrom("#DE1F05");
                }
            }
            return new BrushConverter().ConvertFrom("#E2EBF4");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Point> points = new List<Point>();
            if (Application.Current.TryFindResource(value?.ToString()) is PathGeometry gemoty) {
                foreach (var item in gemoty.Figures)
                {
                    foreach (var i in item.Segments)
                    {
                        if (i.GetType().GetProperty("Points") != null) {
                            var point = i.GetType().GetProperty("Points").GetValue(i);
                            points.AddRange(((PointCollection)point).ToList());
                        }
                    }
                }
            }
            var centerPoint = GetCore2(points);
            return new Thickness(centerPoint.X - 20, centerPoint.Y -30, 0, 0);
        }

        public static Point GetCore2(List<Point> mPoints)
        {
            double area = 0.0f;//多边形面积  
            double Gx = 0.0f, Gy = 0.0f;// 重心的x、y  
            for (int i = 1; i <= mPoints.Count; i++)
            {
                double iLat = mPoints[(i % mPoints.Count())].X;
                double iLng = mPoints[(i % mPoints.Count())].Y;
                double nextLat = mPoints[(i - 1)].X;
                double nextLng = mPoints[(i - 1)].Y;
                double temp = (iLat * nextLng - iLng * nextLat) / 2.0f;
                area += temp;
                Gx += temp * (iLat + nextLat) / 3.0f;
                Gy += temp * (iLng + nextLng) / 3.0f;
            }
            Gx = Gx / area;
            Gy = Gy / area;
            return new Point(Gx, Gy);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
