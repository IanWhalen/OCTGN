// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatBar.cs" company="OCTGN">
//   GNU Whatever
// </copyright>
// <summary>
//   Defines the ChatBar type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Controls
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Octgn.Extentions;

    using Skylabs.Lobby;

    /// <summary>
    /// The chat bar. 
    /// </summary>
    public class ChatBar : TabControl
    {
        private GridLength barHeight = new GridLength(1,GridUnitType.Auto);

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatBar"/> class.
        /// </summary>
        public ChatBar()
        {
            this.TabStripPlacement = Dock.Bottom;
            if (!this.IsInDesignMode())
            {
                this.Loaded +=
                    (sender, args) => Program.OctgnInstance.LobbyClient.Chatting.OnCreateRoom += this.LobbyCreateRoom;
            }
        }

        /// <summary>
        /// Gets or sets the height of the bar.
        /// </summary>
        public GridLength BarHeight
        {
            get
            {
                return this.barHeight;
            }

            set
            {
                this.barHeight = value;
                this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        foreach (var cb in this.Items.OfType<ChatBarItem>())
                        {
                            cb.Height = this.barHeight.Value;
                        }
                    }));
            }
        }

        /// <summary>
        /// This happens when a new room is created.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="room">
        /// The room.
        /// </param>
        private void LobbyCreateRoom(object sender, NewChatRoom room)
        {
            var r = room;
            this.Dispatcher.BeginInvoke(
                new Action(() => this.Items.Add(new ChatBarItem(r) { Height = this.barHeight.Value })));
        }
    }
}
