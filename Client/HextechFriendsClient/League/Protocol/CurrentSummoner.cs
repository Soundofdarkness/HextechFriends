﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.League.Protocol
{
    public class CurrentSummoner
    {
        public Int64 accountId { get; set; }
        public string displayName { get; set; }
        public string internalName { get; set; }
        public string lastSeasonHighestRank { get; set; }
        public int percentCompleteForNextLevel { get; set; }
        public int profileIconId { get; set; }
        public string puuid { get; set; }
        public RerollPoints rerollPoints { get; set; }
        public Int64 summonerId { get; set; }
        public int summonerLevel { get; set; }
        public int xpSinceLastLevel { get; set; }
        public int xpUntilNextLevel { get; set; }
    }

    public class RerollPoints
    {
        public int currentPoints { get; set; }
        public int maxRolls { get; set; }
        public int numberOfRolls { get; set; }
        public int pointsCostToRoll { get; set; }
        public int pointsToReroll { get; set; }
    }
}
