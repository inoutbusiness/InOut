namespace InOut.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ExistsByCpfCnpj(string cpfCnpj);
    }
}