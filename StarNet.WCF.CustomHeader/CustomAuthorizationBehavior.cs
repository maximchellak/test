using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace StarNet.WCF.CustomHeader
{
    /// <summary>
    /// This custom behavior class is used to add both client and server inspectors to
    /// the corresponding WCF end points.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAuthorizationBehavior : Attribute, IServiceBehavior, IEndpointBehavior
    {
        #region IEndpointBehavior Members
        void IEndpointBehavior.AddBindingParameters(
            ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(
            ServiceEndpoint endpoint,
            ClientRuntime clientRuntime)
        {
            var inspector = new CustomMessageInspector();
            clientRuntime.MessageInspectors.Add(inspector);
        }

        void IEndpointBehavior.ApplyDispatchBehavior(
            ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        {
            var channelDispatcher = endpointDispatcher.ChannelDispatcher;

            if (channelDispatcher != null)
            {
                foreach (EndpointDispatcher ed in channelDispatcher.Endpoints)
                {
                    var inspector = new CustomMessageInspector();
                    ed.DispatchRuntime.MessageInspectors.Add(inspector);
                }
            }
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IServiceBehavior Members
        void IServiceBehavior.AddBindingParameters(
            ServiceDescription serviceDescription,
             ServiceHostBase serviceHostBase,
             Collection<ServiceEndpoint> endpoints,
             BindingParameterCollection bindingParameters)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(
            ServiceDescription desc,
            ServiceHostBase host)
        {
            foreach (ChannelDispatcher cDispatcher in host.ChannelDispatchers)
            {
                foreach (EndpointDispatcher eDispatcher in cDispatcher.Endpoints)
                {
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomMessageInspector());
                }
            }
        }

        void IServiceBehavior.Validate(ServiceDescription desc, ServiceHostBase host)
        {
        }

        #endregion
    }
}
