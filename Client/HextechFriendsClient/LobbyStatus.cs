using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient
{
    class LobbyStatus
    {
        public string LobbyType { get; set; }
        public string OwnerIcon { get; set; }
        public string OwnerName { get; set; }
        public int PlacesMax { get; set; }
        public int PlacesCurrent { get; set; }
        public bool Opened { get; set; }
    }
}
