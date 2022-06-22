namespace InOut.Service.Token.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(string accountEmail);
    }
}
