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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Skylabs.Lobby;

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
        public ChatBarItem(NewChatRoom chatRoom)
        {
            this.room = chatRoom;
            this.ConstructControl();
        }

        /// <summary>
        /// Constructs this control
        /// </summary>
        private void ConstructControl()
        {
            this.Background = Brushes.Transparent;

            // Main content object
            var mainBorder = new Border { Margin = new Thickness(5) };

            // Main content grid
            var g = new Grid();
            g.ColumnDefinitions.Add(new ColumnDefinition());
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(16) });

            // Create item label
            var label = new TextBlock(new Run(this.room.GroupUser.User.User))
                { VerticalAlignment = VerticalAlignment.Center };

            // Create close button
            var borderClose = new Border { Width = 16, Height = 16 };
            var imageClose = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Octgn;component/Resources/close.png")),Stretch = Stretch.Uniform };

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
        }
    }
}
