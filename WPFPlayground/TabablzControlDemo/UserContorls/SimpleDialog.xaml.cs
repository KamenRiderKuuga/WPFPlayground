using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using TabablzControlDemo.Models;

namespace TabablzControlDemo.UserContorls
{
    /// <summary>
    /// SimpleDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleDialog : UserControl
    {

        private readonly object _dialogIdentifier;
        private DialogMessage _dialogMessage;

        public SimpleDialog()
        {
            InitializeComponent();
        }

        public SimpleDialog(object dialogIdentifier, string message)
        {
            InitializeComponent();
            _dialogIdentifier = dialogIdentifier;
            _dialogMessage = new DialogMessage();
            _dialogMessage.Message = message;
            DataContext = _dialogMessage;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.Close(_dialogIdentifier);
        }
    }
}
