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
    /// Interaction logic for JoinLobbyView.xaml
    /// </summary>
    public partial class JoinLobbyView : UserControl
    {
        private AppManager manager;
        public JoinLobbyView(AppManager appManager)
        {
            manager = appManager;
            InitializeComponent();
            lobbyurl.GotFocus += Lobbyurl_GotFocus;
            lobbyurl.LostFocus += Lobbyurl_LostFocus;
            joinButton.Click += JoinButton_Click;
            
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            StatusLabel.Content = "Trying to join Lobby";
            var result = manager.JoinLobby(lobbyurl.Text);
            lobbyurl.Text = "Lobby Url";
            lobbyurl.FontSize = 22;
            setStatus(result);
        }

        public void setStatus(LobbyJoinState state)
        {
            Dispatcher.Invoke(() =>
            {
                switch (state)
                {
                    case LobbyJoinState.INVALID_URL:
                        StatusLabel.Content = "Not a valid url";
                        StatusLabel.Foreground = Brushes.Red;
                        break;
                    case LobbyJoinState.LOBBY_NOT_FOUND:
                        StatusLabel.Content = "Couldn't find Lobby";
                        StatusLabel.Foreground = Brushes.Red;
                        break;
                    case LobbyJoinState.GENERIC_ERROR:
                        StatusLabel.Content = "Failed to send Request.";
                        StatusLabel.Foreground = Brushes.Red;
                        break;
                    case LobbyJoinState.REQUEST_SENT:
                        StatusLabel.Content = "Join Request sucessfully sent";
                        StatusLabel.Foreground = Brushes.Green;
                        break;
                    case LobbyJoinState.LOBBY_CLOSED:
                        StatusLabel.Content = "Lobby closed.";
                        StatusLabel.Foreground = Brushes.Red;
                        break;
                    case LobbyJoinState.LOBBY_FULL:
                        StatusLabel.Content = "Lobby full";
                        StatusLabel.Foreground = Brushes.Red;
                        break;
                }
            });
        }

        private void Lobbyurl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lobbyurl.Text != "") return;
            lobbyurl.Text = "Lobby Url";
            lobbyurl.FontSize = 22;
        }

        private void Lobbyurl_GotFocus(object sender, RoutedEventArgs e)
        {
            lobbyurl.Text = "";
            lobbyurl.FontSize = 15;
        }



        public void setLeagueStatus(bool running)
        {
            Dispatcher.Invoke(() =>
            {
                ClientStatusLabel.Content = running ? "League Running" : "League closed";
                ClientStatusLabel.Foreground = running ? Brushes.Green : Brushes.Red;
            });
        }

        public void SetServerStatus(bool connected)
        {
            Dispatcher.Invoke(() =>
            {
                ServerStatusLabel.Content = connected ? "Connected to Server" : "Disconnected from Server";
                ServerStatusLabel.Foreground = connected ? Brushes.Green : Brushes.Red;
            });
        }
    }
}
