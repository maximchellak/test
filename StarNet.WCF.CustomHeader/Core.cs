using System.ServiceModel;

namespace StarNet.WCF.CustomHeader
{
    public static class Core
    {
        public static void AddCustomBehavior(ChannelFactory channelFactory)
        {
            SetBasicAuthorization();
            channelFactory.Endpoint.Behaviors.Add(new CustomAuthorizationBehavior());
        }

        public static void SetBasicAuthorization()
        {
            var tokenInfo = JWTManager.TokenManager.CreateStarnetTokenInfo();
            var tokenString = JWTManager.TokenManager.CreateToken(tokenInfo);

            ClientContext.BasicAuthorization = tokenString;
        }
    }
}
