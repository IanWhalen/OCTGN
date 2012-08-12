using System;
using System.Globalization;
using System.IO;
using System.Windows;
using Octgn.Data;
namespace Octgn
{
    public static class Prefs
    {

        static Prefs()
        {
        }

        public static bool InstallOnBoot
        {
            get
            {
                return (bool)new SimpleConfig().ReadValue("InstallOnBoot", true);
            }
            set
            {
                new SimpleConfig().WriteValue("InstallOnBoot", value);
            }
        }

        public static bool CleanDatabase
        {
            get
            {
                return (bool)new SimpleConfig().ReadValue("CleanDatabase", true);
            }
            set
            {
                new SimpleConfig().WriteValue("CleanDatabase", value);
            }
        }

        public static string Password
        {
            get { return (string)new SimpleConfig().ReadValue("Password" , ""); }
            set
            {
                new SimpleConfig().WriteValue("Password",value);
            }
        }

        public static string Username
        {
            get { return (string)new SimpleConfig().ReadValue("Username", ""); }
            set
            {
                new SimpleConfig().WriteValue("Username", value);
            }
        }        

        public static bool getFilterGame(string name)
        {
            return (bool)new SimpleConfig().ReadValue("FilterGames_" + name, true);
        }
        
        public static void setFilterGame(string name, bool value)
        {
            new SimpleConfig().WriteValue("FilterGames_" + name, value);
        }

        public static string Nickname
        {
            get { return (string)new SimpleConfig().ReadValue("Nickname" , null); }
            set
            {
                new SimpleConfig().WriteValue("Nickname",value);
            }
        }

        public static string LastFolder
        {
            get
            {
                return (string)new SimpleConfig().ReadValue("lastFolder" ,"");

            }
            set
            {
                new SimpleConfig().WriteValue("lastFolder",value);
            }
        }

        public static bool HideLoginNotifications
        {
            get { return (bool)new SimpleConfig().ReadValue("Options_HideLoginNotifications",false); }
            set
            {
                new SimpleConfig().WriteValue("Options_HideLoginNotifications", value);
            }
        }
        
        public static string DataDirectory
        {
            get { return new SimpleConfig().DataDirectory; }
            set { new SimpleConfig().DataDirectory = value; }
        }

        public static string LastRoomName
        {
            get { return (string)new SimpleConfig().ReadValue("lastroomname" , Skylabs.Lobby.Randomness.RandomRoomName()); }
            set
            {
                new SimpleConfig().WriteValue("lastroomname",value);
            }
        }
        
        public static bool TwoSidedTable
        {
            get
            {
                return (bool) new SimpleConfig().ReadValue("twosidedtable", true);
            }
            set
            {
                new SimpleConfig().WriteValue("twosidedtable",value.ToString(CultureInfo.InvariantCulture));
            }
        }

        public static Point LoginLocation
        {
            get
            {
                var y = (double)new SimpleConfig().ReadValue("LoginTopLoc", 100);
                var x = (double) new SimpleConfig().ReadValue("LoginLeftLoc", 100);
                return new Point(x, y);
            }
            set
            {
                new SimpleConfig().WriteValue("LoginTopLoc", value);
                new SimpleConfig().WriteValue("LoginLeftLoc", value);
            }
        }

        public static Point MainLocation
        {
            get
            {
                var y = (double)new SimpleConfig().ReadValue("MainTopLoc", 100);
                var x = (double)new SimpleConfig().ReadValue("MainLeftLoc", 100);
                return new Point(x, y);
            }
            set
            {
                new SimpleConfig().WriteValue("MainTopLoc", value);
                new SimpleConfig().WriteValue("MainLeftLoc", value);
            }
        } 
    }
}