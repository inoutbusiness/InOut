using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InOut.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly IInOutContext _contextInOut;

        public BaseRepository(IInOutContext contextInOut)
        {
            this._contextInOut = contextInOut;
        }

        public async Task<T> Create(T obj)
        {
            await _contextInOut.AddAsync(obj);
            await _contextInOut.SaveChangesAsync();
            return obj;
        }

        public async Task<IEnumerable<T?>> GetAllAsNoTracking()
            => await _contextInOut.Set<T>()
                                  .AsNoTracking()
                                  .ToListAsync();

        public async Task<T?> GetByIdAsNoTracking(long id)
            => await _contextInOut.Set<T>()
                                  .AsNoTracking()
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();

        public async Task<IEnumerable<T?>> GetAll()
            => await _contextInOut.Set<T>()
                                  .ToListAsync();

        public async Task<T?> GetById(long id)
            => await _contextInOut.Set<T>()
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();

        public async Task<T> Update(T obj)
        {
            _contextInOut.Entry(obj).State = EntityState.Modified;
            await _contextInOut.SaveChangesAsync();

            return obj;
        }

        public async Task Delete(long id)
        {
            var obj = await GetById(id);

            if (obj != null)
            {
                _contextInOut.Remove(obj);
                await _contextInOut.SaveChangesAsync();
            }
        }
    }
}
