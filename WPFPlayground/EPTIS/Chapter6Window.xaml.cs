using EPTIS.Chapter6;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Xml.Linq;
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
        private ObservableCollection<Person> _persons;
        private DataTable _dt;

        public Chapter6Window()
        {
            InitializeComponent();

            _student = new Student();
            _persons = new ObservableCollection<Person>()
            {
                {new Person {ID = 1,Age = 2 , Name = "人造人1号"} },
                {new Person {ID = 2,Age = 4 , Name = "人造人2号"} },
                {new Person {ID = 3,Age = 6 , Name = "人造人3号"} },
            };

            listBoxPersons.ItemsSource = _persons;

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

        private void LoadData()
        {
            _dt = new DataTable();
            _dt.Columns.Add("ID", typeof(int));
            _dt.Columns.Add("Name", typeof(string));
            _dt.Columns.Add("Age", typeof(int));
            _dt.Rows.Add(1, "第一个", 18);
            _dt.Rows.Add(2, "第二个", 23);
            _dt.Rows.Add(3, "第三个", 26);
            listViewPersons.ItemsSource = _dt.DefaultView;
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

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button)?.Name)
            {
                case nameof(btnLoadDataTable):
                    // 初始化DataTable数据
                    LoadData();
                    break;

                case nameof(btnLoadConfig):
                    LoadConfig();
                    break;

                case nameof(btnLoadLINQData):
                    LoadLINQData();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 将LINQ的结果绑定到ListView
        /// </summary>
        private void LoadLINQData()
        {
            XDocument xmlData = XDocument.Load($"{AppDomain.CurrentDomain.SetupInformation.ConfigurationFile}");
            listViewBindingLINQ.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = xmlData.Descendants("add")
                                .Select(element => new
                                {
                                    ConfigItem = element.Attribute("key").Value,
                                    ConfigContent = element.Attribute("value").Value
                                })
            });
        }

        /// <summary>
        /// 从文件中加载XML内容并绑定到控件
        /// </summary>
        private void LoadConfig()
        {
            XmlDataProvider xdp = new XmlDataProvider()
            {
                Source = new Uri($@"{AppDomain.CurrentDomain.SetupInformation.ConfigurationFile}"),
                XPath = @"/configuration/appSettings/add",
            };

            listViewConfigs.DataContext = xdp;
            listViewConfigs.SetBinding(ListView.ItemsSourceProperty, new Binding());
        }
    }
}
