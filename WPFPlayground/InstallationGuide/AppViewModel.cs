using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace InstallationGuide
{
    public class AppViewModel : ReactiveObject
    {
        private string _ipAddress;
        /// <summary>
        /// 数据库IP地址
        /// </summary>
        public string IpAddress
        {
            get => _ipAddress;
            set => this.RaiseAndSetIfChanged(ref _ipAddress, value);
        }

        /// <summary>
        /// 改变数据库名输入模式命令
        /// </summary>
        public ReactiveCommand<Unit, Unit> ChangeInputMethod { get; }

        /// <summary>
        /// 保存配置命令
        /// </summary>
        public ReactiveCommand<Window, Unit> SaveConfig { get; }

        private bool _inputByHand = false;
        /// <summary>
        /// 是否手动输入模式
        /// </summary>
        public bool InputByHand
        {
            get => _inputByHand;
            set => this.RaiseAndSetIfChanged(ref _inputByHand, value);
        }

        private readonly ObservableAsPropertyHelper<bool> _dbComboxVisibility;
        /// <summary>
        /// 数据库名Combox可见性
        /// </summary>
        public bool DBComboxVisibility => _dbComboxVisibility.Value;

        private readonly ObservableAsPropertyHelper<bool> _dbTextBoxVisibility;
        /// <summary>
        /// 数据库名TextBox可见性
        /// </summary>
        public bool DBTextBoxVisibility => _dbTextBoxVisibility.Value;

        private readonly ObservableAsPropertyHelper<string> _inputModeButtonContent;
        /// <summary>
        /// 切换数据库名输入模式按钮文本
        /// </summary>
        public string InputModeButtonContent => _inputModeButtonContent.Value;

        private SourceList<string> _ips = new SourceList<string>();

        private readonly ReadOnlyObservableCollection<string> _ipList;
        /// <summary>
        /// 可选的IP列表
        /// </summary>
        public ReadOnlyObservableCollection<string> IpList => _ipList;

        public AppViewModel()
        {
            _ips.Connect().Bind(out _ipList).Subscribe();

            _inputModeButtonContent = this.WhenAnyValue(x => x.InputByHand).Select(x => x ? "从列表选择" : "手动输入").ToProperty(this, x => x.InputModeButtonContent);
            _dbComboxVisibility = this.WhenAnyValue(x => x.InputByHand).Select(x => !x).ToProperty(this, x => x.DBComboxVisibility);
            _dbTextBoxVisibility = this.WhenAnyValue(x => x.InputByHand).Select(x => x).ToProperty(this, x => x.DBTextBoxVisibility);

            ChangeInputMethod = ReactiveCommand.Create(() =>
            {
                InputByHand = !_inputByHand;
            });

            SaveConfig = ReactiveCommand.Create<Window>(window => window.Close());

            AddIPAddress();
        }

        /// <summary>
        /// 构建IP地址列表
        /// </summary>
        private void AddIPAddress()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    _ips.Add(ipa.ToString());
            }
        }
    }
}
