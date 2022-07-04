namespace InOut.Service.Token.Interfaces
{
    public interface ITokenGenerator
    {
        TokenData GenerateToken(long accountId);
    }
}
