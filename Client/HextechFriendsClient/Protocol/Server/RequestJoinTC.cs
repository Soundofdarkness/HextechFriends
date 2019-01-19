using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.Protocol.Server
{
    public class RequestJoinTC
    {
        public string opCode { get; set; }
        public string uuid { get; set; }
        public string summonerName { get; set; }
        public string summonerId { get; set; }
        public int level { get; set; }
        public int iconId { get; set; }
    }
}
