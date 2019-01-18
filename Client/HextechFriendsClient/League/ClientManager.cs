using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HextechFriendsClient.League
{
    public class ClientManager
    {
        public string Password;
        public int Port;
        public string AuthorizationToken;

        public ClientManager()
        {
            if (!LeagueRunning())
            {
                Dialogs.LeagueNotRunning();
                return;
            }
            GetLcuEndpoint();
        }

        public bool LeagueRunning()
        {
            var processes = Process.GetProcessesByName("League").Where(p => p.MainModule.FileName.Contains("LeagueClient.exe"));
            return processes != null;
        }

        private void GetLcuEndpoint()
        {
            if (!LeagueRunning()) return;
            string path = GetLCUPath();
            if (path == null) return;
            File.Copy(path + "lockfile", path + "tmp");
            var content = File.ReadAllText(path + "tmp", Encoding.UTF8);
            File.Delete(path + "tmp");
            var lockfile = content.Split(':');
            Port = int.Parse(lockfile[2]);
            Password = lockfile[3];
            AuthorizationToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"riot:{Password}"));
        }

        private string GetLCUPath()
        {
            var leagueProc = Process.GetProcesses().Where(p => p.ProcessName.Contains("League"));
            foreach(var proc in leagueProc)
            {
                try
                {
                    string cmdline = GetCommandLine(proc);
                    var index = cmdline.IndexOf("--install-directory");
                    if (index == -1) continue;
                    index = cmdline.IndexOf("=", index) + 1;
                    return cmdline.Substring(index, cmdline.IndexOf("\"", index) - index);
                }
                catch(Win32Exception ex) when (ex.HResult == -2146233079) { };
            }
            return null;
        }

        private string GetCommandLine(Process process)
        {
            using (var searcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {process.Id}"))
            using(var objects = searcher.Get())
            {
                return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }
        }


    }
}
