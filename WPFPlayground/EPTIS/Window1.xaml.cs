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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EPTIS
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private DispatcherTimer _timer;
        DateTime _endTime = DateTime.Now;

        public Window1()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWin_Loaded);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            //设置定时器
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(10000000);   //时间间隔为一秒
            _timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// Timer触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            var timeSpan = _endTime - DateTime.Now;
            timeText.Text = timeSpan.Hours.ToString().PadLeft(2, '0') + ":" +
                timeSpan.Minutes.ToString().PadLeft(2, '0') + ":" +
                timeSpan.Seconds.ToString().PadLeft(2, '0');
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = new DateTime();
            var text = textTime.Text;
            text = text.Replace("：", ":").Replace("点", ":");
            bool timeIsRight = DateTime.TryParse(text, out dateTime);

            if (!timeIsRight)
            {
                return;
            }

            dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                    dateTime.Hour, dateTime.Minute, dateTime.Second);

            if (dateTime.Hour <= 11)
            {
                dateTime = dateTime.AddHours(12);
            }

            if (dateTime < DateTime.Now)
            {

            }

            _endTime = dateTime;
            _timer.Start();
            this.textTime.Visibility = Visibility.Hidden;
            this.buttonStart.Visibility = Visibility.Hidden;
        }
    }
}
