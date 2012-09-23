// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatBarItem.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Defines the ChatBarItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Controls
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Octgn.Extentions;

    using Skylabs.Lobby;

    using Uri = System.Uri;

    /// <summary>
    /// The chat bar item.
    /// </summary>
    public class ChatBarItem : TabItem
    {
        /// <summary>
        /// Sets the Chat Room
        /// </summary>
        private readonly NewChatRoom room;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatBarItem"/> class.
        /// </summary>
        /// <param name="chatRoom">
        /// The chat Room.
        /// </param>
        public ChatBarItem(NewChatRoom chatRoom = null)
        {
            this.room = chatRoom;
            this.ConstructControl();
        }

        /// <summary>
        /// Happens when you mouse up on the tab header.
        /// </summary>
        public event MouseButtonEventHandler HeaderMouseUp;

        /// <summary>
        /// Constructs the control.
        /// </summary>
        private void ConstructControl()
        {
            this.Style = (Style)Application.Current.FindResource(typeof(TabItem));
            this.Padding = new Thickness(0);

            this.ConstructHeader();
            this.ConstructChat();
        }

        /// <summary>
        /// Fires the Header Mouse Up event
        /// </summary>
        /// <param name="args">
        /// The arguments.
        /// </param>
        private void FireHeaderMouseUp(MouseButtonEventArgs args)
        {
            if (this.HeaderMouseUp != null)
            {
                this.HeaderMouseUp.Invoke(this, args);
            }
        }

        /// <summary>
        /// Constructs the header
        /// </summary>
        private void ConstructHeader()
        {
            // Main content object
            var mainBorder = new Border { Margin = new Thickness(5, 0, 5, 0), VerticalAlignment = VerticalAlignment.Center };
            
            // Main content grid
            var g = new Grid();
            g.ColumnDefinitions.Add(new ColumnDefinition());
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(21) });

            // Create item label
            var label = new TextBlock() { VerticalAlignment = VerticalAlignment.Center };
            if (this.IsInDesignMode() || this.room == null)
            {
                label.Inlines.Add(new Run("test"));
            }
            else
            {
                if (this.room.GroupUser != null)
                {
                    label.Inlines.Add(new Run(this.room.GroupUser.User.User));
                }
                else
                {
                    var otherUser = this.room.Users.FirstOrDefault(x => x.User.User.ToLowerInvariant() != Program.OctgnInstance.LobbyClient.Me.User.User.ToLowerInvariant());
                    if (
                        otherUser != null)
                    {
                        label.Inlines.Add(new Run(otherUser.User.User));
                    }
                }
            }

            // Create close button
            var borderClose = new Border { Width = 16, Height = 16, HorizontalAlignment = HorizontalAlignment.Right };
            var imageClose = new Image()
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/OCTGN;component/Resources/close.png")), 
                    Stretch = Stretch.Uniform, 
                    VerticalAlignment = VerticalAlignment.Stretch, 
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

            // --Add items to items

            // Add close image to closeBorder
            borderClose.Child = imageClose;

            // Add Close 'button' to grid
            g.Children.Add(borderClose);
            Grid.SetColumn(borderClose, 1);

            // Add label to main grid
            g.Children.Add(label);

            // Add main grid to main border
            mainBorder.Child = g;

            // Add main grid to this
            this.Header = mainBorder;
            mainBorder.PreviewMouseUp += this.MainBorderOnPreviewMouseUp;
        }

        /// <summary>
        /// Happens when the mouse goes up over the header.
        /// </summary>
        /// <param name="sender">The header</param>
        /// <param name="mouseButtonEventArgs">Mouse Arguments</param>
        private void MainBorderOnPreviewMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            this.FireHeaderMouseUp(mouseButtonEventArgs);
        }

        /// <summary>
        /// Constructs the chat portion of the control.
        /// </summary>
        private void ConstructChat()
        {
            var chatBorder = new Border() { BorderBrush = Brushes.DarkGray, BorderThickness = new Thickness(1),Padding = new Thickness(3) };
            var chatControl = new ChatControl { Width = 400, Height = 250 };

            chatBorder.Child = chatControl;

            this.Content = chatBorder;

            if (this.room != null)
            {
                chatControl.SetRoom(this.room);
            }
        }
    }
}
