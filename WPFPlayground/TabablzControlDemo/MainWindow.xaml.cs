using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TabablzControlDemo.UserContorls;

namespace TabablzControlDemo
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button)?.Name)
            {
                case nameof(btnRegister):
                    if (!ValidateValue(out var message))
                    {
                        SimpleDialog simpleDialog = new SimpleDialog("RootDialog", message);
                        await DialogHost.Show(simpleDialog);
                    }
                    else
                    {
                        ProgressBar progressBar = new ProgressBar();
                        progressBar.Width = 200;
                        progressBar.IsIndeterminate = true;
                        await DialogHost.Show(progressBar, "RootDialog", DialogOpened_Event, DialogClosing_Event);
                    }
                    break;

                case nameof(btnClose):
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 验证界面控件的值
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateValue(out string message)
        {
            message = "";

            // 做一些对于值的验证

            return true;
        }

        private void DialogOpened_Event(object sender, DialogOpenedEventArgs eventArgs)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    eventArgs.Session.Close(false);
                });
            });
        }

        private void DialogClosing_Event(object sender, DialogClosingEventArgs eventArgs)
        {
            Task.Run(() =>
            {
                System.Threading.Thread.Sleep(300);

                Dispatcher.Invoke(() =>
                {
                    SimpleDialog simpleDialog = new SimpleDialog("RootDialog", "执行成功！");
                    DialogHost.Show(simpleDialog);
                });
            });
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
