using StarNet.Services.Client.StarNetAPI.WCF.AutomaticActivity;
using StarNet.Services.Client.StarNetAPI.WCF.BatchService;
using StarNet.Services.Client.StarNetAPI.WCF.CommitmentServices;
using StarNet.Services.Client.StarNetAPI.WCF.FileServices;
using StarNet.Services.Client.StarNetAPI.WCF.IdfDigitalHatchServices;
using StarNet.Services.Client.StarNetAPI.WCF.ImportFileServices;
using StarNet.Services.Client.StarNetAPI.WCF.ImportPriceListServices;
using StarNet.Services.Client.StarNetAPI.WCF.MailSender;
using StarNet.Services.Client.StarNetAPI.WCF.PaymentServices;
using StarNet.Services.Client.StarNetAPI.WCF.PrintOutCreator;
using StarNet.Services.Client.StarNetAPI.WCF.StarnetSecurity;

namespace StarNet.Services.Client
{
    public static class Core
    {
        public static AutomaticActivityClient GetAutomaticActivityClient()
        {
            var client = new AutomaticActivityClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static BatchServiceClient GetBatchServiceClient()
        {
            var client = new BatchServiceClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static CommitmentServicesClient GetCommitmentServicesClient()
        {
            var client = new CommitmentServicesClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static FileServicesClient GetFileServicesClient(
            string endpointConfigurationName = null)
        {
            var client = new FileServicesClient(endpointConfigurationName);
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static IdfDigitalHatchServicesClient GetIdfDigitalHatchServicesClient()
        {
            var client = new IdfDigitalHatchServicesClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static ImportFileServicesClient GetImportFileServicesClient()
        {
            var client = new ImportFileServicesClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static ImportPriceListServicesClient GetImportPriceListServicesClient()
        {
            var client = new ImportPriceListServicesClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static MailSenderClient GetMailSenderClient()
        {
            var client = new MailSenderClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static PaymentServicesClient GetPaymentServicesClient()
        {
            var client = new PaymentServicesClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static PrintOutCreatorClient GetPrintOutCreatorClient()
        {
            var client = new PrintOutCreatorClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        public static StarnetSecurityClient GetStarnetSecurityClient()
        {
            var client = new StarnetSecurityClient();
            StarNet.WCF.CustomHeader.Core.AddCustomBehavior(client.ChannelFactory);
            return client;
        }

        //todo: !!! add new Service

    }
}
