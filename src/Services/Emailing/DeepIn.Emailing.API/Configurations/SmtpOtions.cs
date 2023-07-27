namespace DeepIn.Emailing.API.Configurations
{
    public class SmtpOtions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
        public string ReplyTo { get; set; }
        public string Password { get; set; }
    }
}
