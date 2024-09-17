namespace SolviaPfSenseConfigToDocx.DataModels
{
    public class CronJob
    {
        public string Minute { get; set; }
        public string Hour { get; set; }
        public string MDay { get; set; }    // Day of the month
        public string Month { get; set; }   // Month
        public string WDay { get; set; }    // Day of the week
        public string Who { get; set; }     // User executing the command
        public string Command { get; set; } // Command to be executed
    }

}
