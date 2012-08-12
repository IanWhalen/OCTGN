using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Polenter.Serialization;
using Polenter.Serialization.Advanced;
using Polenter.Serialization.Advanced.Deserializing;
using Polenter.Serialization.Advanced.Serializing;

namespace Octgn.Data
{
    public class SharpSerializerWrapper
    {
        private SharpSerializer _serializer;

        #region SharpSerializer

        /// <summary>
        ///   Default it is an instance of PropertyProvider. It provides all properties to serialize.
        ///   You can use an Ihneritor and overwrite its GetAllProperties and IgnoreProperty methods to implement your custom rules.
        /// </summary>
        public PropertyProvider PropertyProvider
        {
            get { return _serializer.PropertyProvider; }
            set { _serializer.PropertyProvider = value; }
        }

        /// <summary>
        ///   What name should have the root property. Default is "Root".
        /// </summary>
        public string RootName
        {
            get { return _serializer.RootName; }
            set { _serializer.RootName = value; }
        }

        /// <summary>
        ///   Standard Constructor. Default is Xml serializing
        /// </summary>
        public SharpSerializerWrapper()
        {
            _serializer = new SharpSerializer();
        }

        /// <summary>
        ///   Overloaded constructor
        /// </summary>
        /// <param name = "binarySerialization">true - binary serialization with SizeOptimized mode, false - xml. For more options use other overloaded constructors.</param>
        public SharpSerializerWrapper(bool binarySerialization)
        {
            _serializer = new SharpSerializer(binarySerialization);
        }

        /// <summary>
        ///   Xml serialization with custom settings
        /// </summary>
        /// <param name = "settings"></param>
        public SharpSerializerWrapper(SharpSerializerXmlSettings settings)
        {
            _serializer = new SharpSerializer(settings);
        }

        /// <summary>
        ///   Binary serialization with custom settings
        /// </summary>
        /// <param name = "settings"></param>
        public SharpSerializerWrapper(SharpSerializerBinarySettings settings)
        {
            _serializer = new SharpSerializer(settings);
        }

        /// <summary>
        ///   Custom serializer and deserializer
        /// </summary>
        /// <param name = "serializer"></param>
        /// <param name = "deserializer"></param>
        public SharpSerializerWrapper(IPropertySerializer serializer, IPropertyDeserializer deserializer)
        {
            _serializer = new SharpSerializer(serializer, deserializer);
        }

        /// <summary>
        ///   Serializing to a file. File will be always new created and closed after the serialization.
        /// </summary>
        /// <param name = "data"></param>
        /// <param name = "filename"></param>
        public void Serialize(object data, string filename)
        {
            _serializer.Serialize(data,filename);
        }

        /// <summary>
        ///   Serializing to the stream. After serialization the stream will NOT be closed.
        /// </summary>
        /// <param name = "data"></param>
        /// <param name = "stream"></param>
        public void Serialize(object data, Stream stream)
        {
            _serializer.Serialize(data,stream);
        }

        /// <summary>
        ///   Deserializing from the file. After deserialization the file will be closed.
        /// </summary>
        /// <param name = "filename"></param>
        /// <returns></returns>
        public virtual object Deserialize(string filename)
        {
            return _serializer.Deserialize(filename);
        }

        /// <summary>
        ///   Deserialization from the stream. After deserialization the stream will NOT be closed.
        /// </summary>
        /// <param name = "stream"></param>
        /// <returns></returns>
        public object Deserialize(Stream stream)
        {
            return _serializer.Deserialize(stream);
        }

        #endregion
    }
}
