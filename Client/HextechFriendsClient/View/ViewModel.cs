using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.View
{
    public class ViewModel
    {
        private ViewState current;
        private MainWindow window;
        public JoinLobbyView joinLobbyView;
        public CreateLobbyView createLobbyView;
        public CurrentLobbyView currentLobbyView;
        public AcceptUserView acceptUserView;
        public AppManager appManager;

        public ViewModel(MainWindow mainWindow, AppManager manager)
        {
            window = mainWindow;
            appManager = manager;
            joinLobbyView = new JoinLobbyView(manager);
            createLobbyView = new CreateLobbyView(manager);
            currentLobbyView = new CurrentLobbyView(manager);
            acceptUserView = new AcceptUserView(manager);
        }

        public void ChangeView(ViewState state)
        {
            current = state;
            switch (state)
            {
                case ViewState.JOIN_LOBBY:
                    window.SetView(joinLobbyView);
                    break;
                case ViewState.CREATE_LOBBY:
                    window.SetView(createLobbyView);
                    break;
                case ViewState.CURRENT_LOBBY:
                    window.SetView(currentLobbyView);
                    break;
                case ViewState.ACCEPT_USER:
                    window.SetView(acceptUserView);
                    break;
            }
        }

        public dynamic GetView<T>()
        {
            Type type = typeof(T);
            if(type == typeof(JoinLobbyView))
            {
                return joinLobbyView;
            }
            else if(type == typeof(CreateLobbyView))
            {
                return createLobbyView;
            }
            else if(type == typeof(CurrentLobbyView))
            {
                return currentLobbyView;
            }
            else
            {
                return acceptUserView;
            }
        }

        public void EndInit()
        {
            ChangeView(ViewState.JOIN_LOBBY);
            joinLobbyView.setLeagueStatus(appManager.leagueClient.Connected);
        }

        public ViewState GetCurrentView()
        {
            return current;
        }
    }

    public enum ViewState
    {
        JOIN_LOBBY,
        CREATE_LOBBY,
        CURRENT_LOBBY,
        ACCEPT_USER
    }
}
