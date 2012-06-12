using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LebroITSolutions.ChatModel
{
    [Serializable]
    public class ChatMessage : INotifyPropertyChanged
    {

#region INotifyProperties

        private List<ChatUserInfo> usersInvolved;

        public List<ChatUserInfo> UsersInvolved
        {
            get { return usersInvolved; }
            set 
            { 
                usersInvolved = value; 
                OnPropertyChanged(new PropertyChangedEventArgs("UsersInvolved")); 
            }
        }

        private string partialMessage;

        public string PartialMessage
        {
            get { return partialMessage; }
            set
            {
                partialMessage = value; 
                OnPropertyChanged(new PropertyChangedEventArgs("PartialMessage"));
            }
        }

        private StringBuilder completeMessage;

        public StringBuilder CompleteMessage
        {
            get { return completeMessage ?? (completeMessage = new StringBuilder()); }
            set
            {
                completeMessage = value; 
                OnPropertyChanged(new PropertyChangedEventArgs("CompleteMessage"));
            }
        }

        private DateTime dateTimeStamp;

        public DateTime DateTimeStamp
        {
            get { return dateTimeStamp; }
            set
            {
                dateTimeStamp = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DateTimeStamp"));
            }
        }

        private Guid messageId;

        public Guid MessageId
        {
            get { return messageId; }
            set { messageId = value; }
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
                PropertyChanged(this, e);
            }
        }

#endregion

    }
}
