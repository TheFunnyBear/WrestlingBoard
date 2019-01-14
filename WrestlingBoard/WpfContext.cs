using WB.SignalR.Base;
using WB.SignalR.Model;
using WB.SignalR.ViewModel;

namespace WrestlingBoard
{
    public class WpfContext : IContext
    {
        public void SendConnectionEvent(MainViewModel mainViewModel, bool connected)
        {
            Execute.OnUIThread(() => SetVMConnectionModel(mainViewModel, connected));
        }

        public void RecieveMessageEvent(MainViewModel mainViewModel, MyMessage myMessage)
        {
            Execute.OnUIThread(() => SetVMMessageModel(mainViewModel, myMessage));
        }

        private void SetVMMessageModel(MainViewModel mainViewModel, MyMessage myMessage)
        {
            mainViewModel.MyMessages.Add(myMessage);
        }

        private void SetVMConnectionModel(MainViewModel mainViewModel, bool connected)
        {
            if (mainViewModel.ConnectionActive != connected)
            {
                mainViewModel.ConnectionActive = connected;
            }
        }
    }
}
