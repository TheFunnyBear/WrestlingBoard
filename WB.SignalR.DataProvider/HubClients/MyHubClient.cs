using Microsoft.AspNet.SignalR.Client;
using System;
using WB.SignalR.DataProvider.Logging;
using WB.SignalR.Model;
using WB.Sync.Client;
using WB.Sync.Client.Model;

namespace WB.SignalR.DataProvider.HubClients
{
    public class MyHubClient : BaseHubClient, ISendHubSync, IRecieveHubSync
    {
        public event Action<MyMessage> RecievedMessageEvent;

        public MyHubClient()
        {
            Init();
        }

        public new void Init()
        {
            HubConnectionUrl = "http://localhost:8089/";
            HubProxyName = "Hubsync";
            HubTraceLevel = TraceLevels.None;
            HubTraceWriter = Console.Out;

            base.Init();

            _myHubProxy.On<string, string>("AddMessage", Recieve_AddMessage);
            _myHubProxy.On("Heartbeat", Recieve_Heartbeat);
            _myHubProxy.On<HelloModel>("SendHelloObject", Recieve_SendHelloObject);

            StartHubInternal();
        }

        public override void StartHub()
        {
            _hubConnection.Dispose();
            Init();
        }

        public void Recieve_AddMessage(string name, string message)
        {
            if (RecievedMessageEvent != null) RecievedMessageEvent.Invoke(new MyMessage { Name = name, Message = message });
            HubClientEvents.Log.Informational("Received addMessage: " + name + ": " + message);
        }

        public void Recieve_Heartbeat()
        {
            if (RecievedMessageEvent != null) RecievedMessageEvent.Invoke(new MyMessage { Name = "Heartbeat", Message = "received" });
            HubClientEvents.Log.Informational("Received heartbeat ");
        }

        public void Recieve_SendHelloObject(HelloModel hello)
        {
            if (RecievedMessageEvent != null) RecievedMessageEvent.Invoke(new MyMessage { Name = hello.Age.ToString(), Message = hello.Molly });
            HubClientEvents.Log.Informational("Received sendHelloObject " + hello.Molly + ", " + hello.Age);
        }

        public void AddMessage(string name, string message)
        {
            _myHubProxy.Invoke("AddMessage", name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    HubClientEvents.Log.Error("There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            HubClientEvents.Log.Informational("Client Sending addMessage to server");
        }

        public void Heartbeat()
        {
            _myHubProxy.Invoke("Heartbeat").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    HubClientEvents.Log.Error("There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            HubClientEvents.Log.Informational("Client heartbeat sent to server");
        }

        public void SendHelloObject(HelloModel hello)
        {
            _myHubProxy.Invoke<HelloModel>("SendHelloObject", hello).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    HubClientEvents.Log.Error("There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            HubClientEvents.Log.Informational("Client sendHelloObject sent to server");
        }
    }
}
