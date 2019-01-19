using HextechFriendsClient.League.Protocol;
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
            var info = JsonConvert.DeserializeObject<LobbyInfo>(message);
            leagueClient.AppManager.ViewModel.ChangeView(View.ViewState.CREATE_LOBBY);
            CreateLobbyView view = leagueClient.AppManager.ViewModel.GetView<CreateLobbyView>();
            view.CreateLobby(info);
        }
    }
}
