namespace InOut.API.Models
{
    public class ResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public object? Data { get; set; }
    }
}
