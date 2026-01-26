using StarNet.Logging;
using StarNet.Services.Core;
using StarNet.WCF.CustomHeader;

namespace StarNet.Services
{
    [CustomAuthorizationBehavior]
    [GlobalErrorBehaviorAttribute(typeof(GlobalErrorHandler))]
    public class BaseService
    {
        public Logger Logger { get; private set; }

        public BaseService()
        {
            Logger = this.InitLogger();
        }
    }
}
