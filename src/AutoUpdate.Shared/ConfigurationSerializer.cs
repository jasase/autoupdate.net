using System.IO;
using System.Xml.Serialization;
using AutoUpdate.Shared.Configurations;

namespace AutoUpdate.Shared
{
    public class ConfigurationSerializer
    {
        private readonly XmlSerializer _serializer;

        public ConfigurationSerializer()
        {
            _serializer = new XmlSerializer(typeof(ExecutorConfiguration));
        }

        public string Serialize(ExecutorConfiguration configuration)
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                _serializer.Serialize(stream, configuration);
                stream.Position = 0;

                return reader.ReadToEnd();
            }
        }

        public ExecutorConfiguration Deserialize(string configuration)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(configuration);
                stream.Position = 0;

                return _serializer.Deserialize(stream) as ExecutorConfiguration;
            }
        }
    }
}
