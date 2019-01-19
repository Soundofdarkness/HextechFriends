using HextechFriendsClient.Protocol.Client;
using HextechFriendsClient.Protocol.Server;
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
                dto.uuid = currentJoinRequest.summonerName;
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
                manager.hextechSocket.webSocket.Send(JsonConvert.SerializeObject(dto));
            });
            manager.ViewModel.ChangeView(ViewState.CURRENT_LOBBY);
            manager.requestQueue.RunQueue(true);
        }

        public void AcceptUser(RequestJoinTC dto)
        {
            Dispatcher.Invoke(() =>
            {
                currentJoinRequest = dto;
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
