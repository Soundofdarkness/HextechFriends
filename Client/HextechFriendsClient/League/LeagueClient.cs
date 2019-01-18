using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HextechFriendsClient.League
{
    public class LeagueClient
    {
        public ClientManager clientManager;
        public WebSocket leagueSocket;
        public AppManager AppManager;

        public LeagueClient(AppManager manager)
        {
            AppManager = manager;
            clientManager = new ClientManager();
            leagueSocket = new WebSocket($"wss://riot:{clientManager.Password}@127.0.0.1:{clientManager.Port}", new[] { "wamp" });
            leagueSocket.SslConfiguration.ServerCertificateValidationCallback = (sender, cert, chain, ssl) => true; 
            Task.Run(() => { leagueSocket.Connect(); });
            leagueSocket.OnMessage += LeagueSocket_OnMessage;
        }

        private void updateMainWindow()
        {
            var running = clientManager.LeagueRunning();
            AppManager.mainWindow.setLeagueStatus(running);
        }

        private void LeagueSocket_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsText)
            {
                Console.WriteLine(e.Data);
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
    }
}
