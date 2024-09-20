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
                    Tracker = ruleElement.Element("tracker")?.Value,
                    Type = ruleElement.Element("type")?.Value,
                    Interface = ruleElement.Element("interface")?.Value,
                    IPProtocol = ruleElement.Element("ipprotocol")?.Value,
                    Tag = ruleElement.Element("tag")?.Value,
                    Tagged = ruleElement.Element("tagged")?.Value,
                    Max = int.TryParse(ruleElement.Element("max")?.Value, out var max) ? max : (int?)null,
                    MaxSrcNodes = int.TryParse(ruleElement.Element("max-src-nodes")?.Value, out var maxSrcNodes) ? maxSrcNodes : (int?)null,
                    MaxSrcConn = int.TryParse(ruleElement.Element("max-src-conn")?.Value, out var maxSrcConn) ? maxSrcConn : (int?)null,
                    MaxSrcStates = int.TryParse(ruleElement.Element("max-src-states")?.Value, out var maxSrcStates) ? maxSrcStates : (int?)null,
                    Statetimeout = int.TryParse(ruleElement.Element("statetimeout")?.Value, out var statetimeout) ? statetimeout : (int?)null,
                    Statetype = ruleElement.Element("statetype")?.Value,
                    OS = ruleElement.Element("os")?.Value,
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
                    Disabled = ruleElement.Element("disabled") != null,
                    Created = new Timestamp(),
                    Updated = new Timestamp()
                };

                rule.Created.Time = DateTime.MinValue; // default
                rule.Updated.Time = DateTime.MinValue; // default

                // Check if both "created" and "time" elements exist and have a valid Unix timestamp
                var createdElement = ruleElement.Element("created");
                if (createdElement != null)
                {
                    var timeElement = createdElement.Element("time");
                    if (timeElement != null && !string.IsNullOrWhiteSpace(timeElement.Value))
                    {
                        if (long.TryParse(timeElement.Value, out long unixTime))
                        {
                            // Convert Unix time to DateTime (UTC)
                            rule.Created.Time = DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
                        }
                    }
                }
                rule.Created.Username = createdElement?.Element("username")?.Value ?? "n/a";

                // Check if both "created" and "time" elements exist and have a valid Unix timestamp
                var updatedElement = ruleElement.Element("updated");
                if (updatedElement != null)
                {
                    var timeElement = updatedElement.Element("time");
                    if (timeElement != null && !string.IsNullOrWhiteSpace(timeElement.Value))
                    {
                        if (long.TryParse(timeElement.Value, out long unixTime))
                        {
                            // Convert Unix time to DateTime (UTC)
                            rule.Updated.Time = DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
                        }
                    }
                }
                rule.Updated.Username = createdElement?.Element("username")?.Value ?? "n/a";

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
