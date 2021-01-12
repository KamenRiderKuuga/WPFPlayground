using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CountdownTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        private DispatcherTimer _timerForShutDown = new DispatcherTimer();
        private DateTime _entTime;
        private int _leftSeconds = 120;

        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timerForShutDown.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Timer_Tick;
            _timerForShutDown.Tick += TimerForShutDown_Tick; ;
        }

        private void TimerForShutDown_Tick(object sender, EventArgs e)
        {
            this.textTime.Text = $"{_leftSeconds}秒后关机";

            if (--_leftSeconds < 0)
            {
                _timerForShutDown.Stop();
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var timeLeft = _entTime - DateTime.Now;

            if (timeLeft.TotalSeconds >= 0)
            {
                this.textTime.Text = $"{timeLeft.Hours.ToString().PadLeft(2, '0')}:" +
                                     $"{timeLeft.Minutes.ToString().PadLeft(2, '0')}:" +
                                     $"{timeLeft.Seconds.ToString().PadLeft(2, '0')}";
            }
            else
            {
                _timer.Stop();
                _timerForShutDown.Start();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isTime = DateTime.TryParse(textboxTime.Text.Replace("点", ":").Replace("：", ":"), out _entTime);

            if (!isTime)
            {
                return;
            }

            if (_entTime.Hour <= 11)
            {
                _entTime = _entTime.AddHours(12);
            }

            if (_entTime < DateTime.Now)
            {
                return;
            }

            _timer.Start();
            this.textboxTime.Visibility = Visibility.Hidden;
            this.buttonConfirm.Visibility = Visibility.Hidden;
            this.ShowInTaskbar = false;
            this.Topmost = true;
        }
    }
}
