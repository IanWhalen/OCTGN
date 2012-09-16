using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Skylabs.Lobby;
using agsXMPP;

namespace Octgn.Controls
{
    /// <summary>
    /// Interaction logic for ChatTableRow.xaml
    /// </summary>
    public partial class ChatTableRow : TableRow
    {
        /// <summary>
        /// Gets or sets the user
        /// </summary>
        public NewUser User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                Dispatcher.BeginInvoke(new Action(() =>
                                                      {
                                                          UsernameParagraph.Inlines.Clear();
                                                          UsernameParagraph.Inlines.Add(new Run(_user.User.User));
                                                          UsernameColumn.Width = new GridLength(GetUsernameWidth());
                                                      }));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                Dispatcher.BeginInvoke(new Action(() =>
                                                      {
                                                          MessageParagraph.Inlines.Clear();
                                                          MessageParagraph.Inlines.Add(new Run(_message));
                                                      }));
            }
        }

        public DateTime MessageDate
        {
            get { return _messageDate; }
            set
            {
                _messageDate = value;
                Dispatcher.BeginInvoke(new Action(() =>
                                                      {
                                                          TimeParagraph.Inlines.Clear();
                                                          TimeParagraph.Inlines.Add(new Run(_messageDate.ToShortTimeString()));
                                                      }));
            }
        }

        public LobbyMessageType MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        private NewUser _user;
        private string _message;
        private DateTime _messageDate;
        private LobbyMessageType _messageType;

        public ChatTableRow()
        {
            InitializeComponent();
            User = new NewUser(new Jid("NoUser","server.octgn.info","agsxmpp"));
            MessageDate = DateTime.Now;
            Message = "TestMessage";
        }

        private double GetUsernameWidth()
        {
            var f =  new FormattedText(User.User.User,
                              CultureInfo.CurrentCulture,
                              FlowDirection.LeftToRight,
                              new Typeface("Arial"), 12,
                              Brushes.Black);
            f.SetFontWeight(FontWeights.Bold);
            return f.Width + 5;
        }
    }
}
