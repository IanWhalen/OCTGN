namespace Skylabs.Lobby.Library.Models
{
    /// <summary>
    /// User
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Username of a user
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// Full Username of a user. Will include username@server
        /// </summary>
        string FullUserName { get; }
        /// <summary>
        /// Server name the user is attached to
        /// </summary>
        string Server { get; }
        /// <summary>
        /// The users status
        /// </summary>
        UserStatus Status { get; }
        /// <summary>
        /// The users custom status
        /// </summary>
        string CustomStatus { get; }
        /// <summary>
        /// The users email
        /// </summary>
        string Email { get; }
    }
}