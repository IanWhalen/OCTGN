// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleConfig.cs" company="OCTGN">
//   Non-Profit Open Software License 3.0 (NPOSL-3.0)
// </copyright>
// <summary>
//   The simple config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Data
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.IO.Abstractions;

    /// <summary>
    /// The simple config.
    /// </summary>
    public class SimpleConfig : ISimpleConfig
    {
        /// <summary>
        /// The lock object.
        /// </summary>
        private readonly object lockObject = new Object();

        /// <summary>
        /// The internal settings hashtable.
        /// </summary>
        private Hashtable settings = new Hashtable();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConfig"/> class.
        /// </summary>
        public SimpleConfig()
        {
            this.FileSystem = new FileSystem();
            this.SharpSerializer = new MockableSharpSerializer();
        }

        /// <summary>
        /// Gets or sets File System object.
        /// </summary>
        public IFileSystem FileSystem { get; set; }

        /// <summary>
        /// Gets or sets the MockableSharpSerializer.
        /// </summary>
        public MockableSharpSerializer SharpSerializer { get; set; }

        /// <summary>
        /// Gets a value indicating whether the settings are open.
        /// </summary>
        public bool IsOpen { get; private set; }

        /// <summary>
        /// Gets the base directory for all OCTGN user data.
        /// </summary>
        public string BaseDirectory
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Octgn");
            }
        }

        /// <summary>
        /// Gets or sets the data directory. This is where all the db and files are stored.
        /// </summary>
        public string DataDirectory
        {
            get
            {
                return (string)this.Get("datadirectory", System.IO.Path.Combine(this.BaseDirectory, "Octgn"));
            }

            set
            {
                this.Set("datadirectory", value);
            }
        }

        /// <summary>
        /// Gets the settings path, the path to the settings xml file.
        /// </summary>
        public string SettingsPath
        {
            get
            {
                var p = System.IO.Path.Combine(this.BaseDirectory, "Config");
                var fullPath = System.IO.Path.Combine(p, "settings.xml");

                if (!this.FileSystem.Directory.Exists(p))
                {
                    this.FileSystem.Directory.CreateDirectory(p);
                }

                return fullPath;                
            }
        }

        /// <summary>
        /// Open the config file and load it's settings.
        /// </summary>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public bool Open()
        {
            lock (this.lockObject)
            {
                this.settings = new Hashtable();
                try
                {
                    using (
                        var stream = this.FileSystem.File.Open(
                            this.SettingsPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                    {
                        this.settings = (Hashtable)this.SharpSerializer.Deserialize(stream);
                    }
                }
                catch (IOException e)
                {
                    this.settings = new Hashtable();
                    return false;
                }
                catch
                {
                    this.settings = new Hashtable();
                }

                this.IsOpen = true;
                return true;
            }
        }

        /// <summary>
        /// The save and close the config file.
        /// </summary>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public bool SaveAndClose()
        {
            lock (this.lockObject)
            {
                if (!this.IsOpen)
                {
                    return false;
                }

                try
                {
                    using (var stream = this.FileSystem.File.Open(this.SettingsPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        this.SharpSerializer.Serialize(this.settings, stream);
                    }
                }
                catch (IOException e)
                {
                    return false;
                }
                catch
                {
                }
                this.IsOpen = false;
                return true;
            }
        }

        /// <summary>
        ///   Reads a string value from the Octgn registry
        /// </summary>
        /// <param name="valName"> The name of the value </param>
        /// <param name="d">Default value if value isn't in settings file</param>
        /// <returns> A string value </returns>
        public object Get(string valName, object d)
        {
            lock (this.lockObject)
            {
                if (!this.IsOpen)
                {
                    throw new InvalidOperationException("Config isn't open.");
                }
                return this.settings[valName] ?? d;
            }
        }

        /// <summary>
        /// Writes a string value to the Octgn registry
        /// </summary>
        /// <param name="valName">
        /// Name of the value 
        /// </param>
        /// <param name="value">
        /// String to write for value 
        /// </param>
        public void Set(string valName, object value)
        {
            lock (this.lockObject)
            {
                if (!this.IsOpen)
                {
                    throw new InvalidOperationException("Config isn't open.");
                }
                this.settings[valName] = value;
            }
        }
    }
}