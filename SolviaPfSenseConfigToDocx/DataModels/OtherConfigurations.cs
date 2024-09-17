using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class OtherConfigurations
    {
        public List<Alias> Aliases { get; set; } = new List<Alias>();
        public SNMPConfig SNMP { get; set; }
        public List<Gateway> Gateways { get; set; } = new List<Gateway>();
        public CaptivePortalConfig CaptivePortal { get; set; }
        public DNSMasqConfig DNSMasq { get; set; }
        public UnboundConfig Unbound { get; set; }
        public List<CronJob> CronJobs { get; set; } = new List<CronJob>();
        public List<Package> Packages { get; set; } = new List<Package>();
        public List<VirtualIP> VirtualIPs { get; set; } = new List<VirtualIP>();
    }
}
