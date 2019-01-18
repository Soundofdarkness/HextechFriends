package xyz.sleepylux.hextechfriends.models.DTO.client;

public class RegisterLobbyTS {
    private String summonerName;
    private int level;
    private int iconId;
    private String ownerId;
    private String opCode;
    private String platform;

    public String getSummonerName(){ return this.summonerName; }
    public int getLevel(){ return this.level; }
    public int getIconId(){ return this.iconId; }
    public String getOwnerId(){ return this.ownerId; }
    public String getPlatform() { return this.platform; }
}
