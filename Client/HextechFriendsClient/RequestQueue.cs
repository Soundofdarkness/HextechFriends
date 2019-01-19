using HextechFriendsClient.Protocol.Server;
using HextechFriendsClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient
{
    public class RequestQueue
    {
        private AppManager appManager;
        private Queue<RequestJoinTC> queue;
        public RequestQueue(AppManager manager)
        {
            appManager = manager;
            queue = new Queue<RequestJoinTC>();
        }


        public void AddToQueue(RequestJoinTC requestJoinTC)
        {
            queue.Enqueue(requestJoinTC);
            RunQueue(false);
        }


        public void RunQueue(bool bypass)
        {
            if(queue.Count > 0 && !bypass)
            {
                if (appManager.ViewModel.GetCurrentView() == ViewState.ACCEPT_USER || !bypass) return;
                var dto = queue.Dequeue();
                AcceptUserView view = appManager.ViewModel.GetView<AcceptUserView>();
                view.AcceptUser(dto);
            }
        }
    }
}
