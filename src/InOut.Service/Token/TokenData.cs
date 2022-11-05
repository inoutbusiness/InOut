namespace InOut.Service.Token
{
    public class TokenData
    {
        public string Token { get; set; }
        public decimal TokenExpirationTime { get; set; }
        public long UserId { get; set; }
    }
}