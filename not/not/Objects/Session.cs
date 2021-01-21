using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace not.Objects
{
    public class Session
    {
        private const string FILENAME = "session.xml";

        private static string _applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string _applicationPath = Path.Combine(_applicationDataPath, "M & Co.dotNET");

        private readonly XmlWriterSettings _writerSettings;

        /// <summary>
        /// Chemain d'acces et nom du fichier de la session.
        /// </summary>
        public string FileName { get; set; } = Path.Combine(_applicationPath, FILENAME);

        [XmlAttribute(AttributeName = "ActiveIndex")]
        public int ActiveIndex { get; set; } = 0;

        [XmlElement(ElementName = "File")]
        public List<TextFile> TextFiles { get; set; }

        public Session()
        {
            TextFiles = new List<TextFile>();

            _writerSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("\t"),
                OmitXmlDeclaration = true
            };

            if (!Directory.Exists(_applicationPath))
            {
                Directory.CreateDirectory(_applicationPath);
            }
        }

        public void Save()
        {
            var emptyNameSpace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(Session));
            using (XmlWriter writer = XmlWriter.Create(FileName, _writerSettings))
            {
                serializer.Serialize(writer, this, emptyNameSpace);
            }

        }
    }
}
