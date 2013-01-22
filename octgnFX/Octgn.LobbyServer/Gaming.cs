using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Skylabs.Lobby;

namespace Skylabs.LobbyServer
{
    using Skylabs.Lobby.Library;
    using Skylabs.Lobby.Library.Models;

    public static class Gaming
    {
        private static int currentHostPort = 10000;
        private static long totalHostedGames;
        private static readonly ReaderWriterLockSlim Locker; 

        static Gaming()
        {
            Locker = new ReaderWriterLockSlim();
            Games = new Dictionary<Guid, HostedGameProcess>();
        }

        private static Dictionary<Guid, HostedGameProcess> Games { get; set; }

        public static int GameCount()
        {
            Locker.EnterReadLock();
                int ret = Games.Count;
            Locker.ExitReadLock();
            return ret;
        }

        public static void Stop()
        {
            Locker.EnterWriteLock();
                foreach (var g in Games)
                {
                    g.Value.Stop();
                }
                Games.Clear();
            Locker.ExitWriteLock();
        }

        public static long TotalHostedGames()
        {
            Locker.EnterReadLock();
                long ret = totalHostedGames;
            Locker.ExitReadLock();
            return ret;
            
        }

        public static int HostGame(Uri host, Guid g, Version v, string gameName,string name, string pass, IUser u)
        {
            Locker.EnterWriteLock();//Enter Lock
            while (Games.Any(x=>x.Value.Host.Port == currentHostPort) || !Networking.IsPortAvailable(currentHostPort))
            {
                currentHostPort++;
                if (currentHostPort >= 20000)
                    currentHostPort = 10000;
            }
            var uriBuilder = new UriBuilder(host) { Port = currentHostPort };

            var hs = new HostedGameProcess(uriBuilder.Uri, g, v, gameName,name, pass, u);
            if (hs.Start())
            {
                Games.Add(hs.Id, hs);
                totalHostedGames++;
                Locker.ExitWriteLock();//Exit Lock
                return currentHostPort;
            }
            Locker.ExitWriteLock();//Exit Lock
            return -1;

        }

        public static void StartGame(Guid gameId)
        {
            Locker.EnterWriteLock();
                try
                {
                    Games[gameId].SetStatus(GameStatus.InProgress);
                }
                catch (Exception e)
                {
                    Logger.Er(e);
                }
            Locker.ExitWriteLock();
        }

        public static List<Lobby.Library.Models.HostedGame> GetLobbyList()
        {
            Locker.EnterReadLock();
            List<Lobby.Library.Models.HostedGame> sendgames =
                Games.Select(
                    g =>
                    new Lobby.Library.Models.HostedGame(g.Value, false)).ToList();
            Locker.ExitReadLock();
            return sendgames;
        }

        private static void HostedGameExited(object sender, EventArgs e)
        {
            Locker.EnterWriteLock();
                var s = sender as HostedGameProcess;
                if (s == null)
                {
                    Locker.ExitWriteLock();
                    return;
                }
                s.SetStatus(GameStatus.Stopped);
                Games.Remove(s.Id);
            Locker.ExitWriteLock();
            GameBot.RefreshLists();
        }
    }
}