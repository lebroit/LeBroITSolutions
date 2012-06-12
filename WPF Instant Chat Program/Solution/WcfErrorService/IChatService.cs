using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Services.Description;
using LebroITSolutions.ChatModel;

namespace WcfErrorService
{
    [ServiceContract(Name = "ChatService")]
    public interface IChatService
    {
        [OperationContract(Action = "*", IsOneWay = false)]
        [FaultContract(typeof(string))]
        string HandleBadRequests(Message request);

        [OperationContract(Action = "InsertErrors", IsOneWay = false)]
        Boolean SaveErrorMessages(List<string> errors, string userName, string password);

        [OperationContract(Action = "InsertUserInfo", IsOneWay = false)]
        Boolean SaveUserInfo(ChatUserInfo userInfo, string password);

        [OperationContract]
        List<string> GetErrorMessages(string userName, string password);

        [OperationContract]
        ChatUserInfo GetUserInfo(string userName, string password);

        [OperationContract]
        List<ChatUserInfo> GetListOfUserInfo(string userName, string password);

    }


   
}
