using HextechFriendsClient.League.Protocol;
using HextechFriendsClient.Protocol.Client;
using HextechFriendsClient.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.League
{
    public class LeagueSocketManager
    {
        private LeagueClient leagueClient;
        public LeagueSocketManager(LeagueClient client)
        {
            leagueClient = client;
        }

        public void HandleLobbyCreate(string message)
        {
            // No need to display create lobby tab after joining one
            if (leagueClient.AppManager.joinedLobby) return;
            var info = JsonConvert.DeserializeObject<LobbyInfo>(message);
            leagueClient.AppManager.ViewModel.ChangeView(View.ViewState.CREATE_LOBBY);
            CreateLobbyView view = leagueClient.AppManager.ViewModel.GetView<CreateLobbyView>();
            view.CreateLobby(info);
        }

        public void HandleLobbyClosed()
        {
            leagueClient.AppManager.joinedLobby = false;
            if (leagueClient.LobbyOwner)
            {
                CloseLobbyTS closeLobbyTS = new CloseLobbyTS();
                leagueClient.AppManager.hextechSocket.webSocket.Send(JsonConvert.SerializeObject(closeLobbyTS));
            }
            else
            {
                LeaveLobbyTS leaveLobbyTS = new LeaveLobbyTS();
                leagueClient.AppManager.hextechSocket.webSocket.Send(JsonConvert.SerializeObject(leaveLobbyTS));
            }
            leagueClient.AppManager.ViewModel.ChangeView(ViewState.JOIN_LOBBY);
        }
    }
}
