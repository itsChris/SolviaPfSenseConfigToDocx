//using SolviaPfSenseConfigToDocx.DataModels;
//using System.Xml.Linq;

//namespace SolviaPfSenseConfigToDocx
//{
//    public class PfSenseConfigParser
//    {
//        public XDocument XmlDoc { get; private set; }

//        public PfSenseConfigParser(string xmlFilePath)
//        {
//            // Load the XML file
//            if (System.IO.File.Exists(xmlFilePath))
//            {
//                try
//                {
//                    XmlDoc = XDocument.Load(xmlFilePath);
//                    Console.WriteLine("XML file loaded successfully.");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error loading XML: {ex.Message}");
//                }
//            }
//            else
//            {
//                Console.WriteLine("XML file does not exist.");
//            }
//        }

//        //public SystemConfig ParseSystemConfig()
//        //{
//        //    var systemConfig = new SystemConfig();

//        //    XElement systemElement = XmlDoc.Root.Element("system");

//        //    if (systemElement != null)
//        //    {
//        //        systemConfig.Hostname = systemElement.Element("hostname")?.Value;
//        //        systemConfig.Domain = systemElement.Element("domain")?.Value;

//        //        // Parse Users
//        //        systemConfig.Users = new List<User>();
//        //        foreach (XElement userElement in systemElement.Elements("user"))
//        //        {
//        //            var user = new User
//        //            {
//        //                Name = userElement.Element("name")?.Value,
//        //                UID = userElement.Element("uid")?.Value,
//        //                // More fields as needed
//        //            };
//        //            systemConfig.Users.Add(user);
//        //        }

//        //        // Parse Groups
//        //        systemConfig.Groups = new List<Group>();
//        //        foreach (XElement groupElement in systemElement.Elements("group"))
//        //        {
//        //            var group = new Group
//        //            {
//        //                Name = groupElement.Element("name")?.Value,
//        //                GID = groupElement.Element("gid")?.Value,
//        //                // More fields as needed
//        //            };
//        //            systemConfig.Groups.Add(group);
//        //        }
//        //    }

//        //    return systemConfig;
//        //}

//        public Dictionary<string, List<string>> ParseConfig()
//        {
//            var configData = new Dictionary<string, List<string>>();

//            try
//            {
//                // Parse root elements in config.xml
//                var rootElements = XmlDoc.Root.Elements();

//                foreach (var element in rootElements)
//                {
//                    string elementName = element.Name.LocalName;
//                    var elementData = new List<string>();

//                    // Parse sub-elements recursively
//                    foreach (var subElement in element.Elements())
//                    {
//                        elementData.Add($"{subElement.Name.LocalName}: {subElement.Value}");
//                    }

//                    configData[elementName] = elementData;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error parsing XML: {ex.Message}");
//            }

//            return configData;
//        }
//    }
//}
