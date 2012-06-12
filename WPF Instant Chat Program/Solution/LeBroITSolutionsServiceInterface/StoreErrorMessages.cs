using LebroITSolutions.ChatModel;
using LeBroITSolutions.ServiceStack.Interfaces.ServiceHost;

namespace LeBroITSolutionsServiceInterface
{
    public class StoreErrorMessages : IService<ErrorMessage>
    {
        public object Execute(ErrorMessage request)
        {
            return ErrorMessageStored(request);
        }

        private static bool ErrorMessageStored(ErrorMessage request)
        {
            return true;
        }
    }
}
