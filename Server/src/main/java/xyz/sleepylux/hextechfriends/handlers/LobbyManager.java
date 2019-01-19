package xyz.sleepylux.hextechfriends.handlers;

import com.google.gson.Gson;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.java_websocket.WebSocket;
import xyz.sleepylux.hextechfriends.models.DTO.client.*;
import xyz.sleepylux.hextechfriends.models.DTO.server.*;
import xyz.sleepylux.hextechfriends.models.Lobby;
import xyz.sleepylux.hextechfriends.models.User;

import java.util.HashMap;
import java.util.Map;

public class LobbyManager {
    private Logger logger = LogManager.getLogger(LobbyManager.class);
    private Map<String, Lobby> lobbies = new HashMap<>();
    private Map<String, String> userToLobby = new HashMap<>();
    private Map<String, User> pendingJoins = new HashMap<>();
    private Gson gson = new Gson();


    public void createLobby(RegisterLobbyTS dto, WebSocket socket){
        User owner = new User(socket, dto);
        Lobby lobby = new Lobby(owner);
        RegisterLobbyTC out = new RegisterLobbyTC();
        out.setUuid(lobby.getUuid());
        socket.send(gson.toJson(out));
        lobbies.put(lobby.getUuid(), lobby);
        userToLobby.put(socket.getAttachment(), lobby.getUuid());
        logger.info("Created new Lobby %s with owner %s", lobby.getUuid(), owner.getUsername());
    }

    public void requestJoin(RequestJoinTS dto, WebSocket socket){
        User summoner = new User(socket, dto);
        Lobby lobby = lobbies.get(dto.getLobbyId());
        // Todo: Maybe custom error ?
        if(lobby == null){
            logger.info("Got invalid Lobby ID: %1", dto.getLobbyId());
            JoinRejectedTC out = new JoinRejectedTC();
            socket.send(gson.toJson(out));
            return;
        }
        if(!lobby.getPlatform().equals(summoner.getPlatform())){
            logger.info("Summoner %1 on wrong platform. Summoner Platform: %2, Lobby Platform: %3",
                    summoner.getUsername(), summoner.getPlatform(), lobby.getPlatform());
            JoinRejectedTC out = new JoinRejectedTC();
            socket.send(gson.toJson(out));
            return;
        }
        if(lobby.getAmountPlayers() == 5){
            logger.info("Summoner %1 rejected: Lobby full.", summoner.getUsername());
            JoinRejectedLobbyFull full = new JoinRejectedLobbyFull();
            socket.send(gson.toJson(full));
            return;
        }
        RequestJoinTC out = new RequestJoinTC();
        out.setIconId(summoner.getIconId());
        out.setSummonerName(summoner.getUsername());
        out.setLevel(summoner.getLevel());
        out.setUuid(summoner.getUuid());
        lobby.getOwner().getSocket().send(gson.toJson(out));
        this.pendingJoins.put(summoner.getUuid(), summoner);
        this.userToLobby.put(summoner.getUuid(), lobby.getUuid());
        logger.info("Set status of Summoner %1 to pending for lobby %2", summoner.getUsername(),
                lobby.getUuid());
    }

    public void acceptJoin(JoinAcceptedTS dto, WebSocket socket){
        User summoner = pendingJoins.get(dto.getUuid());
        String lobbyId = userToLobby.get(summoner.getUuid());
        Lobby lobby = lobbies.get(lobbyId);
        //Todo: Find better solution than ignoring
        if(lobby == null){
            logger.info("Failed to get Lobby for Summoner %1", summoner.getUsername());
            return;
        }
        lobby.addSummoner(summoner);
        JoinAcceptedTC out = new JoinAcceptedTC();
        out.setOwnerSummonerName(lobby.getOwner().getUsername());
        out.setOwnerIconId(lobby.getOwner().getIconId());
        summoner.getSocket().send(gson.toJson(out));
        logger.info("Sucessfully added Summoner %1 to Lobby %2", summoner.getUsername(), lobby.getUuid());
        pendingJoins.remove(dto.getUuid());
    }

    // Rip, poor Summoner ... (Not sure if i should even make that possible .. )
    public void rejectJoin(JoinRejectedTS dto, WebSocket socket){
        JoinRejectedTC out = new JoinRejectedTC();
        User summoner = pendingJoins.get(dto.getUuid());
        summoner.getSocket().send(gson.toJson(out));
        logger.info("Summoner %1 got reject from Joining Lobby.");
        pendingJoins.remove(dto.getUuid());
        userToLobby.remove(dto.getUuid());
    }

    public void leaveLobby(LeaveLobbyTS dto, WebSocket socket){
        Lobby lobby = lobbies.get(userToLobby.get((String)socket.getAttachment()));
        if(lobby == null){
            logger.info("User %1 is not in Lobby. User doesn't need to leave.", (String)socket.getAttachment());
            return;
        }
        lobby.removeSummoner(socket.getAttachment());
        logger.info("User %1 left Lobby.", (String)socket.getAttachment());
        userToLobby.remove((String)socket.getAttachment());
    }

    public void closeLobby(CloseLobbyTS dto, WebSocket socket){
        Lobby lobby = lobbies.get(userToLobby.get((String)socket.getAttachment()));
        if(lobby == null){
            logger.info("User %1 is not in Lobby. Not closing any lobby.", (String)socket.getAttachment());
            return;
        }
        lobby.closeLobby();
        lobbies.remove(lobby.getUuid());
        for(User user: lobby.getSummoners()){
            userToLobby.remove(user.getUuid());
        }
        userToLobby.remove((String)socket.getAttachment());
        logger.info("Lobby %1 sucessfully closed.", lobby.getUuid());

    }

}
