using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace HextechFriendsClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, ssl) => true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length != 1) return;
            if (e.Args[0].Contains("--override-url="))
            {
                string newurl = e.Args[0].Split('=')[1];
                Constants.SERVER_URL = newurl;
            }
        }
    }
}
