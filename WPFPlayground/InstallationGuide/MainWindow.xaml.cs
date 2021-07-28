using DynamicData;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace InstallationGuide
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IViewFor<AppViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(AppViewModel), typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new AppViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.BindCommand(ViewModel, viewModel => viewModel.SaveConfig, view => view.saveButton).DisposeWith(disposableRegistration);
                this.BindCommand(ViewModel, viewModel => viewModel.ChangeInputMethod, view => view.changeInputMethodButton).DisposeWith(disposableRegistration);
                this.Bind(ViewModel, x => x.InputModeButtonContent, view => view.changeInputMethodButton.Content, x => x, x => x.ToString()).DisposeWith(disposableRegistration);
                this.Bind(ViewModel, x => x.IpAddress, view => view.ipTextbox.Text, x => x, x => x?.Replace("。", ".")).DisposeWith(disposableRegistration);
                this.OneWayBind(ViewModel, x => x.DBComboxVisibility, view => view.dataBaseCombox.Visibility).DisposeWith(disposableRegistration);
                this.OneWayBind(ViewModel, x => x.DBTextBoxVisibility, view => view.dataBaseTextBox.Visibility).DisposeWith(disposableRegistration);
                this.OneWayBind(ViewModel, x => x.IpList, x => x.ipListBox.ItemsSource).DisposeWith(disposableRegistration);
            });
        }

        public AppViewModel ViewModel
        {
            get => (AppViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AppViewModel)value;
        }
    }
}
