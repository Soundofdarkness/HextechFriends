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

        public ViewModel(MainWindow mainWindow)
        {
            window = mainWindow
            joinLobbyView = new JoinLobbyView(window.appManager);
            createLobbyView = new CreateLobbyView(window.appManager);
            currentLobbyView = new CurrentLobbyView(window.appManager);
            acceptUserView = new AcceptUserView(window.appManager);

            ChangeView(ViewState.JOIN_LOBBY);
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

        public JoinLobbyView GetView<T>() where T : JoinLobbyView
        {
            return joinLobbyView;
        }

        public CreateLobbyView GetView<T>() where T : CreateLobbyView
        {
            return createLobbyView;
        }

        publ

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
    }

    public enum ViewState
    {
        JOIN_LOBBY,
        CREATE_LOBBY,
        CURRENT_LOBBY,
        ACCEPT_USER
    }
}
