using System.Xml;
using System.Xml.Serialization;

namespace ToDoApp.Data
{
    public class XmlContext
    {
        private readonly IConfiguration _configuration;
        public XmlDocument Document { get; }
        private string _connectionString;

        public XmlContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("XmlConnection")!;

            Document = new XmlDocument();

            if (File.Exists(_connectionString))
            {
                Document.Load(_connectionString);
            }
            else
            {
                Document = InitXmlDocument();
            }
        }

        private static XmlDocument InitXmlDocument()
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlElement root = xmlDocument.CreateElement("data");
            xmlDocument.AppendChild(root);

            XmlElement lastId = xmlDocument.CreateElement("LastId");

            XmlElement note = xmlDocument.CreateElement("Note");
            note.InnerText = "0";
            lastId.AppendChild(note);

            XmlElement category = xmlDocument.CreateElement("Category");
            category.InnerText = "0";
            lastId.AppendChild(category);

            root.AppendChild(lastId);
            root.AppendChild(xmlDocument.CreateElement("Notes"));
            root.AppendChild(xmlDocument.CreateElement("Categories"));

            return xmlDocument;
        }

        public void Save()
        {
            Document.Save(_connectionString);
        }

        public XmlElement? Serialize(object obj)
        {
            XmlDocument doc = new XmlDocument();

            using (XmlWriter writer = doc.CreateNavigator()!.AppendChild())
            {
                new XmlSerializer(obj.GetType()).Serialize(writer, obj);
            }

            doc.DocumentElement!.RemoveAllAttributes();
            return doc.DocumentElement;
        }

        public T? Deserialize<T>(XmlNode node)
        {
            string data = node.OuterXml;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(data))
            {
                object? obj = serializer.Deserialize(reader);

                return (obj is T) ? (T)obj : default;
            }
        }
    }
}