using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace BusinessLogic
{
    /// <summary>
    /// De/Serialization class for object of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XMLSerializerHelper<T> where T : class
    {
        #region Serialization support
        /// <summary>
        /// XML serializer as class attribuut
        /// </summary>
        private readonly System.Xml.Serialization.XmlSerializer serializer =
                                new System.Xml.Serialization.XmlSerializer(typeof(T));

        /// <summary>
        /// Serialize object naar een XML string
        /// </summary>
        /// <param name="myobject"></param>
        /// <returns></returns>
        public String Serialize(T myobject)
        {
            try
            {
                String result = null;
                if (myobject != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var xmlWriter = new XmlTextWriter(ms, Encoding.UTF8))
                        {
                            xmlWriter.Formatting = Formatting.Indented;
                            serializer.Serialize(xmlWriter, myobject);
                            //rewind
                            ms.Seek(0, SeekOrigin.Begin);
                            using (var reader = new StreamReader(ms, Encoding.UTF8))
                            {
                                result = reader.ReadToEnd();
                                xmlWriter.Close();
                                reader.Close();
                            }
                        }
                    }
                }
                return result;
            }
            catch (SerializationException serEx)
            {
                //TODO: log de exception
                throw new SerializationException(string.Format("Het object: {0} kan niet geserialiseerd worden", myobject),
                                                 serEx.InnerException);

            }
        }

        /// <summary>
        /// Deserialize object into an instance of T
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public T Deserialize(String xml)
        {
            try
            {
                if (!String.IsNullOrEmpty(xml))
                {
                    using (var sr = new StringReader(xml))
                    {
                        return (T)serializer.Deserialize(sr);
                    }
                }
            }
            catch (SerializationException serEx)
            {
                //TODO: log de exception
                throw new SerializationException(string.Format("De xml: {0} kan niet gede-serialiseerd worden", xml),
                                                 serEx.InnerException);

            }
            return default(T);
        }

        #endregion
    }
}
