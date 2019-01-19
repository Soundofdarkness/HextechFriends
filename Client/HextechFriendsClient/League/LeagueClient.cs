using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Security.Authentication;
using HextechFriendsClient.View;
using System.IO;
using Newtonsoft.Json.Linq;

namespace HextechFriendsClient.League
{
    public class LeagueClient
    {
        public ClientManager clientManager;
        public WebSocket leagueSocket;
        public AppManager AppManager;
        public LeagueSocketManager Leaguemanager;
        public bool Connected = false;

        public LeagueClient(AppManager manager)
        {
            AppManager = manager;
            clientManager = new ClientManager();
            Leaguemanager = new LeagueSocketManager(this);
            try
            {
                leagueSocket = new WebSocket($"wss://127.0.0.1:{clientManager.Port}/", "wamp");
                leagueSocket.SetCredentials("riot", clientManager.Password, true);
                leagueSocket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
                leagueSocket.SslConfiguration.ServerCertificateValidationCallback = (sender, cert, chain, ssl) => true;
                leagueSocket.Connect();
                leagueSocket.Send("[5, \"OnJsonApiEvent\"]");
                leagueSocket.OnMessage += LeagueSocket_OnMessage;
                leagueSocket.OnOpen += LeagueSocket_OnOpen;
                leagueSocket.OnClose += LeagueSocket_OnClose;
                Connected = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                JoinLobbyView view = AppManager.ViewModel.GetView<JoinLobbyView>();
                view.setLeagueStatus(false);
            }
        }

        private void LeagueSocket_OnClose(object sender, CloseEventArgs e)
        {
            JoinLobbyView view = AppManager.ViewModel.GetView<JoinLobbyView>();
            view.setLeagueStatus(false);
        }

        private void LeagueSocket_OnOpen(object sender, EventArgs e)
        {
            Connected = true;
            JoinLobbyView view = AppManager.ViewModel.GetView<JoinLobbyView>();
            view.setLeagueStatus(true);
        }




        private void LeagueSocket_OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.IsText) return;
            dynamic data = JsonConvert.DeserializeObject(e.Data);
            if (data[0] != 8) { Console.WriteLine("Not 8"); return; }
            if (data[1].ToString() != "OnJsonApiEvent") { Console.WriteLine(data[1].ToString());  return; }
            if(data[2].uri.ToString() == null) { Console.WriteLine(data[2].uri.ToString()); return; }
            string uri = data[2].uri.ToString();
            switch (uri.Trim())
            {
                case "/lol-lobby/v2/lobby":
                    if (data[2]["eventType"].ToString() == "Create")
                    {
                        string lobbyinfo = data[2]["data"].ToString();
                        if (lobbyinfo.Trim() == String.Empty) return;
                        Leaguemanager.HandleLobbyCreate(lobbyinfo.Trim());
                    }
                    else if (data[2]["eventType"].ToString() == "Delete")
                    {
                        // Handle Lobby Close
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Sends Request to League Client API
        /// </summary>
        /// <param name="method">HTTP Method (GET, POST, PUT, HEAD)</param>
        /// <param name="endpoint">Endpoint without beginning /</param>
        /// <param name="data">Additional Request data</param>
        /// <returns><ref>HttpWebResponse</ref></returns>
        public HttpWebResponse SendApiRequest(string method, string endpoint, object data)
        {
            var jsondata = JsonConvert.SerializeObject(data);
            var bytes = Encoding.UTF8.GetBytes(jsondata);
            var url = $"https://127.0.0.1:{clientManager.Port}/{endpoint}";
            var request = HttpWebRequest.CreateHttp(url);
            request.Headers.Set(HttpRequestHeader.Authorization, $"Basic {clientManager.AuthorizationToken}");
            request.ContentType = "application/json";
            request.Method = method;
            if (method == "POST" || method == "PUT")
            {
                request.ContentLength = (long)bytes.Length;
                request.GetRequestStream().Write(bytes, 0, bytes.Length);
            }
            return (HttpWebResponse)request.GetResponse();
        }

        public string GetFromAPI(string endpoint)
        {
            var resp = SendApiRequest("GET", endpoint, null);
            if (resp.StatusCode != HttpStatusCode.OK) return null;
            var stream = new StreamReader(resp.GetResponseStream());
            var data = stream.ReadToEnd();
            resp.Close();
            return data;
        }
    }
}
