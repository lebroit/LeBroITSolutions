using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using LebroITSolutions.ChatModel;

namespace WcfErrorService
{
    public class ChatService : IChatService
    {
        public string HandleBadRequests(Message request)
        {
            return "";
        }

        public bool SaveErrorMessages(List<string> errors, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool SaveUserInfo(ChatUserInfo userInfo, string password)
        {
            throw new NotImplementedException();
        }

        public List<string> GetErrorMessages(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public ChatUserInfo GetUserInfo(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public List<ChatUserInfo> GetListOfUserInfo(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
