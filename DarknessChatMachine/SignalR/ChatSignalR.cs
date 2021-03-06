﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DarknessChatMachine.Signalr
{
    [HubName("Chat")]
    public class ChatSignalR : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendMessag(string message)
        {
            Clients.Others.talk(message);
        }
    }
}