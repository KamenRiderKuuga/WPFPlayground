using EPTIS.Chapter6;
using System;
using System.Collections.Generic;
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
            textBoxName.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = _student = new Student() });

            textboxWithNotify.SourceUpdated += TextboxSourceUpdate_Event;
            textboxWithNotify.TargetUpdated += TextboxTargetUpdate_Event;
            textBoxBindingWithCode.SetBinding(TextBox.TextProperty, new Binding("Text.Length") { Source = textBoxBeBinding, Mode = BindingMode.OneWay });
            textBoxBeBinding.SetBinding(TextBox.TextProperty, new Binding("/[2]") { Source = new List<string>() { "Tim", "Tom", "Blog" }, Mode = BindingMode.OneWay });
        }

        /// <summary>
        /// 在Binding源通知绑定了源的属性更新之后触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextboxTargetUpdate_Event(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("绑定了Binding源的目标属性已被更新");
        }

        /// <summary>
        /// 在Binding源收到通知，对应值发生改变之后会触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextboxSourceUpdate_Event(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("Binding源被更新了");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _student.Name += "Name";
        }
    }
}
