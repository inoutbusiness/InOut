namespace InOut.API.Models.ResponsesDTOs
{
    public class ResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public object? Data { get; set; } = null;
    }
}