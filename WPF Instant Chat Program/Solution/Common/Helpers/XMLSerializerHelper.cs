using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LebroITSolutions.Common.Helpers
{
    public class XMLSerializerHelper<T> where T : class
    {
        #region Serialization support
        /// <summary>
        /// XML serializer as class attribuut
        /// </summary>
        private readonly XmlSerializer serializer =
                                new XmlSerializer(typeof(T));

        /// <summary>
        /// Serialize object naar een XML string
        /// </summary>
        /// <param name="myobject"></param>
        /// <returns></returns>
        public String SerializeWithOutNameSpace(T myobject)
        {
            try
            {
                String result = null;
                if (myobject != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        var settings = new XmlWriterSettings
                        {
                            Indent = true,
                            OmitXmlDeclaration = true,

                        };
                        using (var xmlWriter = XmlWriter.Create(ms, settings))
                        {
                            var ns = new XmlSerializerNamespaces();
                            ns.Add("", "");
                            serializer.Serialize(xmlWriter, myobject, ns);
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
                throw new SerializationException(string.Format("Het object: {0} kan niet geserialiseerd worden", myobject),
                                                 serEx.InnerException);
            }
        }

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
                        var settings = new XmlWriterSettings
                        {
                            Indent = true,

                        };
                        using (var xmlWriter = XmlWriter.Create(ms, settings))
                        {
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
                throw new SerializationException(string.Format("Het object: {0} kan niet geserialiseerd worden", myobject),
                                                 serEx.InnerException);
            }
        }

        /// <summary>
        /// Deserialize xml into an instance of T
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
                throw new SerializationException(string.Format("De xml: {0} kan niet gede-serialiseerd worden", xml),
                                                 serEx.InnerException);
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }

        #endregion
    }
}
