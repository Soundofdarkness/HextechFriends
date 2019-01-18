package xyz.sleepylux.hextechfriends.models.DTO.client;

public class RequestJoinTS {
    private String opCode;
    private int level;
    private int iconId;
    private String lobbyId;
    private String summonerName;
    private String platform;
    private String summonerId;

    public int getIconId() { return iconId; }

    public int getLevel() { return level; }

    public String getLobbyId() { return lobbyId; }

    public String getSummonerName() { return summonerName; }

    public String getPlatform() { return platform; }

    public String getSummonerId() { return summonerId; }
}
