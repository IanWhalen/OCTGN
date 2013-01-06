using System;

namespace Octgn
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Forms;

    using Octgn.Data;

    /// <summary>
    /// The methods to reimplement.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class MethodsToReimplement
    {
        /// <summary>
        /// was found in Login.xaml.cs
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void ChangeDataDirectory()
        {
            var pf = new FolderBrowserDialog();
            pf.SelectedPath = GamesRepository.BasePath;
            var dr = pf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (pf.SelectedPath.ToLower() != GamesRepository.BasePath.ToLower())
                {
                    Prefs.DataDirectory = pf.SelectedPath;
                    var asm = System.Reflection.Assembly.GetExecutingAssembly();
                    var thispath = asm.Location;
                    Program.Exit();
                    Process.Start(thispath);
                    /*Application.Current.Exit += delegate(object o , ExitEventArgs args)
                                                {
                                                    Process.Start(thispath);

                                                };*/
                }
            }
        }

        /// <summary>
        /// Was found in Login.xaml.cs
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void OfflineGameConnect()
        {
            // var g = new GameList();
            // g.Row2.Height = new GridLength(25);
            // g.btnCancel.Click += delegate(object o, RoutedEventArgs args)
            // { if (NavigationService != null) NavigationService.GoBack(); };
            // g.OnGameClick += GOoffConnOnGameClick;
            // if (NavigationService != null) NavigationService.Navigate(g);
        }

        /// <summary>
        /// Found in Login.xaml.cs
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void OfflineGameClickedConnect()
        {
            // var hg = sender as Octgn.Data.Game;
            // if (hg == null || Program.PlayWindow != null)
            // {
            // if (NavigationService != null) NavigationService.Navigate(new Login());
            // return;
            // }
            // Program.IsHost = false;
            // Data.Game theGame =
            // Program.GamesRepository.Games.FirstOrDefault(g => g.Id == hg.Id);
            // if (theGame == null)
            // {
            // if (NavigationService != null) NavigationService.Navigate(new Login());
            // return;
            // }
            // Program.Game = new Data.Game(GameDef.FromO8G(theGame.FullPath), true);
            // if (NavigationService != null) NavigationService.Navigate(new ConnectLocalGame());
        }

        /// <summary>
        /// Found in Login.xaml.cs
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void MenuOfflineClick(object sender, RoutedEventArgs e)
        {
            // var g = new GameList();
            // var sg = new StartGame();
            // g.Row2.Height = new GridLength(25);
            // g.btnCancel.Click += delegate(object o, RoutedEventArgs args)
            // {
            // if (NavigationService != null) NavigationService.GoBack();
            // };
            // g.OnGameClick += GOnOnGameClick;
            // if (NavigationService != null) NavigationService.Navigate(g);
        }

        /// <summary>
        /// Found in Login.xaml.cs
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="eventArgs">
        /// The event Args.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void GOnOnGameClick(object sender, EventArgs eventArgs)
        {
            // var hg = sender as Octgn.Data.Game;
            // if (hg == null || Program.PlayWindow != null)
            // {
            // if (NavigationService != null) NavigationService.Navigate(new Login());
            // return;
            // }
            // var hostport = 5000;
            // while (!Skylabs.Lobby.Networking.IsPortAvailable(hostport))
            // hostport++;
            // var hs = new HostedGame(hostport, hg.Id, hg.Version, "LocalGame", string.Empty, null,true);
            // hs.HostedGameDone += hs_HostedGameDone;
            // if (!hs.StartProcess())
            // {
            // hs.HostedGameDone -= hs_HostedGameDone;
            // if (NavigationService != null) NavigationService.Navigate(new Login());
            // return;
            // }

            // Program.IsHost = true;
            // Game theGame =
            // Program.GamesRepository.Games.FirstOrDefault(g => g.Id == hg.Id);
            // if (theGame == null) return;
            // Program.Game = new Game(GameDef.FromO8G(theGame.FullPath),true);

            // var ad = new IPAddress[1];
            // IPAddress ip = IPAddress.Parse("127.0.0.1");

            // if (ad.Length <= 0) return;
            // try
            // {
            // Program.Client = new Client(ip, hostport);
            // Program.Client.Connect();
            // Dispatcher.Invoke(new Action(() =>
            // {
            // if(NavigationService != null)
            // NavigationService.Navigate(new StartGame(true) {Width = 400});
            // }));
            // }
            // catch (Exception ex)
            // {
            // Debug.WriteLine(ex);
            // if (Debugger.IsAttached) Debugger.Break();
            // }
        }
    }
}
