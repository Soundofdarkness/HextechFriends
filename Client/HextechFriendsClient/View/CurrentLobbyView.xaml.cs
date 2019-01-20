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
    /// Interaction logic for CurrentLobbyView.xaml
    /// </summary>
    public partial class CurrentLobbyView : UserControl
    {
        private AppManager manager;
        public CurrentLobbyView(AppManager appManager)
        {
            manager = appManager;
            InitializeComponent();
        }


        public void SetLobbyJoin(int ownerIconId, string ownerSummonerName)
        {
            manager.leagueClient.LobbyOwner = false;
            Dispatcher.Invoke(() =>
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(Constants.GetIconUrl(ownerIconId));
                img.EndInit();
                OwnerImage.Source = img;
                LobbyURL.Text = "";

                LobbyOwner.Content = ownerSummonerName;
            });
        }

        public void setLobbyCreate(int ownerIconId, string ownerSummonerName, string url)
        {
            manager.leagueClient.LobbyOwner = true;
            Dispatcher.Invoke(() =>
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(Constants.GetIconUrl(ownerIconId));
                img.EndInit();
                OwnerImage.Source = img;

                LobbyOwner.Content = ownerSummonerName;
                LobbyURL.Text = "hextech://" + url;
            });
        }
    }
}
