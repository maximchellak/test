using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace StarNet.Services
{
    public class GlobalErrorHandler : IErrorHandler
    {
        public void ProvideFault(
            Exception error,
            MessageVersion version,
            ref Message fault)
        {
            if (error is FaultException)
            {
                return;
            }

            var faultException = new FaultException($"Run-Time error [{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}]");

            if (error is UnauthorizedAccessException)
            {
                faultException = new FaultException($"{error.Message} [{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}]");
            }

            var messageFault = faultException.CreateMessageFault();

            fault = Message.CreateMessage(version, messageFault, null);
        }

        public bool HandleError(Exception error)
        {
            Logging.Logger.Instance.Init("WCFLogger");
            Logging.Logger.Instance.LogError(error);
            return true;
        }
    }
}
