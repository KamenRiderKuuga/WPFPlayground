using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;

namespace InstallationGuide
{
    public class AppViewModel : ReactiveObject
    {
        private string _ipAddress;
        public string IpAddress
        {
            get => _ipAddress;
            set => this.RaiseAndSetIfChanged(ref _ipAddress, value);
        }

        public ReactiveCommand<Unit, Unit> ChangeInputMethod { get; }
        public ReactiveCommand<MainWindow, Unit> SaveConfig { get; }

        private bool _inputByHand;
        public bool InputByHand
        {
            get => _inputByHand;
            set => this.RaiseAndSetIfChanged(ref _inputByHand, value);
        }

        private readonly ObservableAsPropertyHelper<string> _inputByHandButtonContent;
        public string InputByHandButtonContent => _inputByHandButtonContent.Value;

        private Action _closeWindows;
        public Action CloseWindows
        {
            get => _closeWindows;
            set => this.RaiseAndSetIfChanged(ref _closeWindows, value);
        }

        private SourceList<string> _ips = new SourceList<string>();

        private readonly ReadOnlyObservableCollection<string> _ipList;
        public ReadOnlyObservableCollection<string> IpList => _ipList;

        public AppViewModel()
        {
            _ips.Connect().Bind(out _ipList).Subscribe();

            _inputByHandButtonContent = this.WhenAnyValue(x => x.InputByHand).Select(x => x ? "啊啊" : "啊啊啊").ToProperty(this, x => x.InputByHandButtonContent);

            ChangeInputMethod = ReactiveCommand.Create(() =>
            {
                InputByHand = !_inputByHand;
            });

            SaveConfig = ReactiveCommand.Create<MainWindow>(window => window.Close());

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
