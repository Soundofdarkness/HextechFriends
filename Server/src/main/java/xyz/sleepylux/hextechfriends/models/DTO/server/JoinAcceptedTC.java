package xyz.sleepylux.hextechfriends.models.DTO.server;

public class JoinAcceptedTC {
    private String opCode = "joinAcceptedTC";
    private String ownerSummonerName;
    private int ownerIconId;

    public void setOwnerSummonerName(String ownerSummonerName) { this.ownerSummonerName = ownerSummonerName; }

    public void setOwnerIconId(int ownerIconId) { this.ownerIconId = ownerIconId; }
}
