using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HextechFriendsClient
{
    class Dialogs
    {
        public static void LeagueNotRunning()
        {
            MessageBox.Show("League is not running. Please start League before starting HextechFriends!", "League not found.", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}
