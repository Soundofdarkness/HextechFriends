using HextechFriendsClient.League;
using HextechFriendsClient.Protocol;
using HextechFriendsClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WebSocketSharp;

namespace HextechFriendsClient
{
    public class AppManager
    {
        public HextechSocket hextechSocket;
        public LeagueClient leagueClient;
        public readonly MainWindow mainWindow;
        public readonly RequestQueue requestQueue;
        public ViewModel ViewModel;
        private LobbyStatus lobbyStatus = new LobbyStatus();

        public AppManager(MainWindow window)
        {
            mainWindow = window;
            ViewModel = new ViewModel(mainWindow, this);
            requestQueue = new RequestQueue(this);
            hextechSocket = new HextechSocket(this);
            leagueClient = new LeagueClient(this);
            ViewModel.EndInit();
        }

        public LobbyJoinState JoinLobby(string url)
        {
            if (!url.Contains("hextech://")) return LobbyJoinState.INVALID_URL;
            if (leagueClient.leagueSocket.ReadyState != WebSocketState.Open) return LobbyJoinState.GENERIC_ERROR;
            if (hextechSocket.webSocket.ReadyState != WebSocketState.Open) return LobbyJoinState.GENERIC_ERROR;
            hextechSocket.RequestJoin(url);
            return LobbyJoinState.REQUEST_SENT;
        }
    }
}
