namespace Skylabs.LobbyServer
{
    using Microsoft.AspNet.SignalR;

    using Owin;

    public class HubStart
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapHubs("/", new HubConfiguration());
        }
    }
}
