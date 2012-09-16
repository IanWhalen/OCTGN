using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
using Octgn.Controls;
using Skylabs.Lobby;

namespace Octgn.Launcher
{
    /// <summary>
    /// Interaction logic for LobbyChat.xaml
    /// </summary>
    public partial class LobbyChat : UserControl
    {
        #region Design Mode Detection
        private static bool? _isInDesignMode;

        /// <summary>
        /// Gets a value indicating whether the control is in design mode (running in Blend
        /// or Visual Studio).
        /// </summary>
        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
#if SILVERLIGHT
            _isInDesignMode = DesignerProperties.IsInDesignTool;
#else
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode
                        = (bool)DependencyPropertyDescriptor
                        .FromProperty(prop, typeof(FrameworkElement))
                        .Metadata.DefaultValue;
#endif
                }

                return _isInDesignMode.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the control is in design mode (running under Blend
        /// or Visual Studio).
        /// </summary>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "Non static member needed for data binding")]
        public bool IsInDesignMode
        {
            get
            {
                return IsInDesignModeStatic;
            }
        }
        #endregion

        private NewChatRoom _room;
        private IEnumerable<NewUser> _users;
        private bool _shiftDown;
        private List<String> _messageCache;
        private int _curMessageCacheItem;
        public LobbyChat()
        {
            InitializeComponent();
            _messageCache = new List<string>();
            if(!IsInDesignMode)
                Program.OctgnInstance.LobbyClient.Chatting.OnCreateRoom += Chatting_OnCreateRoom;
        }

        void Chatting_OnCreateRoom(object sender, Skylabs.Lobby.NewChatRoom room)
        {
            if (room.GroupUser.User.User == "lobby" && _room == null)
            {
                _room = room;
                InvokeResetUserList();
                _room.OnUserListChange += RoomOnOnUserListChange;
                _room.OnMessageRecieved += RoomOnOnMessageRecieved;
            }

        }

        private void RoomOnOnMessageRecieved(object sender, NewUser @from, string message, DateTime rTime, LobbyMessageType mType)
        {
            var theFrom = from;
            var theMessage = message;
            var therTime = rTime;
            var themType = mType;
            if (String.IsNullOrWhiteSpace(theFrom.User.User))
                theFrom.User.User = "SYSTEM";
            Dispatcher.BeginInvoke(new Action(() =>
                                                  {

                                                      var ctr = new ChatTableRow();
                                                      ctr.User = theFrom;
                                                      ctr.Message = theMessage;
                                                      ctr.MessageDate = therTime;
                                                      ctr.MessageType = themType;

                                                      ctr.MouseEnter += (o, args) =>
                                                                            {
                                                                                foreach (var r in ChatRowGroup.Rows)
                                                                                {
                                                                                    var rr = r as ChatTableRow;
                                                                                    if (rr != null)
                                                                                    {
                                                                                        if (rr.User.User.User ==
                                                                                            theFrom.User.User)
                                                                                            r.Background =
                                                                                                Brushes.DimGray;
                                                                                    }
                                                                                }
                                                                            };
                                                      ctr.MouseLeave += (o, args) =>
                                                                            {
                                                                                foreach (var r in ChatRowGroup.Rows)
                                                                                    r.Background = null;
                                                                            };
                                                      ChatRowGroup.Rows.Add(ctr);
                                                  }));
        }

        private void RoomOnOnUserListChange(object sender, List<NewUser> users)
        {
            _users = users.ToArray();
            InvokeResetUserList();
        }

        private void InvokeResetUserList()
        {
            Dispatcher.BeginInvoke(new Action(ResetUserList));
        }

        private void ResetUserList()
        {
            if (_room == null)
                return;
            var users = _room.Users.ToArray().Where(x=>x.User.User.ToLower().Contains(UserFilter.Text.ToLower())).OrderBy(x=>x.User.User);
            UserList.Items.Clear();
            foreach(var u in users)
            {
                UserList.Items.Add(u);
            }
        }

        private void UserFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResetUserList();
        }

        private void ChatInput_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (_room == null)
                return;

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                _shiftDown = false;
        }

        private void ChatInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_room == null)
                return;

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                _shiftDown = true;
            if(!_shiftDown && e.Key == Key.Enter)
            {
                _messageCache.Add(ChatInput.Text);
                if (_messageCache.Count >= 51)
                    _messageCache.Remove(_messageCache.Last());
                _room.SendMessage(ChatInput.Text);
                ChatInput.Clear();
                _curMessageCacheItem = -1;
                e.Handled = true;
            }
            else if(e.Key == Key.Up)
            {
                if (_messageCache.Count == 0) return;
                if(_curMessageCacheItem - 1 >= 0)
                    _curMessageCacheItem--;
                else
                    _curMessageCacheItem = _messageCache.Count - 1;
                ChatInput.Text = _messageCache[_curMessageCacheItem];
            }
            else if(e.Key == Key.Down)
            {
                if (_messageCache.Count == 0) return;
                if (_curMessageCacheItem + 1 <= _messageCache.Count - 1)
                    _curMessageCacheItem++;
                else
                    _curMessageCacheItem = 0;
                ChatInput.Text = _messageCache[_curMessageCacheItem];
            }
        }
    }
}
