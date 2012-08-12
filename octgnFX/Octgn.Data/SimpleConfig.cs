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
    using System.IO.Abstractions;

    /// <summary>
    /// The simple config.
    /// </summary>
    public  class SimpleConfig
    {
        /// <summary>
        /// File System object.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConfig"/> class.
        /// </summary>
        public SimpleConfig()
            : this(new FileSystem())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConfig"/> class.
        /// </summary>
        /// <param name="fileSystem">
        /// The file system.
        /// </param>
        public SimpleConfig(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

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
                return (string)this.ReadValue("datadirectory", System.IO.Path.Combine(this.BaseDirectory, "Octgn"));
            }

            set
            {
                this.WriteValue("datadirectory", value);
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

                if (!this.fileSystem.Directory.Exists(p))
                {
                    this.fileSystem.Directory.CreateDirectory(p);
                }

                return fullPath;                
            }
        }

        /// <summary>
        ///   Reads a string value from the Octgn registry
        /// </summary>
        /// <param name="valName"> The name of the value </param>
        /// <param name="d">Default value if value isn't in settings file</param>
        /// <returns> A string value </returns>
        public object ReadValue(string valName, object d)
        {
            try
            {
                if (this.fileSystem.File.Exists(this.SettingsPath))
                {
                    var serializer = new SharpSerializerWrapper();
                    var config = (Hashtable)serializer.Deserialize(this.SettingsPath);
                    if (config.ContainsKey(valName))
                    {
                        return config[valName];
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("[SimpleConfig]ReadValue Error: " + e.Message);
            }

            return d;
        }

        /// <summary>
        ///   Writes a string value to the Octgn registry
        /// </summary>
        /// <param name="valName"> Name of the value </param>
        /// <param name="value"> String to write for value </param>
        public void WriteValue(string valName, object value)
        {
            try
            {
                var serializer = new SharpSerializerWrapper();
                var config = new Hashtable();
                if (this.fileSystem.File.Exists(this.SettingsPath))
                {
                    config = (Hashtable)serializer.Deserialize(this.SettingsPath);
                }

                config[valName] = value;
                serializer.Serialize(config, this.SettingsPath);
            }
            catch (Exception e)
            {
                Trace.WriteLine("[SimpleConfig]WriteValue Error: " + e.Message);
            }
        }
    }
}