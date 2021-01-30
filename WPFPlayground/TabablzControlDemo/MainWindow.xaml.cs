using MaterialDesignThemes.Wpf;
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
