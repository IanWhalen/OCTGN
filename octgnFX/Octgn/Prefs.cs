using System;
using System.Globalization;
using System.IO;
using System.Windows;
using Octgn.Data;
namespace Octgn
{
    public static class Prefs
    {
        /// <summary>
        /// Raw Simple Config
        /// </summary>
        public static SimpleConfig Config;

        static Prefs()
        {
            Config = new SimpleConfig();
            Config.Open();
            Application.Current.Exit += (sender, args) => SaveAndClose();
        }

        /// <summary>
        /// The save and close the config file.
        /// </summary>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public static bool SaveAndClose()
        {
            return Config.SaveAndClose();
        }

        public static bool InstallOnBoot
        {
            get
            {
                return (bool)Config.Get("InstallOnBoot", true);
            }
            set
            {
                Config.Set("InstallOnBoot", value);
            }
        }

        public static bool CleanDatabase
        {
            get
            {
                return (bool)Config.Get("CleanDatabase", true);
            }
            set
            {
                Config.Set("CleanDatabase", value);
            }
        }

        public static string Password
        {
            get { return (string)Config.Get("Password" , ""); }
            set
            {
                Config.Set("Password",value);
            }
        }

        public static string Username
        {
            get { return (string)Config.Get("Username", ""); }
            set
            {
                Config.Set("Username", value);
            }
        }        

        public static bool getFilterGame(string name)
        {
            return (bool)Config.Get("FilterGames_" + name, true);
        }
        
        public static void setFilterGame(string name, bool value)
        {
            Config.Set("FilterGames_" + name, value);
        }

        public static string Nickname
        {
            get { return (string)Config.Get("Nickname" , null); }
            set
            {
                Config.Set("Nickname",value);
            }
        }

        public static string LastFolder
        {
            get
            {
                return (string)Config.Get("lastFolder" ,"");

            }
            set
            {
                Config.Set("lastFolder",value);
            }
        }

        public static bool HideLoginNotifications
        {
            get { return (bool)Config.Get("Options_HideLoginNotifications",false); }
            set
            {
                Config.Set("Options_HideLoginNotifications", value);
            }
        }
        
        public static string DataDirectory
        {
            get { return new SimpleConfig().DataDirectory; }
            set { new SimpleConfig().DataDirectory = value; }
        }

        public static string LastRoomName
        {
            get { return (string)Config.Get("lastroomname" , Skylabs.Lobby.Randomness.RandomRoomName()); }
            set
            {
                Config.Set("lastroomname",value);
            }
        }
        
        public static bool TwoSidedTable
        {
            get
            {
                return (bool) Config.Get("twosidedtable", true);
            }
            set
            {
                Config.Set("twosidedtable",value.ToString(CultureInfo.InvariantCulture));
            }
        }

        public static Point LoginLocation
        {
            get
            {
                var y = (double)Config.Get("LoginTopLoc", 100);
                var x = (double) Config.Get("LoginLeftLoc", 100);
                return new Point(x, y);
            }
            set
            {
                Config.Set("LoginTopLoc", value);
                Config.Set("LoginLeftLoc", value);
            }
        }

        public static Point MainLocation
        {
            get
            {
                var y = (double)Config.Get("MainTopLoc", 100);
                var x = (double)Config.Get("MainLeftLoc", 100);
                return new Point(x, y);
            }
            set
            {
                Config.Set("MainTopLoc", value);
                Config.Set("MainLeftLoc", value);
            }
        } 
    }
}