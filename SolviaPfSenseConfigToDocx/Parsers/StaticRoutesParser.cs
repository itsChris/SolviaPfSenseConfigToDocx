using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class StaticRoutesParser : IParser<List<StaticRoute>>
    {
        public List<StaticRoute> Parse(XElement staticRoutesElement)
        {
            var staticRoutes = new List<StaticRoute>();

            foreach (var routeElement in staticRoutesElement.Elements("route"))
            {
                var staticRoute = new StaticRoute
                {
                    Destination = routeElement.Element("network")?.Value,
                    Gateway = routeElement.Element("gateway")?.Value,
                    Interface = routeElement.Element("interface")?.Value,
                    Description = routeElement.Element("descr")?.Value,
                    Disabled = routeElement.Element("disabled") != null
                };

                staticRoutes.Add(staticRoute);
            }

            return staticRoutes;
        }
    }

}
