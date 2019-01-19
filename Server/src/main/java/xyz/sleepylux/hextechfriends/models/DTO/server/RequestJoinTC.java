package xyz.sleepylux.hextechfriends.models.DTO.server;

public class RequestJoinTC {
    private String opCode = "requestJoinTC";
    private String uuid;
    private String summonerName;
    private String summonerId;
    private int level;
    private int iconId;

    public void setUuid(String uuid) { this.uuid = uuid; }

    public void setLevel(int level) { this.level = level; }

    public void setIconId(int iconId) { this.iconId = iconId; }

    public void setSummonerName(String summonerName){ this.summonerName = summonerName; }

    public void setSummonerId(String summonerId) { this.summonerId = summonerId; }
}
