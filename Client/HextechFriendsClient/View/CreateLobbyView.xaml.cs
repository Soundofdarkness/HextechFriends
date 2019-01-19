using HextechFriendsClient.League.Protocol;
using HextechFriendsClient.Protocol.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HextechFriendsClient.View
{
    /// <summary>
    /// Interaction logic for CreateLobbyView.xaml
    /// </summary>
    public partial class CreateLobbyView : UserControl
    {
        private AppManager manager;
        private LobbyInfo currentLobby;
        public CreateLobbyView(AppManager appManager)
        {
            manager = appManager;
            InitializeComponent();
            Create.Click += Create_Click;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var lobbyInfo = currentLobby;
            Task.Run(() =>
            {
                RegisterLobbyTS dto = new RegisterLobbyTS();
                dto.iconId = lobbyInfo.localMember.summonerIconId;
                dto.level = lobbyInfo.localMember.summonerLevel;
                dto.summonerName = lobbyInfo.localMember.summonerName;
                dto.platform = "EUW";
                dto.ownerId = lobbyInfo.localMember.summonerId.ToString();
                manager.hextechSocket.webSocket.Send(JsonConvert.SerializeObject(dto));
            });
        }

        public void CreateLobby(LobbyInfo lobbyInfo)
        {
            Dispatcher.Invoke(() =>
            {
                currentLobby = lobbyInfo;
            });
            
        }
    }
}
