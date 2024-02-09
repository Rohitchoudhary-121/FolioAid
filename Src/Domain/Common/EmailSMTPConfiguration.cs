namespace Domain.Common
{
    public class EmailSMTPConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string NetworkEmail { get; set; }
        public string NetworkPassword { get; set; }
        public string FromEmail { get; set; }
        public string BaseUrl { get; set; }
    }
}
