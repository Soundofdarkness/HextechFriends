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
        public ViewModel ViewModel;
        private LobbyStatus lobbyStatus = new LobbyStatus();

        public AppManager(MainWindow window)
        {
            mainWindow = window;
            hextechSocket = new HextechSocket(this);
            leagueClient = new LeagueClient(this);
            ViewModel = new ViewModel(this.mainWindow);
        }
    }
}
