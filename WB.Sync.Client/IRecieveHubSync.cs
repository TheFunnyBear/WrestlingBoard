using WB.Sync.Client.Model;

namespace WB.Sync.Client
{
    public interface IRecieveHubSync
    {
        void Recieve_AddMessage(string name, string message);

        void Recieve_Heartbeat();

        void Recieve_SendHelloObject(HelloModel hello);

    }
}
