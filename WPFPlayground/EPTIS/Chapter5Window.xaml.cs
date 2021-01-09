using EPTIS.Chapter5;
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
    /// Chapter5Window.xaml 的交互逻辑
    /// </summary>
    public partial class Chapter5Window : Window
    {
        public Chapter5Window()
        {
            InitializeComponent();

            List<Employee> employees = new List<Employee>()
            {
                new Employee(){Id = 1, Name = "Tim", Age = 30},
                new Employee(){Id = 2, Name = "Tom", Age = 26},
                new Employee(){Id = 3, Name = "Guo", Age = 26},
                new Employee(){Id = 4, Name = "Yan", Age = 25},
                new Employee(){Id = 5, Name = "Owen", Age = 30},
                new Employee(){Id = 6, Name = "Victor", Age = 30},
            };

            this.listBoxEmployee.DisplayMemberPath = "Name";
            this.listBoxEmployee.SelectedValuePath = "Id";
            this.listBoxEmployee.ItemsSource = employees;
        }

        private void buttonVictor_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DependencyObject level1 = VisualTreeHelper.GetParent(btn);
            DependencyObject level2 = VisualTreeHelper.GetParent(level1);
            DependencyObject level3 = VisualTreeHelper.GetParent(level2);
            MessageBox.Show(level3.GetType().ToString());
        }
    }
}
