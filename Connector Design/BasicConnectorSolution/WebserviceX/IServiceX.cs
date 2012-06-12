using System.ServiceModel;
using DomainModel;

namespace WebserviceX
{
    [ServiceContract]
    public interface IServiceX
    {
        [OperationContract]
        Customer GetData(string value);
    }

}
