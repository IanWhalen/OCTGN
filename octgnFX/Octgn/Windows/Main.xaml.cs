using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Octgn.Controls;
using Skylabs.Lobby;

namespace Octgn.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : OctgnChrome
    {
        private string ConnectMessage
        {
            get
            {
                var tbText = "";
                Dispatcher.Invoke(new Action(() =>
                                                 {
                                                     tbText = tbConnect.Content as String;
                                                     ConnectBox.Visibility = String.IsNullOrWhiteSpace(tbText) ? Visibility.Hidden : Visibility.Visible;
                                                 }));
                return tbText;
            }
            set
            {
                Dispatcher.BeginInvoke(new Action(() =>
                                                      {
                                                          tbConnect.Content = value;
                                                          ConnectBox.Visibility = String.IsNullOrWhiteSpace(value) ? Visibility.Hidden : Visibility.Visible;
                                                      }));
            }
        }

        public Main()
        {
            InitializeComponent();
            ConnectBox.Visibility = Visibility.Hidden;
            Program.OctgnInstance.LobbyClient.OnStateChanged += LobbyClientOnOnStateChanged;
            Program.OctgnInstance.LobbyClient.OnLoginComplete += LobbyClientOnOnLoginComplete;
        }

        #region LobbyEvents

        private void LobbyClientOnOnStateChanged(object sender, string state)
        {
            ConnectMessage = state;
        }

        private void LobbyClientOnOnLoginComplete(object sender, Client.LoginResults results)
        {
            switch (results)
            {
                case Client.LoginResults.ConnectionError:
                    SetStateOffline();
                    break;
                case Client.LoginResults.Success:
                    Dispatcher.BeginInvoke(new Action(()=>ConnectBox.Visibility = Visibility.Hidden));
                    SetStateOnline();
                    break;
                case Client.LoginResults.Failure:
                    SetStateOffline();
                    break;
            }
        }

        #endregion

        #region Main States

        private void SetStateOffline()
        {
            //Must be wrapped in a dispatcher
        }
        
        private void SetStateReconnecting()
        {
            //Must be wrapped in a dispatcher
        }

        private void SetStateOnline()
        {
            //Must be wrapped in a dispatcher
        }

        #endregion 

        private void borderHeader_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Thumb_DragDelta_1(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (this.Width + e.HorizontalChange > 10)
                this.Width += e.HorizontalChange;
            if (this.Height + e.VerticalChange > 10)
                this.Height += e.VerticalChange;
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            var w = new AboutWindow();
            w.ShowDialog();
        }
    }
}
