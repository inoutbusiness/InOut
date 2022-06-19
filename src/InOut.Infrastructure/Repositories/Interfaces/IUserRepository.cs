using InOut.Domain.Entities;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> ExistsByCpfCnpj(string cpfCnpj);
    }
}