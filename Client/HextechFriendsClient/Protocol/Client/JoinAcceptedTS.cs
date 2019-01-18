using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.Protocol.Client
{
    class JoinAcceptedTS
    {
        public string opCode = "joinAcceptedTS";
        public string uuid { get; set; }
    }
}
