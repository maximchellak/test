using System.ServiceModel;

namespace StarNet.WCF.CustomHeader
{
    /// <summary>
    /// This class will act as a custom context, an extension to the OperationContext.
    /// This class holds all context information for our application.
    /// </summary>
    public class ServerContext : IExtension<OperationContext>
    {
        public string BasicAuthorization;

        // Get the current one from the extensions that are added to OperationContext.
        public static ServerContext Current
        {
            get
            {
                return OperationContext.Current.Extensions.Find<ServerContext>();
            }
        }

        #region IExtension<OperationContext> Members
        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
        #endregion
    }
}
