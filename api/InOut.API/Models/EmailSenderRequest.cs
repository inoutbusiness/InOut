namespace InOut.API.Models
{
    public class EmailSenderRequest
    {
        public string Subject { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public EmailCodeConfigDto CodeConfig { get; set; }
        public EmailAuthenticateInfo AuthenticateInfo { get; set; }
    }
}
