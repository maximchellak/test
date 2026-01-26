using System.Runtime.Serialization;

namespace StarNet.WCF.CustomHeader
{
    [DataContract]
    public class ServiceHeader
    {
        [DataMember]
        public string BasicAuthorization { get; set; }
    }
}
