namespace InOut.Domain.DTOs
{
    public class EmailSenderRequest
    {
        public string Subject { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string RecoveryToken { get; set; }
        public EmailAuthenticateInfoDto AuthenticateInfo { get; set; }
    }
}