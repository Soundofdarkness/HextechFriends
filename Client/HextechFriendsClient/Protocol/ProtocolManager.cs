using HextechFriendsClient.League.Protocol;
using HextechFriendsClient.Protocol.Server;
using HextechFriendsClient.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HextechFriendsClient.Protocol
{
    public class ProtocolManager
    {
        private HextechSocket hextechSocket;
        public ProtocolManager(HextechSocket socket)
        {
            hextechSocket = socket;
        }

        public void HandleMessage(string message)
        {
            Task.Run(() =>
            {
                handleMessageInternal(message);
            });
        }

        private void handleMessageInternal(string message)
        {
            var jObject = JObject.Parse(message);
            var opcode = jObject.GetValue("opCode").Value<string>();
            switch (opcode)
            {
                case "closeLobbyTC":
                    handleCloseLobbyTC();
                    break;
                case "joinAcceptedTC":
                    HandleJoinAcceptedTC(message);
                    break;
                case "joinRejectedLobbyFull":
                    HandleJoinRejectedLobbyFull();
                    break;
                case "joinRejectedTC":
                    HandleJoinRejectedTC();
                    break;
                case "leaveLobbyTC":
                    //Nothing to do i think.
                    break;
                case "registerLobbyTC":
                    HandleRegisterLobbyTC(message);
                    break;
                case "requestJoinTC":
                    HandleRequestJoinTC(message);
                    break;
            }
        }


        private void handleCloseLobbyTC()
        {
            hextechSocket.appManager.ViewModel.ChangeView(View.ViewState.JOIN_LOBBY);
            JoinLobbyView view = hextechSocket.appManager.ViewModel.GetView<JoinLobbyView>();
            view.setStatus(LobbyJoinState.LOBBY_CLOSED);
        }

        private void HandleJoinAcceptedTC(string message)
        {
            hextechSocket.appManager.joinedLobby = true;
            var dto = JsonConvert.DeserializeObject<JoinAcceptedTC>(message);
            hextechSocket.appManager.ViewModel.ChangeView(ViewState.CURRENT_LOBBY);
            CurrentLobbyView view = hextechSocket.appManager.ViewModel.GetView<CurrentLobbyView>();
            view.SetLobbyJoin(dto.ownerIconId, dto.ownerSummonerName);
            hextechSocket.appManager.notifyManager.DisplayNotification();
            /* Task.Run(() =>
            {
                var invitesJson = hextechSocket.appManager.leagueClient.GetFromAPI("lol-lobby/v2/received-invitations");
                var invites = JsonConvert.DeserializeObject<List<CurrentInvites>>(invitesJson);
                string inviteId = null;
                foreach (var invite in invites)
                {
                    if (invite.fromSummonerName == dto.ownerSummonerName)
                    {
                        inviteId = invite.invitationId;
                        break;
                    }
                }
                if (inviteId == null) return;
                var resp = hextechSocket.appManager.leagueClient.SendApiRequest("POST", $"lol-lobby/v2/received-invitations/{inviteId}/accept", null);
                if (resp.StatusCode == System.Net.HttpStatusCode.NoContent) return;
                MessageBox.Show(null, "Failed to accept invite. Please do so manually.", "League Client error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            */
        }

        private void HandleJoinRejectedLobbyFull()
        {
            hextechSocket.appManager.ViewModel.ChangeView(ViewState.JOIN_LOBBY);
            JoinLobbyView view = hextechSocket.appManager.ViewModel.GetView<JoinLobbyView>();
            view.setStatus(LobbyJoinState.LOBBY_FULL);
        }

        private void HandleJoinRejectedTC()
        {
            hextechSocket.appManager.ViewModel.ChangeView(ViewState.JOIN_LOBBY);
            JoinLobbyView view = hextechSocket.appManager.ViewModel.GetView<JoinLobbyView>();
            view.setStatus(LobbyJoinState.GENERIC_ERROR);
        }

        private void HandleRegisterLobbyTC(string message)
        {
            var dto = JsonConvert.DeserializeObject<RegisterLobbyTC>(message);
            hextechSocket.appManager.ViewModel.ChangeView(ViewState.CURRENT_LOBBY);
            CurrentLobbyView view = hextechSocket.appManager.ViewModel.GetView<CurrentLobbyView>();
            CurrentSummoner summoner = null;
            Task.Run(() =>
            {
                var data = hextechSocket.appManager.leagueClient.GetFromAPI("lol-summoner/v1/current-summoner");
                summoner = JsonConvert.DeserializeObject<CurrentSummoner>(data);
                if (summoner == null) Console.WriteLine("Critical Error summoner is null");
                view.setLobbyCreate(summoner.profileIconId, summoner.displayName, dto.uuid);
            });
        }

        private void HandleRequestJoinTC(string message)
        {
            var dto = JsonConvert.DeserializeObject<RequestJoinTC>(message);
            hextechSocket.appManager.requestQueue.AddToQueue(dto);
        }
    }
}
