namespace Skylabs.Lobby
{
    /// <summary>
    /// The register results.
    /// </summary>
    public enum RegisterResults
    {
        /// <summary>
        /// The connection error.
        /// </summary>
        ConnectionError,

        /// <summary>
        /// The success.
        /// </summary>
        Success,

        /// <summary>
        /// The username taken.
        /// </summary>
        UsernameTaken,

        /// <summary>
        /// The username invalid.
        /// </summary>
        UsernameInvalid,

        /// <summary>
        /// The password failure.
        /// </summary>
        PasswordFailure
    }

    /// <summary>
    /// The login results.
    /// </summary>
    public enum LoginResults
    {
        /// <summary>
        /// The connection error.
        /// </summary>
        ConnectionError,

        /// <summary>
        /// The success.
        /// </summary>
        Success,

        /// <summary>
        /// The failure.
        /// </summary>
        Failure,

        /// <summary>
        /// The firewall error.
        /// </summary>
        FirewallError,

        /// <summary>
        /// Authorization Error.
        /// </summary>
        AuthError
    }

    /// <summary>
    /// The data receive type.
    /// </summary>
    public enum DataRecType
    {
        /// <summary>
        /// The friend list.
        /// </summary>
        FriendList,

        /// <summary>
        /// The my info.
        /// </summary>
        MyInfo,

        /// <summary>
        /// The game list.
        /// </summary>
        GameList,

        /// <summary>
        /// The hosted game ready.
        /// </summary>
        HostedGameReady,

        /// <summary>
        /// The games need refresh.
        /// </summary>
        GamesNeedRefresh,

        /// <summary>
        /// The announcement.
        /// </summary>
        Announcement
    }

    /// <summary>
    /// The login result.
    /// </summary>
    public enum LoginResult
    {
        /// <summary>
        /// The success.
        /// </summary>
        Success,

        /// <summary>
        /// The failure.
        /// </summary>
        Failure,

        /// <summary>
        /// The banned.
        /// </summary>
        Banned,

        /// <summary>
        /// The waiting for response.
        /// </summary>
        WaitingForResponse
    }

    /// <summary>
    /// The lobby message type.
    /// </summary>
    public enum LobbyMessageType
    {
        /// <summary>
        /// The standard.
        /// </summary>
        Standard,

        /// <summary>
        /// The error.
        /// </summary>
        Error,

        /// <summary>
        /// The topic.
        /// </summary>
        Topic
    }

}
