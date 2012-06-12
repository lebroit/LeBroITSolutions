using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;

namespace LebroITSolution.Generics.IsolatedStorage
{
    //TODO L.B. hier gebleven moet nog getest worden
    /// <summary>
    /// A Generic class to de/serialize objects to IsolatedUserStorage
    /// </summary>
    public class IsolatedUserStorage<T> where T : class
    {
        #region Private FIelds

        private const string Multivers = "Multivers";
        private const string SecurityProblemSave =
            "Security permissions zijn niet voldoende om de {0} te bewaren!";
        private const string SecurityProblemLoad =
            "Security permissions zijn niet voldoende om de {0} op te halen!";

        #endregion

        #region Load Objects from IsolatedUserStorage

        /// <summary>
        /// Loads the Objects from isolated user storage.
        /// </summary>
        /// <returns></returns>
        public T LoadObjectsFromIsolatedStorage(string pathName)
        {
            //Argument/parameter checking
            if (string.IsNullOrEmpty(pathName)) throw new ArgumentNullException("pathName");

            try
            {
                var userStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                var isfp = new IsolatedStorageFilePermission(PermissionState.Unrestricted);

                if (userStorage.GetFileNames(pathName).Length > 0)
                {
                    var bf = new BinaryFormatter();
                    using (var fs = new IsolatedStorageFileStream(pathName, FileMode.OpenOrCreate, userStorage))
                    {
                        return (T)bf.Deserialize(fs);
                    }
                }

                if (!UsageAllowed(isfp, IsolatedStorageContainment.UnrestrictedIsolatedStorage))
                    throw new SecurityException();

                return null;
            }
            catch (SecurityException secuEx)
            {
                throw new SecurityException(string.Format(
                        "{0}{1}Tijdens het uitvoeren van LoadObjectsFromIsolatedStorage(T myObject) in klasse IsolatedUserStorage",
                        string.Format(SecurityProblemLoad, typeof(T)),
                        Environment.NewLine), secuEx);
            }
            //only occurs during first time usage, will be catched in the save methode
            catch (IOException)
            {
                return null;
            }
            //only occurs if the storage file exists only doen't have content
            //or when a not serializable object is used
            catch (SerializationException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(
                    "Onverwachte fout tijdens het uitvoeren van LoadObjectsFromIsolatedStorage({0} myObject) in klasse IsolatedUserStorage",
                    typeof(T)), ex);
            }
        }

        #endregion

        #region Save Objects from IsolatedUserStorage

        /// <summary>
        /// Saves the Objects toe isolated user storage.
        /// </summary>
        public bool SaveObjectsToIsolatedUserStorage(T myObject, string pathName)
        {
            //Argument/parameter checking
            if (string.IsNullOrEmpty(pathName)) throw new ArgumentNullException("pathName");
            if (Equals(myObject, null)) throw new ArgumentNullException("myObject");

            try
            {
                //opening the userstorage and streaming it into a 
                //StreamWriter so save the objects
                var userStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                var isfp = new IsolatedStorageFilePermission(PermissionState.Unrestricted);

                if (!UsageAllowed(isfp, IsolatedStorageContainment.UnrestrictedIsolatedStorage))
                    throw new SecurityException();

                if (userStorage.GetDirectoryNames(Multivers).Length == 0) userStorage.CreateDirectory(Multivers);
                var bf = new BinaryFormatter();
                using (var fs = new IsolatedStorageFileStream(pathName, FileMode.OpenOrCreate, userStorage))
                {
                    bf.Serialize(fs, myObject);
                    fs.Close();
                    return true;
                }
            }
            catch (SecurityException sucEx)
            {
                throw new SecurityException(string.Format(SecurityProblemSave, myObject.GetType()), sucEx);
            }
            catch (IOException ioEx)
            {
                throw new IOException(
                    "IO fout opgetreden  tijdens uitvoer van SaveObjectsToIsolatedUserStorage() in klasse IsolatedUserStorage",
                    ioEx);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(
                    "Ongedefinieerde fout tijdens uitvoer van SaveObjectsToIsolatedUserStorage({0} myObject) in klasse IsolatedUserStorage"
                    , myObject.GetType()), ex);
            }
        }

        #endregion


        #region Helper Methodes

        /// <summary>
        /// Check if the isolated storage permission is valid and sufficient
        /// </summary>
        /// <param name="isfp"></param>
        /// <param name="isc"></param>
        /// <returns></returns>
        private static bool UsageAllowed(IsolatedStoragePermission isfp, IsolatedStorageContainment isc)
        {
            isfp.UsageAllowed = isc;
            try
            {
                isfp.Demand();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
