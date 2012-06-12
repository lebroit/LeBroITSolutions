using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using LebroITSolutions.Common.Helpers;

namespace LebroITSolutions.ChatModel
{
    [Serializable]
    public class ErrorMessages : List<ErrorMessage>
    {
        public bool WriteExceptions2File(string filePath, bool useInstallFolder)
        {
            try
            {
                //no filePath use Isolatedstorage
                if (string.IsNullOrEmpty(filePath) && useInstallFolder)
                {
                    filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }

                //serialize the errormessages to xml
                var ser = new XMLSerializerHelper<ErrorMessages>().Serialize(this);
                //check if the argument is not null
                if (Equals(filePath, null)) throw new ArgumentNullException("filePath");
                //construct the used directory fullname
                var fileDirectory = string.Format(@"{0}\Errors", filePath);
                //construct the full filename
                filePath = string.Format(@"{0}\Errorlog{1}.xml", fileDirectory, 
                    DateTime.Now.ToString().Replace(':', '-'));
                //check if the directory exists
                if (!Directory.Exists(fileDirectory))
                {
                    //if not create it here
                    Directory.CreateDirectory(fileDirectory);
                }
                //write the errors to disk
                using (var writer = new StreamWriter(filePath))
                {
                    writer.Write(ser);
                    writer.Flush();
                }

                return true;
            }
            catch (Exception)
            {
                //do nothing here, the application is closing down anyway
                return false;
            }
        }
    }
    /// <summary> 
    /// A wrapper class for serializing exceptions. 
    /// </summary> 
    [Serializable]
    [DesignerCategory(@"code")]
    [XmlType(AnonymousType = true, Namespace = "http://LeBroITSolutions.Exceptions")]
    [XmlRootAttribute(Namespace = "http://LeBroITSolutions.Exceptions", IsNullable = false)]
    public class ErrorMessage
    {
        #region Members
        //This is the reason this class exists. Turning an IDictionary into a serializable object     
        private KeyValuePair<object, object>[] data;
        #endregion

        #region Constructors
        public ErrorMessage() { }
        public ErrorMessage(Exception exception, Assembly assembly)
            : this()
        {
            SetValues(exception, assembly);
        }
        #endregion
        #region Properties
        [XmlElement("UsesWebservice", typeof(bool))]
        public bool UsesWebservice { get; set; }
        [XmlElement("CurrentUser", typeof(string))]
        public string CurrentUser{ get; set; }
        [XmlElement("HelpLink", typeof(string))]
        public string HelpLink { get; set; }
        [XmlElement("Message", typeof(string))]
        public string Message { get; set; }
        [XmlElement("Source", typeof(string))]
        public string Source { get; set; }
        [XmlElement("StackTrace", typeof(string))]
        public string StackTrace { get; set; }
        // Allow null to be returned, so serialization doesn't cascade until an out of memory exception occurs     
        [XmlElement("InnerException", typeof(ErrorMessage))]
        public ErrorMessage InnerException { get; set; }
        [XmlElement("Data", typeof(KeyValuePair<object, object>))]
        public KeyValuePair<object, object>[] Data
        {
            get { return data ?? new KeyValuePair<object, object>[0]; }
            set { data = value; }
        }
        #region extra information properties

        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        [XmlElement("ExceptionType", typeof(string))]
        public string ExceptionType { get; set; }

        /// <summary>
        /// Holds the runtime version where the exception occured.
        /// </summary>
        [XmlElement("RuntimeVersion", typeof(string))]
        public string RuntimeVersion { get; set; }

        /// <summary>
        /// Holds the current OS information.
        /// </summary>
        public SerializableOperatingSystem OSInformation { get; set; }

        public SerializableAssembly CurrentAssembly { get; set; }

        public SerializableAssembly EntryAssembly { get; set; }

        #endregion
        #endregion
        #region Private Methods
        private void SetValues(Exception exception, Assembly assembly)
        {
            if (null == exception) return;

            HelpLink = exception.HelpLink ?? string.Empty;
            Message = exception.Message;
            Source = exception.Source ?? string.Empty;
            StackTrace = exception.StackTrace ?? string.Empty;
            SetData(exception.Data);
            InnerException = new ErrorMessage(exception.InnerException, Assembly.GetExecutingAssembly());
            ExceptionType = exception.GetType().FullName;
            RuntimeVersion = exception.GetType().Assembly.ImageRuntimeVersion;
            OSInformation = new SerializableOperatingSystem();
            EntryAssembly = new SerializableAssembly(Assembly.GetEntryAssembly()) { IsEntryAssembly = true };
            CurrentAssembly = new SerializableAssembly(assembly) { IsExecutingAssembly = true };
        }
        private void SetData(ICollection collection)
        {
            data = new KeyValuePair<object, object>[0];
            if (null != collection)
                collection.CopyTo(data, 0);
        }
        #endregion
    }

    /// <summary>
    /// Wrapper class to serialize OS information
    /// </summary>
    [Serializable]
    public class SerializableOperatingSystem
    {
        #region Properties
        [XmlElement("OsVersion", typeof(string))]
        public string OsVersion { get; set; }
        [XmlElement("ServicePack", typeof(string))]
        public string ServicePack { get; set; }
        [XmlElement("Platform", typeof(string))]
        public string Platform { get; set; }
        [XmlElement("ProcessorCount", typeof(int))]
        public int ProcessorCount { get; set; }
        [XmlElement("OSEnvironmentStacktrace", typeof(string))]
        public string OSEnvironmentStacktrace { get; set; }

        #endregion

        #region Constructors

        public SerializableOperatingSystem()
        {
            Platform = Environment.OSVersion.Platform.ToString();
            ServicePack = Environment.OSVersion.ServicePack;
            OsVersion = Environment.OSVersion.VersionString;
            ProcessorCount = Environment.ProcessorCount;
            OSEnvironmentStacktrace = Environment.StackTrace;
        }

        #endregion
    }

    /// <summary>
    ///Wrapper class to serialize Assembly information
    /// </summary>
    [Serializable]
    public class SerializableAssembly
    {
        #region Properties

        [XmlElement("IsExecutingAssembly", typeof(bool))]
        public bool IsExecutingAssembly { get; set; }
        [XmlElement("IsEntryAssembly", typeof(bool))]
        public bool IsEntryAssembly { get; set; }
        [XmlElement("CodeBase", typeof(string))]
        public string CodeBase { get; set; }
        [XmlElement("FullName", typeof(string))]
        public string FullName { get; set; }
        [XmlElement("GlobalAssemblyCache", typeof(string))]
        public bool GlobalAssemblyCache { get; set; }
        [XmlElement("ImageRuntimeVersion", typeof(string))]
        public string ImageRuntimeVersion { get; set; }
        [XmlElement("Location", typeof(string))]
        public string Location { get; set; }
        [XmlElement("FullyQualifiedName", typeof(string))]
        public string FullyQualifiedName { get; set; }
        [XmlElement("ModuleVersionId", typeof(string))]
        public string ModuleVersionId { get; set; }
        [XmlElement("Name", typeof(string))]
        public string Name { get; set; }
        [XmlElement("ScopeName", typeof(string))]
        public string ScopeName { get; set; }


        #endregion

        #region Constructors

        public SerializableAssembly() { }

        public SerializableAssembly(Assembly assembly)
        {
            CodeBase = assembly.CodeBase;
            FullName = assembly.FullName;
            GlobalAssemblyCache = assembly.GlobalAssemblyCache;
            ImageRuntimeVersion = assembly.ImageRuntimeVersion;
            Location = assembly.Location;
            FullyQualifiedName = assembly.ManifestModule.FullyQualifiedName;
            ModuleVersionId = assembly.ManifestModule.ModuleVersionId.ToString();
            Name = assembly.ManifestModule.Name;
            ScopeName = assembly.ManifestModule.ScopeName;

        }

        #endregion
    }
}
