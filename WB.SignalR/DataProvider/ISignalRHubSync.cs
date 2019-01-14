using System;
using WB.SignalR.Model;

namespace WB.SignalR.DataProvider
{
    public interface ISignalRHubSync
    {
        event Action<bool> ConnectionEvent;

        event Action<MyMessage> RecieveMessageEvent;

        void Connect();

        void Disconnect();

        void Heartbeat();

        void AddMessage(MyMessage message);

        void SendHelloObject(MyMessage message);
    }
}
