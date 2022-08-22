namespace InOut.API.Models
{
    public class ResetPasswordModel
    {
        public long AccountId { get; set; }
        public string NewPassword { get; set; }
    }
}
