package xyz.sleepylux.hextechfriends.models;

import com.google.gson.Gson;
import xyz.sleepylux.hextechfriends.models.DTO.server.CloseLobbyTC;
import xyz.sleepylux.hextechfriends.models.DTO.server.LeaveLobbyTC;

import javax.jws.soap.SOAPBinding;
import java.util.*;
import java.util.concurrent.atomic.AtomicBoolean;

public class Lobby {
    private String uuid;
    private User owner;
    private Map<String, User> summoners;
    private String platform;
    private Gson gson;

    public Lobby(User lobbyOwner){
        uuid = UUID.randomUUID().toString();
        owner = lobbyOwner;
        summoners = new HashMap<>();
        gson = new Gson();
    }

    public void addSummoner(User summoner){
        summoners.put(summoner.getUuid(), summoner);
    }

    public void removeSummoner(String uuid){
        summoners.remove(uuid);
    }

    public void closeLobby(){
        String json = gson.toJson(new CloseLobbyTC());
        broadcast(json);
    }

    public Collection<User> getSummoners(){ return summoners.values(); }

    private void broadcast(String message){
        this.owner.getSocket().send(message);
        this.summoners.values().forEach(k -> k.getSocket().send(message));
    }

    public String getUuid(){ return uuid; }
    public String getPlatform(){ return platform; }
    public int getAmountPlayers(){ return summoners.size() + 1; }
    public User getOwner(){ return this.owner; }
}
