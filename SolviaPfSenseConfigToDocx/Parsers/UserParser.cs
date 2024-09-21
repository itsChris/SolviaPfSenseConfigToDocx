using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class UserParser : IParser<List<User>>
    {
        public List<User> Parse(XElement systemElement)
        {

            HtmlDecodeTextOnly(systemElement);

            var users = new List<User>();
            foreach (var userElement in systemElement.Elements("user"))
            {
                var user = new User
                {
                    Scope = userElement.Element("scope")?.Value,
                    BcryptHash = userElement.Element("bcrypt-hash")?.Value,
                    Name = userElement.Element("name")?.Value,
                    Expires = DateTime.TryParse(userElement.Element("expires")?.Value, out var expires) ? expires : (DateTime?)null,
                    DashboardColumns = int.Parse(userElement.Element("dashboardcolumns")?.Value ?? "0"),
                    UID = int.Parse(userElement.Element("uid")?.Value ?? "0"),
                    Description = userElement.Element("descr")?.Value,
                    IPSecPSK = userElement.Element("ipsecpsk")?.Value,
                    WebGUICSS = userElement.Element("webguicss")?.Value,
                    
                    Certificates = new List<Certificate>(),

                    AuthorizedKeys = new List<string>(),

                };
                users.Add(user);
            }

            return users;
        }
        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }

}
