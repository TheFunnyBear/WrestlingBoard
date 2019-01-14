using WB.SignalR.Model;
using WB.SignalR.ViewModel;

namespace WB.SignalR.Base
{
    public interface IContext
    {
        void SendConnectionEvent(MainViewModel mainViewModel, bool connected);
        void RecieveMessageEvent(MainViewModel mainViewModel, MyMessage myMessage);
    }
}
