namespace InOut.API.Models
{
    public class VerifyEmailCodeModel
    {
        public string Email { get; set; }
        public string InputedToken { get; set; }
    }
}