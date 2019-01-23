using HextechFriendsClient.Protocol.Client;
using HextechFriendsClient.Protocol.Server;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HextechFriendsClient.View
{
    /// <summary>
    /// Interaction logic for AcceptUserView.xaml
    /// </summary>
    public partial class AcceptUserView : UserControl
    {
        private AppManager manager;
        private RequestJoinTC currentJoinRequest;
        public AcceptUserView(AppManager manager)
        {
            this.manager = manager;
            InitializeComponent();

            Accept.Click += Accept_Click;
            Decline.Click += Decline_Click;
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                var dto = new JoinRejectedTS();
                dto.uuid = currentJoinRequest.uuid;
                manager.hextechSocket.webSocket.Send(JsonConvert.SerializeObject(dto));
            });
            manager.ViewModel.ChangeView(ViewState.CURRENT_LOBBY);
            manager.requestQueue.RunQueue(true);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                var dto = new JoinAcceptedTS();
                dto.uuid = currentJoinRequest.uuid;
                manager.leagueClient.InviteUser(currentJoinRequest.summonerName, currentJoinRequest.summonerId);
                manager.hextechSocket.webSocket.Send(JsonConvert.SerializeObject(dto));
            });
            manager.ViewModel.ChangeView(ViewState.CURRENT_LOBBY);
            manager.requestQueue.RunQueue(true);
        }

        public void AcceptUser(RequestJoinTC dto)
        {
            currentJoinRequest = dto;
            manager.notifyManager.DisplayNotification();
            Dispatcher.Invoke(() =>
            { 
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(Constants.GetIconUrl(dto.iconId));
                img.EndInit();
                icon.Source = img;
                summonerName.Content = dto.summonerName;
            });
        }
    }
}
