using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WebSocketSharp;

namespace HextechFriendsClient.Protocol
{
    public class HextechSocket
    {
        public AppManager appManager;
        public WebSocket webSocket;
        public HextechSocket(AppManager appManager)
        {
            this.appManager = appManager;
            webSocket = new WebSocket(Constants.SERVER_URL);
            webSocket.OnError += WebSocket_OnError;
            webSocket.OnOpen += WebSocket_OnOpen;
            webSocket.OnMessage += WebSocket_OnMessage;
            webSocket.OnClose += WebSocket_OnClose;
            Task.Run(() => { webSocket.Connect(); });
        }

        private void WebSocket_OnClose(object sender, CloseEventArgs e)
        {
            appManager.mainWindow.setServerStatus(false);
        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            
        }

        private void WebSocket_OnOpen(object sender, EventArgs e)
        {
            appManager.mainWindow.setServerStatus(true);
        }

        private void WebSocket_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}
