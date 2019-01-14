using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows;
using WB.SignalR.Base;
using WB.SignalR.Command;
using WB.SignalR.DataProvider;
using WB.SignalR.Model;

namespace WB.SignalR.ViewModel
{
    public class WrestlerControlViewModel : Observable
    {
        private bool _connectionActive;
        private string _wrestlerName;
        private string _account;
        private Visibility _firstWarningVisibility;
        private Visibility _secondWarningVisibility;
        private Visibility _thirdWarningVisibility;

        public DelegateCommand AddOnePointCommand { get; private set; }
        public DelegateCommand AddTwoPointCommand { get; private set; }
        public DelegateCommand AddFourPointCommand { get; private set; }
        public DelegateCommand AddWarningCommand { get; private set; }
        public DelegateCommand RemoveOnePointCommand { get; private set; }
        public DelegateCommand RemoveTwoPointCommand { get; private set; }
        public DelegateCommand RemoveFourPointCommand { get; private set; }
        public DelegateCommand RemoveWarningCommand { get; private set; }

        private void OnAddOnePointCommandExecute(object obj)
        {
        }

        private bool OnAddOnePointCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnAddTwoPointCommandExecute(object obj)
        {
        }

        private bool OnAddTwoPointCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnAddForPointCommandExecute(object obj)
        {
        }

        private bool OnAddFourPointCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnAddWarningCommandExecute(object obj)
        {
        }

        private bool OnAddWarningCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnRemoveOnePointCommandExecute(object obj)
        {
        }

        private bool OnRemoveOnePointCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnRemoveTwoPointCommandExecute(object obj)
        {
        }

        private bool OnRemoveTwoPointCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnRemoveForPointCommandExecute(object obj)
        {
        }

        private bool OnRemoveFourPointCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnRemoveWarningCommandExecute(object obj)
        {
        }

        private bool OnRemoveWarningCommandCanExecute(object obj)
        {
            return ConnectionActive;
        }

        public void Load()
        {
            // Nothing to do here unless we want to persist our messages 
        }

        public bool ConnectionActive
        {
            get { return _connectionActive; }
            set
            {
                SetProperty(ref _connectionActive, value);
                AddOnePointCommand.RaiseCanExecuteChanged();
                AddTwoPointCommand.RaiseCanExecuteChanged();
                AddFourPointCommand.RaiseCanExecuteChanged();
                RemoveOnePointCommand.RaiseCanExecuteChanged();
                RemoveTwoPointCommand.RaiseCanExecuteChanged();
                RemoveFourPointCommand.RaiseCanExecuteChanged();
            }
        }

        public string WrestlerName
        {
            get { return _wrestlerName; }
            set
            {
                SetProperty(ref _wrestlerName, value);
            }
        }

        public string Account
        {
            get { return _account; }
            set
            {
                SetProperty(ref _account, value);
            }
        }

        public Visibility FirstWarningVisibility
        {
            get { return _firstWarningVisibility; }
            set
            {
                SetProperty(ref _firstWarningVisibility, value);
            }
        }

        public Visibility SecondWarningVisibility
        {
            get { return _secondWarningVisibility; }
            set
            {
                SetProperty(ref _secondWarningVisibility, value);
            }
        }

        public Visibility ThirdWarningVisibility
        {
            get { return _thirdWarningVisibility; }
            set
            {
                SetProperty(ref _thirdWarningVisibility, value);
            }
        }
    }

    public class MainViewModel : Observable
    {
        private string _mollyText;
        private int _ageText;
        private string _messageText;
        private string _nameText;
        private bool _connectionActive;
        private readonly ISignalRHubSync _signalRHubSync;
        private readonly IContext _context;

        public DelegateCommand AddMessageCommand { get; private set; }
        public DelegateCommand SendHeartbeatCommand { get; private set; }
        public DelegateCommand SendObjectCommand { get; private set; }
        public DelegateCommand SendWrestlingInfoCommand { get; private set; }

        public DelegateCommand DisconnectSignalR { get; private set; }
        public DelegateCommand ConnectSignalR { get; private set; }

        public ObservableCollection<MyMessage> MyMessages { get; private set; }

        public MainViewModel(ISignalRHubSync signalRHubSync, IContext context)
        {
            _signalRHubSync = signalRHubSync;
            _context = context;
            MyMessages = new ObservableCollection<MyMessage>();
            AddMessageCommand = new DelegateCommand(OnAddMessageExecute, OnAddMessageCanExecute);
            SendHeartbeatCommand = new DelegateCommand(OnSendHeartbeatExecute, OnSendHeartbeatCanExecute);
            SendObjectCommand = new DelegateCommand(OnSendObjectExecute, OnSendObjectCanExecute);
            SendWrestlingInfoCommand = new DelegateCommand(OnSendWrestlingInfoExecute, OnSendWrestlingInfoCanExecute);

            DisconnectSignalR = new DelegateCommand(OnDisconnectSignalRExecute, OnDisconnectSignalRCanExecute);
            ConnectSignalR = new DelegateCommand(OnConnectSignalRExecute, OnConnectSignalRCanExecute);

            _signalRHubSync.ConnectionEvent += _signalRHubSync_ConnectionEvent;
            _signalRHubSync.RecieveMessageEvent += _signalRHubSync_RecieveMessageEvent;
        }

        void _signalRHubSync_RecieveMessageEvent(MyMessage obj)
        {
            _context.RecieveMessageEvent(this, obj);
        }

        void _signalRHubSync_ConnectionEvent(bool obj)
        {
            _context.SendConnectionEvent(this, obj);
        }


        private bool OnDisconnectSignalRCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnDisconnectSignalRExecute(object obj)
        {
            _signalRHubSync.Disconnect();
        }

        private bool OnConnectSignalRCanExecute(object obj)
        {
            return !ConnectionActive;
        }

        private void OnConnectSignalRExecute(object obj)
        {
            _signalRHubSync.Connect();
        }

        public void Load()
        {
            // Nothing to do here unless we want to persist our messages 
        }

        private bool OnSendObjectCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(MollyText) && AgeText > 0 && ConnectionActive;
        }

        private void OnSendObjectExecute(object obj)
        {
            //MyMessages.Add(new MyMessage { Name = AgeText.ToString(), Message = MollyText });
            _signalRHubSync.SendHelloObject(new MyMessage { Name = AgeText.ToString(), Message = MollyText });
            MollyText = "";
            AgeText = 0;
        }

        private bool OnSendHeartbeatCanExecute(object obj)
        {
            return ConnectionActive;
        }


        private void OnSendWrestlingInfoExecute(object obj)
        {
            //MyMessages.Add(new MyMessage { Name = AgeText.ToString(), Message = MollyText });

            var wrestlersFight = new WrestlersFight();

            string messageText = JsonConvert.SerializeObject(wrestlersFight);
            
            _signalRHubSync.AddMessage(new MyMessage { Name = "WrestlingInfo", Message = messageText });
        }

        private bool OnSendWrestlingInfoCanExecute(object obj)
        {
            return ConnectionActive;
        }

        private void OnSendHeartbeatExecute(object obj)
        {
            _signalRHubSync.Heartbeat();
            //MyMessages.Add(new MyMessage { Name = "Heartbeat Test", Message = "hhhh" });
        }

        private bool OnAddMessageCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(MessageText) && !string.IsNullOrWhiteSpace(NameText) && ConnectionActive;
        }

        private void OnAddMessageExecute(object obj)
        {
            //MyMessages.Add(new MyMessage { Name = NameText, Message = MessageText });
            _signalRHubSync.AddMessage(new MyMessage { Name = NameText, Message = MessageText });
            MessageText = "";
            NameText = "";
        }

        public string MollyText
        {
            get { return _mollyText; }
            set
            {
                SetProperty(ref _mollyText, value);
                SendObjectCommand.RaiseCanExecuteChanged();
            }
        }

        public int AgeText
        {
            get { return _ageText; }
            set
            {
                SetProperty(ref _ageText, value);
                SendObjectCommand.RaiseCanExecuteChanged();
            }
        }

        public string MessageText
        {
            get { return _messageText; }
            set
            {
                SetProperty(ref _messageText, value);
                AddMessageCommand.RaiseCanExecuteChanged();
            }
        }

        public string NameText
        {
            get { return _nameText; }
            set
            {
                SetProperty(ref _nameText, value);
                AddMessageCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ConnectionActive
        {
            get { return _connectionActive; }
            set
            {
                SetProperty(ref _connectionActive, value);
                DisconnectSignalR.RaiseCanExecuteChanged();
                ConnectSignalR.RaiseCanExecuteChanged();
                AddMessageCommand.RaiseCanExecuteChanged();
                SendObjectCommand.RaiseCanExecuteChanged();
                SendWrestlingInfoCommand.RaiseCanExecuteChanged();
                SendHeartbeatCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
