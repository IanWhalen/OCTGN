using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skylabs.Lobby;

namespace Octgn
{
    public sealed class OctgnInstance
    {
        public Client LobbyClient { get; set; }

        public OctgnInstance()
        {
            LobbyClient = new Client();
        }

        public void Close()
        {
            if(LobbyClient != null)
                LobbyClient.Stop();
        }
    }
}
