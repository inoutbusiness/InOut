namespace InOut.Domain.DTOs
{
    public class EmailSenderDto
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}