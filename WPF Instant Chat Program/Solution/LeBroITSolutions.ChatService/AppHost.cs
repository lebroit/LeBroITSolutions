using LeBroITSolutions.ServiceStack.Funq;
using LeBroITSolutions.ServiceStack.WebHost.Endpoints;
using LeBroITSolutionsServiceInterface;

namespace LeBroITSolutions.ChatService
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("ChatService", typeof(StoreErrorMessages).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
            base.SetConfig(new EndpointHostConfig
            {
                GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
					},
            });
        }
    }
}