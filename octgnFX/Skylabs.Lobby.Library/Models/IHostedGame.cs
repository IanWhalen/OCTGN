namespace Skylabs.Lobby.Library.Models
{
    using System;

    /// <summary>
    /// Hosted Game
    /// </summary>
    public interface IHostedGame
    {
        /// <summary>
        /// Unique Id of the hosted game.
        /// </summary>
        Guid Id { get;}
        /// <summary>
        /// Id of the game being played
        /// </summary>
        Guid GameId { get; }
        /// <summary>
        /// Version of the game being played
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// Host Uri of the game
        /// </summary>
        Uri Host { get; }
        /// <summary>
        /// Name of the game reported by the client
        /// </summary>
        string GameName { get; }
        /// <summary>
        /// Name of the hosted game, set by the user
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Host of the game
        /// </summary>
        IUser UserHost { get; }
        /// <summary>
        /// Game status
        /// </summary>
        GameStatus Status { get; }
        /// <summary>
        /// Time the game was started
        /// </summary>
        DateTime StartTime { get; }
        /// <summary>
        /// Is this game password protected?
        /// </summary>
        bool PasswordProtected { get; }
        /// <summary>
        /// Password for the game
        /// </summary>
        string Password { get; }
    }
}
