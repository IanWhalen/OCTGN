namespace Skylabs.LobbyServer
{
    using System;

    using Skylabs.Lobby.Library;
    using Skylabs.Lobby.Library.Models;

    public class HostedGameProcess : IHostedGame
    {
        public Guid Id { get; private set; }

        public Guid GameId { get; private set; }

        public Version Version { get; private set; }

        public Uri Host { get; private set; }

        public string GameName { get; private set; }

        public string Name { get; private set; }

        public IUser UserHost { get; private set; }

        public GameStatus Status { get; private set; }

        public DateTime StartTime { get; private set; }

        public bool PasswordProtected { get; private set; }

        public string Password { get; private set; }

        public HostedGameProcess(Guid id, Uri host, Guid gameId, Version version, 
            string gameName, string name, string password, IUser hoster)
        {
            Host = host;
            GameId = gameId;
            Version = version;
            Name = name;
            GameName = gameName;
            Password = password;
            UserHost = hoster;
            Id = id;
        }

        public HostedGameProcess(IHostedGame game)
        {
            Host = game.Host;
            GameId = game.GameId;
            Version = game.Version;
            Name = game.Name;
            GameName = game.GameName;
            Password = game.Password;
            UserHost = game.UserHost;
            Id = game.Id;
        }

        public bool Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void SetStatus(GameStatus status)
        {
            Status = status;
        }
    }
}
