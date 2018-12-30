using System.IO;
using System.Xml.Serialization;
using AutoUpdate.Abstraction.Configurations;

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
            using (var writer = new StringWriter())
            {
                _serializer.Serialize(writer, configuration);
                return writer.GetStringBuilder().ToString();
            }
        }

        public ExecutorConfiguration Deserialize(string configuration)
        {
            using (var reader = new StringReader(configuration))
            {
                return _serializer.Deserialize(reader) as ExecutorConfiguration;
            }
        }
    }
}
