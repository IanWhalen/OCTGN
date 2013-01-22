namespace Skylabs.Lobby.Library.Models
{
    using System;

    public class HostedGame : IHostedGame
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

        public HostedGame(IHostedGame game, bool includePassword = true)
        {
            Id = game.Id;
            GameId = game.GameId;
            Version = game.Version;
            Host = game.Host;
            GameName = game.GameName;
            Name = game.Name;
            UserHost = game.UserHost;
            Status = game.Status;
            StartTime = game.StartTime;
            PasswordProtected = game.PasswordProtected;
            Password = includePassword ? game.Password : string.Empty;
        }

        /// <summary>
        /// Constructor used for sending data to the server for starting a game.
        /// </summary>
        /// <param name="userHost">User initiating the request.</param>
        /// <param name="gameId">Id of the game"/></param>
        /// <param name="gameVersion">Version of the game</param>
        /// <param name="gameName">Name of the game</param>
        /// <param name="name">Name of the game room</param>
        /// <param name="password">Password for the room. If null or empty, than no password required</param>
        public HostedGame(IUser userHost, Guid gameId, Version gameVersion, string gameName, string name, string password = null)
        {
            // Host not set
            UserHost = userHost;
            GameId = gameId;
            Version = gameVersion;
            GameName = gameName;
            Name = name;
            Password = password;
            if (!String.IsNullOrWhiteSpace(password)) PasswordProtected = true;
            Status = GameStatus.NotStarted;
            StartTime = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
    }
}
