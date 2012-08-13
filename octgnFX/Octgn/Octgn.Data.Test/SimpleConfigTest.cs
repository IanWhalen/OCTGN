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
    using System.Collections;
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
        /// Test the constructors
        /// </summary>
        [Fact]
        public void Constructors()
        {
            var conf = new SimpleConfig();
            Assert.Throws<InvalidOperationException>(() => conf.Get("name", "default"));
            Assert.Throws<InvalidOperationException>(() => conf.Set("name", "default"));
            Assert.NotNull(conf.FileSystem);
            Assert.NotNull(conf.SharpSerializer);
            Assert.NotNull(conf.BaseDirectory);
            Assert.Throws<InvalidOperationException>(() => conf.DataDirectory);
            Assert.NotNull(conf.SettingsPath);
            Assert.False(conf.IsOpen);

        }

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
            sconf = new SimpleConfig { FileSystem = mockFileSystem };
            Assert.Equal(settingsPath, sconf.SettingsPath);
            Assert.True(mockFileSystem.Directory.Exists(settingsDirPath));
        }

        /// <summary>
        /// The read/write value Unit Test
        /// </summary>
        [Fact]
        public void ReadValue()
        {
            var hashWithKey = new Hashtable { { "testKey", "value" } };
            var hashWithoutKey = new Hashtable();

            var simpleConfig = new SimpleConfig
                {
                    SharpSerializer = A.Fake<MockableSharpSerializer>(), 
                    FileSystem = new MockFileSystem(new Dictionary<string, MockFileData>())
                };

            // If Settings file exists && key exists.
            ((MockFileSystem)simpleConfig.FileSystem).AddFile(
                simpleConfig.SettingsPath, new MockFileData("not important"));
            simpleConfig.SharpSerializer.CallsTo(x => x.Deserialize(simpleConfig.SettingsPath)).Returns(hashWithKey);
            Assert.Equal((string)simpleConfig.Get("testKey", "taco"), "value");

            // Settings file exists && key doesn't.
            simpleConfig.SharpSerializer.CallsTo(x => x.Deserialize(simpleConfig.SettingsPath)).Returns(hashWithoutKey);
            Assert.Equal((string)simpleConfig.Get("testKey", "taco"), "taco");
            
            // Settings file doesn't exist
            ((MockFileSystem)simpleConfig.FileSystem).RemoveFile(simpleConfig.SettingsPath);
            Assert.Equal((string)simpleConfig.Get("testKey2", "taco"), "taco");

            // On Exception
            simpleConfig.SharpSerializer.CallsTo(x => x.Deserialize(simpleConfig.SettingsPath)).Throws(new Exception());
            Assert.Equal((string)simpleConfig.Get("testKey2", "taco"), "taco");
        }

        [Fact]
        public void WriteValue()
        {
            var hashtableProto = new Hashtable { { "testKey", "value" } };
            var hashtable = new Hashtable { { "testKey", "value" } };
            
            var simpleConfig = new SimpleConfig
                {
                    SharpSerializer = A.Fake<MockableSharpSerializer>(), 
                    FileSystem = new MockFileSystem(new Dictionary<string, MockFileData>())
                };

            simpleConfig.SharpSerializer.CallsTo(x => x.Serialize(A<object>.Ignored, simpleConfig.SettingsPath)).DoesNothing();

            // If Settings file exists && file not corrupt && write worked.
            ((MockFileSystem)simpleConfig.FileSystem).AddFile(
                simpleConfig.SettingsPath, new MockFileData("not important"));
            simpleConfig.SharpSerializer.CallsTo(x => x.Deserialize(simpleConfig.SettingsPath)).Returns(hashtable);
            Assert.DoesNotThrow(() => simpleConfig.Set("testKey2", "taco"));
            Assert.True(hashtable.ContainsKey("testKey2"));

            // If Settings file exists && file not corrupt && write failed
            hashtable = (Hashtable)hashtableProto.Clone();
            simpleConfig.SharpSerializer.CallsTo(x => x.Serialize(A<object>.Ignored, simpleConfig.SettingsPath)).Throws(new Exception());
            Assert.DoesNotThrow(() => simpleConfig.Set("testKey2", "taco"));
            
            // If Settings file exists && file corrupt && write worked
            hashtable = (Hashtable)hashtableProto.Clone();
            simpleConfig.SharpSerializer.CallsTo(x => x.Serialize(A<object>.Ignored, simpleConfig.SettingsPath)).DoesNothing();
            simpleConfig.SharpSerializer.CallsTo(x => x.Deserialize(simpleConfig.SettingsPath)).Throws(new Exception());
            Assert.DoesNotThrow(() => simpleConfig.Set("testKey2", "taco"));

            Assert.False(hashtable.ContainsKey("testKey2"));

            // Settings file doesn't exist
            ((MockFileSystem)simpleConfig.FileSystem).RemoveFile(simpleConfig.SettingsPath);
            Assert.Equal((string)simpleConfig.Get("testKey2", "taco"), "taco");
            
        }
    }
}
