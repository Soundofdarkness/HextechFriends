using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.Protocol.Client
{
    class RegisterLobbyTS
    {
        public string summonerName { get; set; }
        public int level { get; set; }
        public int iconId { get; set; }
        public string ownerId { get; set; }
        public string platform { get; set; }
        public string opCode = "registerLobbyTS";
    }
}
