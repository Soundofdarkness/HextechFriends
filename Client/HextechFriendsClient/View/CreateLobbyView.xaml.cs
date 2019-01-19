﻿using HextechFriendsClient.League.Protocol;
using HextechFriendsClient.Protocol.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
