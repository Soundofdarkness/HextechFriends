using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.Protocol.Server
{
    class JoinAcceptedTC
    {
        public string opCode { get; set; }
        public string ownerSummonerName { get; set; }
        public int ownerIconId { get; set; }
    }
}
