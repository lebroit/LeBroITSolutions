using System;
using System.ComponentModel;
using LebroITSolutions.ChatModel.Properties;

namespace LebroITSolutions.ChatModel
{
    /// <summary>
    /// Object Class to retain, collect and retreive
    /// chat user related information
    /// </summary>
    public class ChatUserInfo : INotifyPropertyChanged
    {

#region INotifyProperties

        private string ipAddress;
        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IpAddress
        {
            get { return ipAddress; }
            set{ipAddress = value; OnPropertyChanged(new PropertyChangedEventArgs("IpAddress"));}
        }

        private string hostName;
        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        /// <value>
        /// The name of the host.
        /// </value>
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; OnPropertyChanged(new PropertyChangedEventArgs("HostName")); }
        }
        private string subnetMask;
        /// <summary>
        /// Gets or sets the subnet mask.
        /// </summary>
        /// <value>
        /// The subnet mask.
        /// </value>
        public string SubnetMask
        {
            get { return subnetMask; }
            set { subnetMask = value; OnPropertyChanged(new PropertyChangedEventArgs("SubnetMask")); }
        }

        private string userName;
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(new PropertyChangedEventArgs("UserName")); }
        }

        private string defaultPort;
        /// <summary>
        /// Gets or sets the default port.
        /// </summary>
        /// <value>
        /// The default port.
        /// </value>
        public string DefaultPort
        {
            get { return defaultPort; }
            set { defaultPort = value; OnPropertyChanged(new PropertyChangedEventArgs("DefaultPort")); }
        }

        private string alias;
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        public string Alias
        {
            get { return alias; }
            set { alias = value; OnPropertyChanged(new PropertyChangedEventArgs("Alias")); }
        }

        private bool userNameOrAlias;
        /// <summary>
        /// Gets or sets a value indicating whether [user name or alias].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [user name or alias]; otherwise, <c>false</c>.
        /// </value>
        public bool UserNameOrAlias
        {
            get { return userNameOrAlias; }
            set { userNameOrAlias = value; OnPropertyChanged(new PropertyChangedEventArgs("UserNameOrAlias")); }
        }

        private bool webserviceUsed;

        /// <summary>
        /// Gets or sets a value indicating whether [webservice is used].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [webservice used]; otherwise, <c>false</c>.
        /// </value>
        public bool WebserviceUsed
        {
            get { return webserviceUsed; }
            set { webserviceUsed = value; OnPropertyChanged(new PropertyChangedEventArgs("WebserviceUsed")); }
        }

        private string textColor;
        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        public string TextColor
        {
            get { return textColor; }
            set { textColor = value; OnPropertyChanged(new PropertyChangedEventArgs("TextColor")); }
        }

        private bool isMainUser;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is main user.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is main user; otherwise, <c>false</c>.
        /// </value>
        public bool IsMainUser
        {
            get { return isMainUser; }
            set { isMainUser = value; OnPropertyChanged(new PropertyChangedEventArgs("IsMainUser")); }
        }

        private bool ignore;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ChatUserInfo"/> is ignore.
        /// </summary>
        /// <value>
        ///   <c>true</c> if ignore; otherwise, <c>false</c>.
        /// </value>
        public bool Ignore
        {
            get { return ignore; }
            set { ignore = value; OnPropertyChanged(new PropertyChangedEventArgs("Ignore")); }
        }

        private bool hidden;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ChatUserInfo"/> is hidden.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hidden; otherwise, <c>false</c>.
        /// </value>
        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; OnPropertyChanged(new PropertyChangedEventArgs("Hidden")); }
        }

        private string language;
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language
        {
            get { return language; }
            set
            {
                language = value; OnPropertyChanged(new PropertyChangedEventArgs("Language"));
            }
        }

        private string fontName;
        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>
        /// The name of the font.
        /// </value>
        public string FontName
        {
            get { return fontName; }
            set
            {
                fontName = value; OnPropertyChanged(new PropertyChangedEventArgs("FontName"));
            }
        }

        private double? fontSize;

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        public double? FontSize
        {
            get { return fontSize; }
            set { fontSize = value; OnPropertyChanged(new PropertyChangedEventArgs("FontSize")); }
        }

        /// <summary>
        /// Used to keep track is somthing had changed on the MainUser props
        /// </summary>
        public bool MainUserHasChanges { get; set; }
#endregion

#region Load & Save
        /// <summary>
        /// Loading MainUser props from User scoped settings
        /// </summary>
        public void Load()
        {
            HostName = Settings.Default.HostName;
            UserName = !(Settings.Default.UserNameOrAlias) ? Settings.Default.UserName : null;
            Alias = (Settings.Default.UserNameOrAlias) ? Settings.Default.Alias : null;
            UserName = string.IsNullOrEmpty(UserName) ? Environment.UserName : UserName;
            DefaultPort = Settings.Default.DefaultPort;
            IpAddress = Settings.Default.IPAddress;
            SubnetMask = Settings.Default.SubNetMask;
            UserNameOrAlias = Settings.Default.UserNameOrAlias;
            TextColor = (!(string.IsNullOrEmpty(Settings.Default.TextColor)) ? Settings.Default.TextColor : "Black");
            IsMainUser = true;
            Language = Settings.Default.Language;
            FontName = Settings.Default.FontName;
            FontSize = Settings.Default.FontSize;
            WebserviceUsed = Settings.Default.WebserviceUsed;
        }

        /// <summary>
        /// Saving props changes to the User scoped settings
        /// </summary>
        public void Save()
        {
            Settings.Default.UserName = UserName;
            Settings.Default.Alias = Alias;
            Settings.Default.UserNameOrAlias = userNameOrAlias;
            Settings.Default.DefaultPort = DefaultPort;
            Settings.Default.IPAddress = IpAddress;
            Settings.Default.SubNetMask = SubnetMask;
            Settings.Default.TextColor = TextColor;
            Settings.Default.FontName = FontName;
            Settings.Default.FontSize = FontSize.GetValueOrDefault();
            Settings.Default.Language = Language;
            Settings.Default.WebserviceUsed = WebserviceUsed;

            Settings.Default.Save();
        }
#endregion

#region PropertyChanged Event
        /// <summary>
        /// the props changed eventhandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifiying that a prop has changed and setting the
        /// boolean value who keeps track, to true
        /// </summary>
        /// <param name="e"></param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                MainUserHasChanges = IsMainUser;
                PropertyChanged(this, e);
            }
        }

#endregion
        
    }
}
