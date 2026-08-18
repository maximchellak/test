using System.ServiceModel;

namespace StarNet.Services.Contracts.DataContracts.BatchService
{
    [MessageContract]
    public class CompleteDownloadFilesRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public string ClientTaskId { get; set; }

    }
}
