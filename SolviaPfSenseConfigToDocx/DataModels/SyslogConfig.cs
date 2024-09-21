using System.Xml.Serialization;

namespace SolviaPfSenseConfigToDocx.DataModels
{
    [Serializable]
    [XmlRoot("syslog")]
    public class SyslogConfig
    {
        [XmlElement("filterdescriptions")]
        public int FilterDescriptions { get; set; }

        [XmlElement("nentries")]
        public int NEntries { get; set; }

        [XmlElement("logcompressiontype")]
        public string LogCompressionType { get; set; }

        [XmlElement("format")]
        public string Format { get; set; }

        [XmlElement("rotatecount")]
        public int RotateCount { get; set; }

        [XmlElement("remoteserver")]
        public string RemoteServer { get; set; }

        [XmlElement("remoteserver2")]
        public string RemoteServer2 { get; set; }

        [XmlElement("remoteserver3")]
        public string RemoteServer3 { get; set; }

        [XmlElement("sourceip")]
        public string SourceIP { get; set; }

        [XmlElement("ipproto")]
        public string IpProto { get; set; }

        [XmlElement("logconfigchanges")]
        public string LogConfigChanges { get; set; }
    }
}
