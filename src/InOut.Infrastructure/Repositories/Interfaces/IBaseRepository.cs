using InOut.Domain.Entities;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);

        Task<T?> GetById(long id);

        Task<IEnumerable<T?>> GetAll();

        Task<T> Update(T obj);

        Task Delete(long id);
    }
}