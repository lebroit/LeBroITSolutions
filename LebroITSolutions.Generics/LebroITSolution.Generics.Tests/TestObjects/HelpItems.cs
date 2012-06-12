using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using LebroITSolution.Generics.Serialization;

namespace LebroITSolution.Generics.Tests.TestObjects
{
    [Serializable]
    public class HelpItems : ObservableCollection<HelpItem>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        #endregion

        #region Load Methodes

        /// <summary>
        /// Loads the help file, overload with filenam parameter
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public HelpItems LoadHelpFileFromXml(string fileName)
        {
            FileName = fileName;
            return LoadHelpFile();
        }

        /// <summary>
        /// Loads the help file MetaDate to retain in this class
        /// </summary>
        /// <returns></returns>
        public HelpItems LoadHelpFile()
        {

            if (string.IsNullOrEmpty(FileName)) return null;
            ReplaceCsvExtension(FileName);
            try
            {
                using (var reader = new StreamReader(FileName))
                {
                    var xml = reader.ReadToEnd();
                    var temp = new HelpItems();
                    foreach (var helpItem in (new XMLSerializerHelper<HelpItems>().Deserialize(xml)))
                    {
                        temp.Add(helpItem);
                    }
                    return temp;
                }
            }
            catch (IOException ioException)
            {
                var melding = string.Format(@"De XML file kon niet gelezen worden uit het pad: {0}\{1}", Environment.CurrentDirectory, FileName);
                throw new IOException(melding, ioException);
            }
            catch (Exception ex)
            {
                var melding = string.Format(@"De XML file kon niet gelezen worden uit het pad: {0}\{1} door een onbekende fout",
                                            Environment.CurrentDirectory, FileName);
                throw new Exception(melding, ex);
            }
        }

        #endregion

        #region Save Methode

        public bool SaveHelpMetaDataItems(string fileName)
        {
            //there aren't any entries so return false
            if (Count <= 0) return false;
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName", @"de File kon niet bewaard worden!");
            try
            {
                ReplaceCsvExtension(fileName);

                var xml = new XMLSerializerHelper<HelpItems>().SerializeWithOutNameSpace(this);
                using (var writer = new StreamWriter(FileName))
                {
                    writer.Write(xml);
                    return true;
                }
            }
            catch (IOException ioException)
            {
                var melding = string.Format(@"De XML file kon niet bewaard worden in het pad: {0}", FileName);
                throw new IOException(melding, ioException);
            }
        }

        #endregion

        #region Helpers

        private void ReplaceCsvExtension(string fileName)
        {
            //if the extension is still csv replace it with xml
            FileName = fileName.EndsWith("csv") ? fileName.Replace("csv", "xml") : fileName;
        }

        #endregion
    }

    public class HelpItem
    {
        /// <summary>
        /// Gets or sets the help topic id.
        /// Used to fine a specific help page by ID
        /// </summary>
        /// <value>
        /// The help topic id.
        /// </value>
        [XmlElement("HelpTopicId", typeof(string))]
        public string HelpTopicId { get; set; }

        /// <summary>
        /// Gets or sets the help topic.
        /// remark Helptopic is the same as the HTML page of the CHM file
        /// </summary>
        /// <value>
        /// The help topic.
        /// </value>
        [XmlElement("HelpTopic", typeof(string))]
        public string HelpTopic { get; set; }

        /// <summary>
        /// Gets or sets the help description.
        /// Used to give extra information during the search
        /// </summary>
        /// <value>
        /// The help description.
        /// </value>
        [XmlElement("HelpDescription", typeof(string))]
        public string HelpDescription { get; set; }

        /// <summary>
        /// Gets or sets the helpversion number.
        /// </summary>
        /// <value>
        /// The help versie.
        /// </value>
        [XmlElement("HelpVersie", typeof(string))]
        public string HelpVersie { get; set; }

        /// <summary>
        /// Gets or sets the in source name.
        /// </summary>
        /// <value>
        /// The in source name for the help.
        /// </value>
        [XmlElement("HelpInSource", typeof(string))]
        public string HelpInSource { get; set; }
    }
}
