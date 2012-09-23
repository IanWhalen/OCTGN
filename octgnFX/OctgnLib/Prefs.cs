// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Prefs.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Defines the Prefs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn
{
    using System;
    using System.Globalization;

    using Octgn.Data;

    /// <summary>
    /// The static Preferences class.
    /// </summary>
    public static class Prefs
    {
        /// <summary>
        /// Hide login notifications.
        /// </summary>
        private static string hideLoginNotifications;

        /// <summary>
        /// Initializes static members of the <see cref="Prefs"/> class.
        /// </summary>
        static Prefs()
        {
            var hln = SimpleConfig.ReadValue("Options_HideLoginNotifications");
            hideLoginNotifications = hln == null || hln == "false" ? "false" : "true";
        }

        /// <summary>
        /// Gets or sets a value indicating whether to install on boot.
        /// </summary>
        public static bool InstallOnBoot
        {
            get
            {
                var ret = true;
                bool.TryParse(SimpleConfig.ReadValue("InstallOnBoot", true.ToString(CultureInfo.InvariantCulture)), out ret);
                return ret;
            }

            set
            {
                SimpleConfig.WriteValue("InstallOnBoot", value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to clean the database.
        /// </summary>
        public static bool CleanDatabase
        {
            get
            {
                var ret = false;
                var present = bool.TryParse(SimpleConfig.ReadValue("CleanDatabase", true.ToString(CultureInfo.InvariantCulture)), out ret);
                if (!present)
                {
                    ret = true;
                }

                return ret;
            }

            set
            {
                SimpleConfig.WriteValue("CleanDatabase", value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets or sets the users password.
        /// </summary>
        public static string Password
        {
            get
            {
                return SimpleConfig.ReadValue("Password", string.Empty);
            }

            set
            {
                SimpleConfig.WriteValue("Password", value);
            }
        }

        /// <summary>
        /// Gets or sets the users username.
        /// </summary>
        public static string Username
        {
            get
            {
                return SimpleConfig.ReadValue("Username", string.Empty);
            }

            set
            {
                SimpleConfig.WriteValue("Username", value);
            }
        }

        /// <summary>
        /// Gets or sets the users nickname.
        /// </summary>
        public static string Nickname
        {
            get
            {
                return SimpleConfig.ReadValue("Nickname", "null");
            }

            set
            {
                SimpleConfig.WriteValue("Nickname", value);
            }
        }

        /// <summary>
        /// Gets or sets the last folder.
        /// </summary>
        public static string LastFolder
        {
            get
            {
                return SimpleConfig.ReadValue("lastFolder", string.Empty);
            }

            set
            {
                SimpleConfig.WriteValue("lastFolder", value);
            }
        }

        /// <summary>
        /// Gets or sets whether we should hide login notifications.
        /// </summary>
        public static string HideLoginNotifications
        {
            get
            {
                return hideLoginNotifications;
            }

            set
            {
                hideLoginNotifications = value;
                SimpleConfig.WriteValue("Options_HideLoginNotifications", value);
            }
        }

        /// <summary>
        /// Gets or sets the data directory.
        /// </summary>
        public static string DataDirectory
        {
            get { return SimpleConfig.DataDirectory; }
            set { SimpleConfig.DataDirectory = value; }
        }

        /// <summary>
        /// Gets or sets the last room name.
        /// </summary>
        public static string LastRoomName
        {
            get
            {
                return SimpleConfig.ReadValue("lastroomname", Skylabs.Lobby.Randomness.RandomRoomName());
            }

            set
            {
                SimpleConfig.WriteValue("lastroomname", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to default to a two sided table.
        /// </summary>
        public static bool TwoSidedTable
        {
            get
            {
                var val = true;
                if (
                    !bool.TryParse(
                        SimpleConfig.ReadValue("twosidedtable", true.ToString(CultureInfo.InvariantCulture)), out val))
                {
                    SimpleConfig.WriteValue("twosidedtable", true.ToString(CultureInfo.InvariantCulture));
                }

                return val;
            }

            set
            {
                SimpleConfig.WriteValue("twosidedtable", value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets whether we should filter a game.
        /// </summary>
        /// <param name="name">
        /// The name of the game.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetFilterGame(string name)
        {
            var ret = true;
            if (!bool.TryParse(SimpleConfig.ReadValue("FilterGames_" + name, "true"), out ret))
            {
                ret = true;
                SimpleConfig.WriteValue("FilterGames_" + name , true.ToString(CultureInfo.InvariantCulture));
            }

            return ret;
        }

        /// <summary>
        /// Sets whether we should filter a game
        /// </summary>
        /// <param name="name">
        /// The name of the game.
        /// </param>
        /// <param name="value">
        /// The value
        /// </param>
        public static void SetFilterGame(string name, bool value)
        {
            SimpleConfig.WriteValue("FilterGames_" + name, value.ToString(CultureInfo.InvariantCulture));
        }
    }
}