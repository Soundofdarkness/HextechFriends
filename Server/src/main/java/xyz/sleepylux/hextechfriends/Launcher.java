package xyz.sleepylux.hextechfriends;

import xyz.sleepylux.hextechfriends.server.HextechServer;

public class Launcher {
    public static void main(String[] args){
        HextechServer server = new HextechServer(9090);
        server.start();
    }
}
