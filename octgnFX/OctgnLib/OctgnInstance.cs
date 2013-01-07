// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctgnInstance.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Defines the OctgnInstance type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn
{
    using Skylabs.Lobby;

    /// <summary>
    /// The OCTGN Main Instance.
    /// </summary>
    public sealed class OctgnInstance
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OctgnInstance"/> class.
        /// </summary>
        /// <param name="lobbyHost">Server host for Lobby</param>
        public OctgnInstance(string lobbyHost)
        {
            this.LobbyClient = new Client(lobbyHost);
        }

        /// <summary>
        /// Gets or sets the Lobby client.
        /// </summary>
        public Client LobbyClient { get; set; }

        /// <summary>
        /// Closes down the Octgn Instance.
        /// </summary>
        public void Close()
        {
            if (this.LobbyClient != null)
            {
                this.LobbyClient.Stop();
            }
        }
    }
}
