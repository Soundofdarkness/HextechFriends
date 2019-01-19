package xyz.sleepylux.hextechfriends.server;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.java_websocket.WebSocket;
import org.java_websocket.handshake.ClientHandshake;
import org.java_websocket.server.WebSocketServer;
import xyz.sleepylux.hextechfriends.handlers.MessageHandler;

import java.net.InetSocketAddress;
import java.util.UUID;

public class HextechServer extends WebSocketServer {
    private Logger logger = LogManager.getLogger(HextechServer.class);
    public MessageHandler messageHandler = new MessageHandler(this);

    public HextechServer(int port){
        super(new InetSocketAddress(port));
    }

    public HextechServer(InetSocketAddress address){
        super(address);
    }

    @Override
    public void onOpen(WebSocket conn, ClientHandshake handshake) {
        UUID uuid = UUID.randomUUID();
        conn.setAttachment(uuid.toString());
        // userHandler.RegisterSocket(uuid.toString(), conn);
        logger.debug("New Websocket connection: " + uuid.toString());
    }

    @Override
    public void onClose(WebSocket conn, int code, String reason, boolean remote) {
        // userHandler.RemoveUser(conn.getAttachment());
        messageHandler.leaveLobbyDC(conn);
        logger.debug(String.format("Removed User %s from database", conn.getAttachment().toString()));
    }

    @Override
    public void onMessage(WebSocket conn, String message) {
        logger.debug(conn.getAttachment() + " " + message);
        messageHandler.handleMessage(conn, message);
    }

    @Override
    public void onError(WebSocket conn, Exception ex) {
        ex.printStackTrace();
    }

    @Override
    public void onStart() {

    }
}
