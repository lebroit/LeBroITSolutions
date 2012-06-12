using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using LebroITSolutions.ChatModel;

namespace WPFChatProgram.Controllers
{
    /// <summary>
    /// The controller class for the Configuration view
    /// </summary>
    public class ConfigurationController : MainController
    {

#region Configuration Propterties and Attributes
        
        private ObservableCollection<string> languages;
        /// <summary>
        /// Prop to plug in the languages that ar available
        /// </summary>
        public ObservableCollection<string> Languages
        {
            get { languages = LanguageUsed(MainUser.Language); return languages ; }
            set { languages = value; OnPropertyChanged(new PropertyChangedEventArgs("Languages")); }
        }
        
#endregion

#region Constructor

        /// <summary>
        /// Set the available values in the Globals
        /// </summary>
        public ConfigurationController()
        {
            Globals.ListWithIPAdresses.Clear();
            
            //get the mainuser from the app.config file
            if(MainUser.UserName == null)MainUser.Load();
            Globals.UpdateHosts();
            MainUser.HostName = Dns.GetHostName();
            if (!Globals.ListWithIPAdresses.Contains(MainUser.IpAddress)) Globals.ListWithIPAdresses.Add(MainUser.IpAddress);
            MainUser.MainUserHasChanges = false;
        }

#endregion

#region Set languages
        //Help method to change languagelist with appropiate values
        public ObservableCollection<string > LanguageUsed(string language)
        {
            if (language == null) return languages;
            if(language.ToLower() == "english" || language.ToLower() == "engels")
            {
                AddOrRemoveResourceDict("English", "Dutch");
                MainUser.Language = "English";
                return new ObservableCollection<string> { "English", "Dutch" };
            }
            AddOrRemoveResourceDict("Dutch", "English");
            MainUser.Language = "Nederlands";
            return new ObservableCollection<string> { "Nederlands", "Engels" };
        }

        public void AddOrRemoveResourceDict(string add,string remove)
        {
            Application.Current.Resources.MergedDictionaries.Add(
                        Application.LoadComponent(new Uri(string.Format("Resources/{0}Dictionary.xaml", add),
                        UriKind.Relative)) as ResourceDictionary);

            Application.Current.Resources.MergedDictionaries.Remove(
                        Application.LoadComponent(new Uri(string.Format("Resources/{0}Dictionary.xaml", remove),
                        UriKind.Relative)) as ResourceDictionary);
        }

#endregion
    }
}
