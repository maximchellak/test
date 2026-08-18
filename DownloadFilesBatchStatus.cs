using System.Runtime.Serialization;

namespace StarNet.Services.Contracts.DataContracts.BatchService
{
    [DataContract]
    public class DownloadFilesBatchStatus : BaseBatchStatus
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FilePath { get; set; }
    }
}
