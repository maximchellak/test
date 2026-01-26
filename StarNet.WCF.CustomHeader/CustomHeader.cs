using System.ServiceModel.Channels;
using System.Xml;

namespace StarNet.WCF.CustomHeader
{
    public class CustomHeader : MessageHeader
    {
        private const string CUSTOM_HEADER_NAME = "Authorization";
        private const string CUSTOM_HEADER_NAMESPACE = "";

        public ServiceHeader CustomData { get; private set; }

        public CustomHeader()
        {
        }

        public CustomHeader(ServiceHeader customData)
        {
            CustomData = customData;
        }

        public override string Name
        {
            get { return (CUSTOM_HEADER_NAME); }
        }

        public override string Namespace
        {
            get { return (CUSTOM_HEADER_NAMESPACE); }
        }

        protected override void OnWriteHeaderContents(
            System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(ServiceHeader));
            //StringWriter textWriter = new StringWriter();
            //serializer.Serialize(textWriter, _customData);
            //textWriter.Close();
            //string text = textWriter.ToString();

            writer.WriteString(CustomData?.BasicAuthorization?.Trim());

            //writer.WriteElementString(
            //    CUSTOM_HEADER_NAME, 
            //    CustomData?.BasicAuthorization?.Trim());
        }

        public static ServiceHeader ReadHeader(Message request)
        {
            var headerPosition = request.Headers.FindHeader(
                CUSTOM_HEADER_NAME, 
                CUSTOM_HEADER_NAMESPACE);

            if (headerPosition == -1)
            {
                return null;
            }

            //MessageHeaderInfo headerInfo = request.Headers[headerPosition];
            var content = request.Headers.GetHeader<XmlNode[]>(headerPosition);
            var text = content[0].InnerText;
            //XmlSerializer deserializer = new XmlSerializer(typeof(ServiceHeader));
            //TextReader textReader = new StringReader(text);
            //ServiceHeader customData = (ServiceHeader)deserializer.Deserialize(textReader);
            //textReader.Close();

            var customData = new ServiceHeader();
            customData.BasicAuthorization = text;

            return customData;
        }
    }
}
