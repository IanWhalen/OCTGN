// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockSharpSerializer.cs" company="OCTGN">
//   Non-Profit Open Software License 3.0 (NPOSL-3.0)
// </copyright>
// <summary>
//   Defines the MockSharpSerializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Data
{
    using System.IO;

    using Polenter.Serialization;
    using Polenter.Serialization.Advanced;
    using Polenter.Serialization.Advanced.Deserializing;
    using Polenter.Serialization.Advanced.Serializing;

    /// <summary>
    /// The mock sharp serializer.
    /// </summary>
    public class MockableSharpSerializer 
    {
        /// <summary>
        /// The real SharpSerializer.
        /// </summary>
        private readonly SharpSerializer serializer;

        #region SharpSerializer

        /// <summary>
        ///   Default it is an instance of PropertyProvider. It provides all properties to serialize.
        ///   You can use an Ihneritor and overwrite its GetAllProperties and IgnoreProperty methods to implement your custom rules.
        /// </summary>
        public virtual PropertyProvider PropertyProvider
        {
            get { return this.serializer.PropertyProvider; }
            set { this.serializer.PropertyProvider = value; }
        }

        /// <summary>
        ///   What name should have the root property. Default is "Root".
        /// </summary>
        public virtual string RootName
        {
            get { return this.serializer.RootName; }
            set { this.serializer.RootName = value; }
        }

        /// <summary>
        ///   Standard Constructor. Default is Xml serializing
        /// </summary>
        public MockableSharpSerializer()
        {
            this.serializer = new SharpSerializer();
        }

        /// <summary>
        ///   Overloaded constructor
        /// </summary>
        /// <param name = "binarySerialization">true - binary serialization with SizeOptimized mode, false - xml. For more options use other overloaded constructors.</param>
        public MockableSharpSerializer(bool binarySerialization)
        {
            this.serializer = new SharpSerializer(binarySerialization);
        }

        /// <summary>
        ///   Xml serialization with custom settings
        /// </summary>
        /// <param name = "settings"></param>
        public MockableSharpSerializer(SharpSerializerXmlSettings settings)
        {
            this.serializer = new SharpSerializer(settings);
        }

        /// <summary>
        ///   Binary serialization with custom settings
        /// </summary>
        /// <param name = "settings"></param>
        public MockableSharpSerializer(SharpSerializerBinarySettings settings)
        {
            this.serializer = new SharpSerializer(settings);
        }

        /// <summary>
        ///   Custom serializer and deserializer
        /// </summary>
        /// <param name = "serializer"></param>
        /// <param name = "deserializer"></param>
        public MockableSharpSerializer(IPropertySerializer serializer, IPropertyDeserializer deserializer)
        {
            this.serializer = new SharpSerializer(serializer, deserializer);
        }

        /// <summary>
        ///   Serializing to a file. File will be always new created and closed after the serialization.
        /// </summary>
        /// <param name = "data"></param>
        /// <param name = "filename"></param>
        public virtual void Serialize(object data, string filename)
        {
            this.serializer.Serialize(data,filename);
        }

        /// <summary>
        ///   Serializing to the stream. After serialization the stream will NOT be closed.
        /// </summary>
        /// <param name = "data"></param>
        /// <param name = "stream"></param>
        public virtual void Serialize(object data, Stream stream)
        {
            this.serializer.Serialize(data,stream);
        }

        /// <summary>
        ///   Deserializing from the file. After deserialization the file will be closed.
        /// </summary>
        /// <param name = "filename"></param>
        /// <returns></returns>
        public virtual object Deserialize(string filename)
        {
            return this.serializer.Deserialize(filename);
        }

        /// <summary>
        ///   Deserialization from the stream. After deserialization the stream will NOT be closed.
        /// </summary>
        /// <param name = "stream"></param>
        /// <returns></returns>
        public virtual object Deserialize(Stream stream)
        {
            return this.serializer.Deserialize(stream);
        }

        #endregion
    }
}
