using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.League.Protocol
{
    public class CurrentInvites
    {
        public bool canAcceptInvitation { get; set; }
        public int fromSummonerId { get; set; }
        public string fromSummonerName { get; set; }
        public GameConfig gameConfig { get; set; }
        public string invitationId { get; set; }
        public List<object> restrictions { get; set; }
        public string state { get; set; }
        public string timestamp { get; set; }
    }

    public class GameConfig
    {
        public string gameMode { get; set; }
        public string inviteGameType { get; set; }
        public int mapId { get; set; }
        public int queueId { get; set; }
    }
}
