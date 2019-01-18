package xyz.sleepylux.hextechfriends.models;

import org.java_websocket.WebSocket;
import xyz.sleepylux.hextechfriends.models.DTO.client.RegisterLobbyTS;
import xyz.sleepylux.hextechfriends.models.DTO.client.RequestJoinTS;

public class User {

    public String getUuid() { return uuid; }

    public String getUsername() { return username; }

    public String getPlatform() { return platform; }

    public int getIconId() { return iconId; }

    public String getSummonerId(){ return summonerId; }

    public int getLevel() { return level; }

    public WebSocket getSocket(){
        return socket;
    }

    private String uuid;
    private String username;
    private String platform;
    private int level;
    private int iconId;
    private String summonerId;
    private WebSocket socket;

    public User(WebSocket webSocket, RegisterLobbyTS dto){
        this.uuid = webSocket.getAttachment();
        this.socket = webSocket;
        this.username = dto.getSummonerName();
        this.platform = dto.getPlatform();
        this.summonerId = dto.getOwnerId();
        this.iconId = dto.getIconId();
        this.level = dto.getLevel();
    }

    public User(WebSocket socket, RequestJoinTS dto){
        this.uuid = socket.getAttachment();
        this.socket = socket;
        this.username = dto.getSummonerName();
        this.platform = dto.getPlatform();
        this.summonerId = dto.getSummonerId();
        this.iconId = dto.getIconId();
        this.level = dto.getLevel();
    }


}
