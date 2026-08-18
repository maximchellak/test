using System.Runtime.Serialization;

namespace StarNet.Services.Contracts.DataContracts.BatchService
{
    [DataContract]
    public class DownloadFilesBatchTaskRequest : BaseTaskRequest
    {
        [DataMember]
        public int[] DocumentIds { get; set; }

        [DataMember]
        public int? InvoiceId { get; set; }

        [DataMember]
        public long? InvoiceNumber { get; set; }

        [DataMember]
        public string InvoiceType { get; set; }

    }
}
