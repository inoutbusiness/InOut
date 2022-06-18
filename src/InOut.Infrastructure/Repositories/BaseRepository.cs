using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InOut.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly InOutContext _inOutContext;

        public BaseRepository(InOutContext inOutContext)
        {
            this._inOutContext = inOutContext;
        }

        public async Task<T> Create(T obj)
        {
            await _inOutContext.AddAsync(obj);
            await _inOutContext.SaveChangesAsync();
            return obj;
        }

        public async Task<IEnumerable<T?>> GetAllAsNoTracking()
            => await _inOutContext.Set<T>()
                                  .AsNoTracking()
                                  .ToListAsync();

        public async Task<T?> GetByIdAsNoTracking(long id)
            => await _inOutContext.Set<T>()
                                  .AsNoTracking()
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();

        public async Task<IEnumerable<T?>> GetAll()
            => await _inOutContext.Set<T>()
                                  .ToListAsync();

        public async Task<T?> GetById(long id)
            => await _inOutContext.Set<T>()
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();

        public async Task<T> Update(T obj)
        {
            _inOutContext.Entry(obj).State = EntityState.Modified;
            await _inOutContext.SaveChangesAsync();

            return obj;
        }

        public async Task Delete(long id)
        {
            var obj = await GetById(id);

            if (obj != null)
            {
                _inOutContext.Remove(obj);
                await _inOutContext.SaveChangesAsync();
            }
        }
    }
}