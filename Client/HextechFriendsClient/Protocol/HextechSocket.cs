using HextechFriendsClient.League.Protocol;
using HextechFriendsClient.Protocol.Client;
using HextechFriendsClient.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public ProtocolManager protocolManager;
        public HextechSocket(AppManager appManager)
        {
            this.appManager = appManager;
            protocolManager = new ProtocolManager(this);
            webSocket = new WebSocket(Constants.SERVER_URL);
            webSocket.OnError += WebSocket_OnError;
            webSocket.OnOpen += WebSocket_OnOpen;
            webSocket.OnMessage += WebSocket_OnMessage;
            webSocket.OnClose += WebSocket_OnClose;
            Task.Run(() => { webSocket.Connect(); });
        }

        private void WebSocket_OnClose(object sender, CloseEventArgs e)
        {
            JoinLobbyView view = appManager.ViewModel.GetView<JoinLobbyView>();
            view.SetServerStatus(false);
        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.IsText) return;
            protocolManager.HandleMessage(e.Data);
        }

        private void WebSocket_OnOpen(object sender, EventArgs e)
        {
            JoinLobbyView view = appManager.ViewModel.GetView<JoinLobbyView>();
            view.SetServerStatus(true);
        }

        private void WebSocket_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e);
        }

        public void RequestJoin(string lobbyurl)
        {
            string lobbyid = lobbyurl.Replace("hextech://", "");
            RequestJoinTS dto = new RequestJoinTS();
            var data = appManager.leagueClient.GetFromAPI("lol-summoner/v1/current-summoner");
            var json = JsonConvert.DeserializeObject<CurrentSummoner>(data);
            dto.iconId = json.profileIconId;
            dto.level = json.summonerLevel;
            dto.platform = "EUW";
            dto.summonerId = json.summonerId.ToString();
            dto.lobbyId = lobbyid;
            dto.summonerName = json.displayName;
            webSocket.Send(JsonConvert.SerializeObject(dto));
        }
    }
}
