using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using Octgn.Data;
using Octgn.DeckBuilder;
using Octgn.Launcher;
using Octgn.Networking;
using Octgn.Play;
using Octgn.Utils;
using Skylabs.Lobby;
using Client = Octgn.Networking.Client;
using LauncherWindow = Octgn.Windows.LauncherWindow;
using Main = Octgn.Windows.Main;
using RE = System.Text.RegularExpressions;

namespace Octgn
{
    public static class Program
    {
        public static Windows.DWindow DebugWindow;
        public static Main MainWindow;
        public static DeckBuilderWindow DeckEditor;
        public static PlayWindow PlayWindow;

        //TODO This needs to be removed
        public static Skylabs.Lobby.Client LobbyClient { get { return OctgnInstance.LobbyClient; } }

        public static Game Game;
        public static OctgnInstance OctgnInstance = new OctgnInstance("of.octgn.net");
        public static GameSettings GameSettings = new GameSettings();
        public static GamesRepository GamesRepository;
        internal static Client Client;

        internal static bool IsGameRunning;
        internal static readonly string BasePath;
        internal static readonly string GamesPath;
        internal static ulong PrivateKey = ((ulong) Crypto.PositiveRandom()) << 32 | Crypto.PositiveRandom();

#pragma warning disable 67
        internal static event EventHandler<ServerErrorEventArgs> ServerError;
#pragma warning restore 67

        internal static bool IsHost { get; set; }

        internal static Dispatcher Dispatcher;

        internal static readonly TraceSource Trace = new TraceSource("MainTrace", SourceLevels.Information);
        internal static readonly TraceSource DebugTrace = new TraceSource("DebugTrace", SourceLevels.All);
        internal static readonly CacheTraceListener DebugListener = new CacheTraceListener();
        internal static Inline LastChatTrace;

        private static bool _locationUpdating;

        static Program()
        {
#if(!DEBUG)
            var pList = Process.GetProcessesByName("OCTGN");
            if(pList != null && pList.Length > 0 && pList.Any(x=>x.Id != Process.GetCurrentProcess().Id))
            {
                var res = MessageBox.Show("Another instance of OCTGN is current running. Would you like to close it?","OCTGN",MessageBoxButton.YesNo,MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    foreach (var p in Process.GetProcessesByName("OCTGN"))
                    {
                        if (p.Id != Process.GetCurrentProcess().Id)
                            p.Kill();
                    }
                }
            }
#endif


            Debug.Listeners.Add(DebugListener);
            DebugTrace.Listeners.Add(DebugListener);
            Trace.Listeners.Add(DebugListener);
            BasePath = Path.GetDirectoryName(typeof (Program).Assembly.Location) + '\\';
            GamesPath = BasePath + @"Games\";
            MainWindow = new Main();
            Application.Current.MainWindow = MainWindow;
            //OctgnInstance.LobbyClient.Chatting.OnCreateRoom += Chatting_OnCreateRoom;
        }

        public static void StopGame()
        {
            if (Client != null)
            {
                Client.Disconnect();
                Client = null;
            }
            Game.End();
            Game = null;
            Dispatcher = null;
            IsGameRunning = false;
        }

        public static void SaveLocation()
        {
            if (_locationUpdating) return;
            _locationUpdating = true;
            _locationUpdating = false;
        }

        public static void Exit()
        {
            Application.Current.MainWindow = null;
            SaveLocation();
            try
            {
                if (DebugWindow != null)
                    if (DebugWindow.IsLoaded)
                        DebugWindow.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                if (Debugger.IsAttached) Debugger.Break();
            }
            if (MainWindow != null)
                if (MainWindow.IsLoaded)
                    MainWindow.Close();
            if (PlayWindow != null)
                if (PlayWindow.IsLoaded)
                    PlayWindow.Close();
            //Apparently this can be null sometimes?
            if(Application.Current != null)
                Application.Current.Shutdown(0);
        }

        internal static void Print(Player player, string text)
        {
            string finalText = text;
            int i = 0;
            var args = new List<object>(2);
            Match match = Regex.Match(text, "{([^}]*)}");
            while (match.Success)
            {
                string token = match.Groups[1].Value;
                finalText = finalText.Replace(match.Groups[0].Value, "{" + i + "}");
                i++;
                object tokenValue = token;
                switch (token)
                {
                    case "me":
                        tokenValue = player;
                        break;
                    default:
                        if (token.StartsWith("#"))
                        {
                            int id;
                            if (!int.TryParse(token.Substring(1), out id)) break;
                            ControllableObject obj = ControllableObject.Find(id);
                            if (obj == null) break;
                            tokenValue = obj;
                            break;
                        }
                        break;
                }
                args.Add(tokenValue);
                match = match.NextMatch();
            }
            args.Add(player);
            Trace.TraceEvent(TraceEventType.Information,
                             EventIds.Event | EventIds.PlayerFlag(player) | EventIds.Explicit, finalText, args.ToArray());
        }

        internal static void TracePlayerEvent(Player player, string message, params object[] args)
        {
            var args1 = new List<object>(args) {player};
            Trace.TraceEvent(TraceEventType.Information, EventIds.Event | EventIds.PlayerFlag(player), message,
                             args1.ToArray());
        }

        internal static void TraceWarning(string message)
        {
            Trace.TraceEvent(TraceEventType.Warning, EventIds.NonGame, message);
        }

        internal static void TraceWarning(string message, params object[] args)
        {
            Trace.TraceEvent(TraceEventType.Warning, EventIds.NonGame, message, args);
        }
    }
}