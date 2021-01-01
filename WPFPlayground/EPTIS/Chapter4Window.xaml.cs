using EPTIS.Chapter3;
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

namespace EPTIS
{
    /// <summary>
    /// Chapter3.xaml 的交互逻辑
    /// </summary>
    public partial class Chapter4Window : Window
    {
        public Chapter4Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            StackPanel stackPanel = this.Content as StackPanel;
            TextBox textBox = stackPanel.Children[0] as TextBox;

            switch (button.Name)
            {
                case nameof(buttonOK):
                    textBox.Text = button.Name;
                    break;

                case nameof(buttonShowResource):
                    string content = this.FindResource("myString") as string;
                    this.textBox1.Text = content;
                    break;

                default:
                    textBox.Text = "No Name!";
                    break;
            }
        }
    }
}
