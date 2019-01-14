using WB.Sync.Client.Model;

namespace WB.Sync.Client
{
    public interface ISendHubSync
    {
        void AddMessage(string name, string message);

        void Heartbeat();

        void SendHelloObject(HelloModel hello);
    }
}
