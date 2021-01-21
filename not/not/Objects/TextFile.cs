using System.IO;
using System.Xml.Serialization;

namespace not.Objects
{
    public class TextFile
    {
        [XmlAttribute(AttributeName = "FileName")]
        /// <summary>
        /// Chemain d'accès et nom du fichier.
        /// </summary>
        public string FileName { get; set; }

        [XmlAttribute(AttributeName = "BackupFileName")]
        /// <summary> 
        /// Chemain d'accès et nom du fichier Backup. 
        /// </summary>
        public string BackupFileName { get; set; } = string.Empty;

        [XmlIgnore()]
        /// <summary> 
        /// Nom et extension du fichier. Le nom du fichier sans chemain d'accès. 
        /// </summary>
        public string SafeFileName { get; set; }

        [XmlIgnore()]
        /// <summary> 
        /// Nom et extension du fichier Backup. Le nom du fichier sans chemain d'accès. 
        /// </summary>
        public string SafeBackupFileName { get; set; }

        [XmlIgnore()]
        /// <summary> 
        /// Contenu du fichier. 
        /// </summary>
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// Constructeur de la classe TextFile.
        /// </summary>
        public TextFile()
        {

        }

        /// <summary>
        /// Constructeur de la classe TextFile.
        /// </summary>
        /// <param name="filename">Chemain d'acces et nom du fichier</param>
        public TextFile(string fileName)
        {
            FileName = fileName;
            SafeFileName = Path.GetFileName(fileName);
        }
    }
}
