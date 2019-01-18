using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.Protocol.Client
{
    class JoinRejectedTS
    {
        public string opCode = "joinRejectedTS";
        public string uuid { get; set; }
    }
}
