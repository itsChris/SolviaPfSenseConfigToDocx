using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class UserParser : IParser<List<User>>
    {
        public List<User> Parse(XElement systemElement)
        {
            var users = new List<User>();
            foreach (var userElement in systemElement.Elements("user"))
            {
                var user = new User
                {
                    Name = userElement.Element("name")?.Value,
                    UID = int.Parse(userElement.Element("uid")?.Value ?? "0"),
                    Description = userElement.Element("description")?.Value
                };
                users.Add(user);
            }

            return users;
        }
    }

}
