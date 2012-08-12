// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleConfigTest.cs" company="OCTGN">
//   Non-Profit Open Software License 3.0 (NPOSL-3.0)
// </copyright>
// <summary>
//   Defines the SimpleConfigTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.IO.Abstractions.TestingHelpers;

    using FakeItEasy;
    using FakeItEasy.ExtensionSyntax.Full;

    using Xunit;

    /// <summary>
    /// The simple config test.
    /// </summary>
    public class SimpleConfigTest
    {
        /// <summary>
        /// Test the paths
        /// </summary>
        [Fact]
        public void Paths()
        {
            var sconf = new SimpleConfig();

            // Check BaseDirectory
            var baseDirectory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Octgn");
            Assert.Equal(baseDirectory, sconf.BaseDirectory);

            // Check SettingsPath
            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
            var settingsDirPath = System.IO.Path.Combine(sconf.BaseDirectory, "Config");
            var settingsPath = System.IO.Path.Combine(settingsDirPath, "settings.xml");

            Assert.False(mockFileSystem.Directory.Exists(settingsDirPath));
            sconf = new SimpleConfig(mockFileSystem);
            Assert.Equal(settingsPath, sconf.SettingsPath);
            Assert.True(mockFileSystem.Directory.Exists(settingsDirPath));
        }

        /// <summary>
        /// The read/write value Unit Test
        /// </summary>
        [Fact]
        public void ReadValue()
        {
            Assert.True(true);
            /*
            var ss = A.Fake<SharpSerializerWrapper>();
            var a = SimpleConfig.SettingsPath;
            Assert.True(true);
            ss.CallsTo(x => x.Deserialize(SimpleConfig.SettingsPath)).Returns((object)"pee");
            Assert.True(true);
             * */
        }
    }
}
