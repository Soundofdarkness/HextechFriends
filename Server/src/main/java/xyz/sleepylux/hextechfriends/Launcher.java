package xyz.sleepylux.hextechfriends;

import io.sentry.Sentry;
import org.apache.logging.log4j.LogManager;
import xyz.sleepylux.hextechfriends.server.HextechServer;

public class Launcher {
    public static void main(String[] args){
        Sentry.init("https://19fe83e1006e4a70976981fa21b22579@sentry.sleepylux.xyz/2");


        HextechServer server = new HextechServer(9090);
        server.start();
    }
}
