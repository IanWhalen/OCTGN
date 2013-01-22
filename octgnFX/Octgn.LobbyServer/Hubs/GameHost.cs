namespace Skylabs.LobbyServer.Hubs
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    using Microsoft.AspNet.SignalR;

    using Skylabs.Lobby.Library;
    using Skylabs.Lobby.Library.Models;

    internal class GameHost : Hub
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal GameHost()
        {
        }

        internal IHostedGame RegisterGame(IHostedGame game)
        {
            var p = new HostedGameProcess(game);
            p.SetStatus(GameStatus.Registered);
            var app = new Process();
            var curFullPath = new FileInfo(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), Program.SASLocation)));
            var sasExe = curFullPath.Name;
            var curPath = curFullPath.DirectoryName;
            var newPath = Path.Combine(Path.GetTempPath(), game.Id.ToString());
            
            Lobby.Library.Util.IO.DirectoryCopy(curPath,newPath,true);

            app.StartInfo.FileName = Path.Combine(newPath,sasExe);
            app.StartInfo.Arguments = string.Format("-i={0} -h={1}",game.Id,Program.Host);
            Log.InfoFormat("Starting SAS {0} {1}",app.StartInfo.FileName,app.StartInfo.Arguments);
            return new HostedGame(p,false);
        }
    }
}
