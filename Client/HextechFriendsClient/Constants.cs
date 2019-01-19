using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient
{
    class Constants
    {
        public static string SERVER_URL = "ws://localhost:9090";

        public static String GetIconUrl(int iconId)
        {
            return $"https://cdn.communitydragon.org/latest/profile-icon/{iconId.ToString()}.jpg";
        }
    }

    public enum LobbyJoinState
    {
        REQUEST_SENT,
        INVALID_URL,
        LOBBY_NOT_FOUND,
        LOBBY_CLOSED,
        GENERIC_ERROR,
        LOBBY_FULL
    }
}
