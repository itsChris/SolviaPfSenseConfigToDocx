using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class FirewallRulesAndNATParser : IParser<FirewallConfig>
    {
        public FirewallConfig Parse(XElement firewallElement)
        {
            var firewallConfig = new FirewallConfig();

            // Parse Firewall Rules
            foreach (var ruleElement in firewallElement.Elements("rule"))
            {
                var rule = new FirewallRule
                {
                    ID = ruleElement.Element("tracker")?.Value,
                    Interface = ruleElement.Element("interface")?.Value,
                    Protocol = ruleElement.Element("protocol")?.Value,
                    Source = new RuleEndpoint
                    {
                        Address = ruleElement.Element("source")?.Element("address")?.Value
                    },
                    Destination = new RuleEndpoint
                    {
                        Address = ruleElement.Element("destination")?.Element("address")?.Value
                    },
                    Description = ruleElement.Element("descr")?.Value,
                    Log = ruleElement.Element("log") != null,
                    Disabled = ruleElement.Element("disabled") != null
                };

                firewallConfig.FirewallRules.Add(rule);
            }

            // Parse NAT Rules
            foreach (var natElement in firewallElement.Elements("nat"))
            {
                var natRule = new NATRule
                {
                    Source = new RuleEndpoint
                    {
                        Address = natElement.Element("source")?.Element("address")?.Value
                    },
                    Target = new RuleEndpoint
                    {
                        Address = natElement.Element("target")?.Element("address")?.Value
                    },
                    //Protocol = natElement.Element("protocol")?.Value,
                    Interface = natElement.Element("interface")?.Value,
                    Description = natElement.Element("descr")?.Value,
                    //Log = natElement.Element("log") != null,
                    //Disabled = natElement.Element("disabled") != null
                };

                firewallConfig.NATRules.Add(natRule);
            }

            return firewallConfig;
        }
    }

}
