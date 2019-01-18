using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.Protocol.Client
{
    class RequestJoinTS
    {
        public string opCode = "requestJoinTS";
        public int level { get; set; }
        public int iconId { get; set; }
        public string lobbyId { get; set; }
        public string summonerName { get; set; }
        public string platform { get; set; }
        public string summonerId { get; set; }
    }
}
