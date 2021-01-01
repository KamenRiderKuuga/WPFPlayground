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
    public partial class Chapter3Window : Window
    {
        public Chapter3Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Human human = (Human)this.FindResource("human");
            MessageBox.Show(human.Child.Name);
        }
    }
}
