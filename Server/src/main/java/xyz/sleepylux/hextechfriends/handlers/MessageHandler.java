package xyz.sleepylux.hextechfriends.handlers;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.java_websocket.WebSocket;
import xyz.sleepylux.hextechfriends.models.DTO.client.*;
import xyz.sleepylux.hextechfriends.server.HextechServer;

public class MessageHandler {

    private HextechServer _server;
    private LobbyManager _lobbyManager = new LobbyManager();
    private JsonParser _jsonParser;
    private Gson _gson;
    private Logger logger = LogManager.getLogger(MessageHandler.class);
    public MessageHandler(HextechServer server){
        _server = server;
        _jsonParser = new JsonParser();
        _gson = new Gson();
    }

    public void handleMessage(WebSocket socket, String message){
		System.out.println(message);
        String opcode = getOpcode(message);

        switch(opcode){
            case "registerLobbyTS":
                registerLobby(socket, message); break;
            case "requestJoinTS":
                requestJoin(socket, message); break;
            case "joinAcceptedTS":
                joinAccepted(socket, message); break;
            case "joinRejectedTS":
                joinRejected(socket, message); break;
            case "closeLobbyTS":
				closeLobby(socket, message); break;
            case "leaveLobbyTS":
                leaveLobby(socket, message); break;
			default:
				logger.warn("Unknown Opcode: " + message);
				break;
        }
    }


    private String getOpcode(String message){
        JsonElement jsonElement = _jsonParser.parse(message);
        return jsonElement.getAsJsonObject().get("opCode").getAsString();
    }

    private void registerLobby(WebSocket socket, String message){
        RegisterLobbyTS dto = _gson.fromJson(message, RegisterLobbyTS.class);
        _lobbyManager.createLobby(dto, socket);
    }

    private void requestJoin(WebSocket socket, String message){
        RequestJoinTS dto = _gson.fromJson(message, RequestJoinTS.class);
        _lobbyManager.requestJoin(dto, socket);
    }

    private void joinAccepted(WebSocket socket, String message){
        JoinAcceptedTS dto = _gson.fromJson(message, JoinAcceptedTS.class);
        _lobbyManager.acceptJoin(dto, socket);
    }

    private void joinRejected(WebSocket socket, String message){
        JoinRejectedTS dto = _gson.fromJson(message, JoinRejectedTS.class);
        _lobbyManager.rejectJoin(dto, socket);
    }

    private void leaveLobby(WebSocket socket, String message){
        LeaveLobbyTS dto = _gson.fromJson(message, LeaveLobbyTS.class);
        _lobbyManager.leaveLobby(dto, socket);
    }
    public void leaveLobbyDC(WebSocket socket){
        LeaveLobbyTS dto = new LeaveLobbyTS();
        _lobbyManager.closeLobby(new CloseLobbyTS(), socket);
        _lobbyManager.leaveLobby(dto, socket);
    }
    private void closeLobby(WebSocket socket, String message){
        CloseLobbyTS dto = _gson.fromJson(message, CloseLobbyTS.class);
        _lobbyManager.closeLobby(dto, socket);
    }
}
