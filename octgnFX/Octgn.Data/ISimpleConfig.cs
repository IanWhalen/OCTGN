// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISimpleConfig.cs" company="OCTGN">
//   Non-Profit Open Software License 3.0 (NPOSL-3.0)
// </copyright>
// <summary>
//   The SimpleConfig Interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Octgn.Data
{
    using System.IO.Abstractions;

    /// <summary>
    /// The SimpleConfig interface.
    /// </summary>
    public interface ISimpleConfig
    {
        /// <summary>
        /// Gets or sets File System object.
        /// </summary>
        IFileSystem FileSystem { get; set; }

        /// <summary>
        /// Gets a value indicating whether the settings are open.
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Gets the base directory for all OCTGN user data.
        /// </summary>
        string BaseDirectory { get; }

        /// <summary>
        /// Gets the settings path, the path to the settings xml file.
        /// </summary>
        string SettingsPath { get; }

        /// <summary>
        /// Open the config file and load it's settings.
        /// </summary>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        bool Open();

        /// <summary>
        /// The save and close the config file.
        /// </summary>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        bool SaveAndClose();

        /// <summary>
        ///   Reads a string value from the Octgn registry
        /// </summary>
        /// <param name="valName"> The name of the value </param>
        /// <param name="d">Default value if value isn't in settings file</param>
        /// <returns> A string value </returns>
        object Get(string valName, object d);

        /// <summary>
        /// Writes a string value to the Octgn registry
        /// </summary>
        /// <param name="valName">
        /// Name of the value 
        /// </param>
        /// <param name="value">
        /// String to write for value 
        /// </param>
        void Set(string valName, object value);
    }
}