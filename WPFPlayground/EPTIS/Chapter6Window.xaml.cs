using EPTIS.Chapter6;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EPTIS
{
    /// <summary>
    /// Chapter6Window.xaml 的交互逻辑
    /// </summary>
    public partial class Chapter6Window : Window
    {
        private Student _student;

        public Chapter6Window()
        {
            InitializeComponent();

            _student = new Student();

            Binding binding = new Binding();
            binding.Source = _student;
            binding.Path = new PropertyPath("Name");

            BindingOperations.SetBinding(textBoxName, TextBox.TextProperty, binding);
             
            // 继承于FrameworkElement的UI元素类型，比如TextBox还可以写成
            textBoxName.SetBinding(TextBox.TextProperty, binding);

            // 或者简写成
            textBoxName.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = _student = new Student()});
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _student.Name += "Name";
        }
    }
}
