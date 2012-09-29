using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Octgn.Controls;
using Skylabs.Lobby;
using Skylabs.Lobby.Threading;
using agsXMPP;

namespace Octgn.Launcher
{
    /// <summary>
    ///   Interaction logic for ContactList.xaml
    /// </summary>
    public partial class ContactList
    {
        public ContactList()
        {
            InitializeComponent();
            Program.LobbyClient.OnDataReceived += this.LobbyClientOnOnDataReceived;
            Program.LobbyClient.Chatting.OnCreateRoom += ChattingOnOnCreateRoom;
        }

        private void ChattingOnOnCreateRoom(object sender , NewChatRoom room)
        {
            LazyAsync.Invoke(RefreshList);
        }

        private void LobbyClientOnOnDataReceived(object sender, DataRecType type, object data)
        {
            if (type == DataRecType.FriendList)
            {
                RefreshList();
            }
        }

        public void RefreshList()
        {
            Dispatcher.Invoke(new Action(() =>
                                             {
                stackPanel1.Children.Clear();
                NewUser[] flist = Program.LobbyClient.Friends.ToArray();
                foreach (FriendListItem f in flist.Select(u => new FriendListItem
                                                                {
                                                                    ThisUser = u,
                                                                    HorizontalAlignment
                                                                        =
                                                                        HorizontalAlignment
                                                                        .
                                                                        Stretch
                                                                }))
                {
                    f.MouseDoubleClick += FMouseDoubleClick;
                    stackPanel1.Children.Add(f);
                }
                foreach( var g in Program.LobbyClient.Chatting.Rooms.Where(x=>x.IsGroupChat))
                {
                    var gc = new GroupChatListItem()
                    {
                        ThisRoom = g ,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    gc.MouseDoubleClick += GiMouseDoubleClick;
                    stackPanel1.Children.Add(gc);
                }
                                             }));
        }


        private static void GiMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var fi = sender as GroupChatListItem;
            if (fi == null) return;
            var room = Program.LobbyClient.Chatting.GetRoom(fi.ThisRoom.GroupUser,true);
        }


        private static void FMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var fi = sender as FriendListItem;
            if (fi == null) return;
            var room = Program.LobbyClient.Chatting.GetRoom(fi.ThisUser);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }


        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
            Program.LobbyClient.OnDataReceived -= this.LobbyClientOnOnDataReceived;
        }
    }
}