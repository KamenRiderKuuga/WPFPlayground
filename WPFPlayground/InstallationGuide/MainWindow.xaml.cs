using DynamicData;
using ReactiveUI;
using System.Reactive.Disposables;

namespace InstallationGuide
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : ReactiveWindow<AppViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new AppViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.BindCommand(ViewModel, viewModel => viewModel.SaveConfig, view => view.saveButton).DisposeWith(disposableRegistration);
                this.BindCommand(ViewModel, viewModel => viewModel.ChangeInputMethod, view => view.changeInputMethodButton).DisposeWith(disposableRegistration);
                this.Bind(ViewModel, x => x.InputModeButtonContent, view => view.changeInputMethodButton.Content, x => x, x => x.ToString()).DisposeWith(disposableRegistration);
                this.Bind(ViewModel, x => x.IpAddress, view => view.ipTextbox.Text).DisposeWith(disposableRegistration);
                this.OneWayBind(ViewModel, x => x.DBComboxVisibility, view => view.dataBaseCombox.Visibility).DisposeWith(disposableRegistration);
                this.OneWayBind(ViewModel, x => x.DBTextBoxVisibility, view => view.dataBaseTextBox.Visibility).DisposeWith(disposableRegistration);
                this.OneWayBind(ViewModel, x => x.IpList, x => x.ipListBox.ItemsSource).DisposeWith(disposableRegistration);
            });
        }
    }
}
