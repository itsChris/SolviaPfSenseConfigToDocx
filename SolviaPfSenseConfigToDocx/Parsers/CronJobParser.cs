using SolviaPfSenseConfigToDocx.DataModels;
using SolviaPfSenseConfigToDocx.ExtensionMethods;
using SolviaPfSenseConfigToDocx.Factory;
using System.Xml.Linq;

namespace SolviaPfSenseConfigToDocx.Parsers
{
    public class CronJobParser : IParser<List<CronJob>>
    {
        List<CronJob> IParser<List<CronJob>>.Parse(XElement element)
        {
            HtmlDecodeTextOnly(element);

            var cronJobs = new List<CronJob>();

            // Parse Cron Jobs
            foreach (var cronElement in element.Elements("item"))
            {
                var cronJob = new CronJob
                {
                    Minute = cronElement.Element("minute")?.Value ?? string.Empty,
                    Hour = cronElement.Element("hour")?.Value ?? string.Empty,
                    Command = cronElement.Element("command")?.Value ?? string.Empty,
                    MDay = cronElement.Element("mday")?.Value ?? string.Empty,
                    Month = cronElement.Element("month")?.Value ?? string.Empty,
                    WDay = cronElement.Element("wday")?.Value ?? string.Empty,
                    Who = cronElement.Element("who")?.Value ?? string.Empty
                };
                cronJobs.Add(cronJob);
            }
            return cronJobs;
        }

        public void HtmlDecodeTextOnly(XElement element)
        {
            element.HtmlDecodeTextOnly();
        }
    }
}
