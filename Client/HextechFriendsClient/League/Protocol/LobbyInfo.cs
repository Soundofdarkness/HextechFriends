using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.League.Protocol
{
    public class LobbyGameConfig
    {
        public List<int> allowablePremadeSizes { get; set; }
        public string customLobbyName { get; set; }
        public string customMutatorName { get; set; }
        public List<object> customRewardsDisabledReasons { get; set; }
        public string customSpectatorPolicy { get; set; }
        public List<object> customSpectators { get; set; }
        public List<object> customTeam100 { get; set; }
        public List<object> customTeam200 { get; set; }
        public string gameMode { get; set; }
        public bool isCustom { get; set; }
        public bool isLobbyFull { get; set; }
        public bool isTeamBuilderManaged { get; set; }
        public int mapId { get; set; }
        public int maxHumanPlayers { get; set; }
        public int maxLobbySize { get; set; }
        public int maxTeamSize { get; set; }
        public string pickType { get; set; }
        public bool premadeSizeAllowed { get; set; }
        public int queueId { get; set; }
        public bool showPositionSelector { get; set; }
    }

    public class Invitation
    {
        public string invitationId { get; set; }
        public string state { get; set; }
        public string timestamp { get; set; }
        public int toSummonerId { get; set; }
        public string toSummonerName { get; set; }
    }

    public class LocalMember
    {
        public bool allowedChangeActivity { get; set; }
        public bool allowedInviteOthers { get; set; }
        public bool allowedKickOthers { get; set; }
        public bool allowedStartActivity { get; set; }
        public bool allowedToggleInvite { get; set; }
        public bool autoFillEligible { get; set; }
        public bool autoFillProtectedForPromos { get; set; }
        public bool autoFillProtectedForSoloing { get; set; }
        public bool autoFillProtectedForStreaking { get; set; }
        public int botChampionId { get; set; }
        public string botDifficulty { get; set; }
        public string botId { get; set; }
        public string firstPositionPreference { get; set; }
        public bool isBot { get; set; }
        public bool isLeader { get; set; }
        public bool isSpectator { get; set; }
        public string lastSeasonHighestRank { get; set; }
        public string puuid { get; set; }
        public bool ready { get; set; }
        public string secondPositionPreference { get; set; }
        public bool showGhostedBanner { get; set; }
        public int summonerIconId { get; set; }
        public int summonerId { get; set; }
        public string summonerInternalName { get; set; }
        public int summonerLevel { get; set; }
        public string summonerName { get; set; }
        public int teamId { get; set; }
    }

    public class Member
    {
        public bool allowedChangeActivity { get; set; }
        public bool allowedInviteOthers { get; set; }
        public bool allowedKickOthers { get; set; }
        public bool allowedStartActivity { get; set; }
        public bool allowedToggleInvite { get; set; }
        public bool autoFillEligible { get; set; }
        public bool autoFillProtectedForPromos { get; set; }
        public bool autoFillProtectedForSoloing { get; set; }
        public bool autoFillProtectedForStreaking { get; set; }
        public int botChampionId { get; set; }
        public string botDifficulty { get; set; }
        public string botId { get; set; }
        public string firstPositionPreference { get; set; }
        public bool isBot { get; set; }
        public bool isLeader { get; set; }
        public bool isSpectator { get; set; }
        public string lastSeasonHighestRank { get; set; }
        public string puuid { get; set; }
        public bool ready { get; set; }
        public string secondPositionPreference { get; set; }
        public bool showGhostedBanner { get; set; }
        public int summonerIconId { get; set; }
        public int summonerId { get; set; }
        public string summonerInternalName { get; set; }
        public int summonerLevel { get; set; }
        public string summonerName { get; set; }
        public int teamId { get; set; }
    }

    public class LobbyInfo
    {
        public bool canStartActivity { get; set; }
        public string chatRoomId { get; set; }
        public string chatRoomKey { get; set; }
        public LobbyGameConfig gameConfig { get; set; }
        public List<Invitation> invitations { get; set; }
        public LocalMember localMember { get; set; }
        public List<Member> members { get; set; }
        public string partyId { get; set; }
        public string partyType { get; set; }
        public List<object> restrictions { get; set; }
    }
}
