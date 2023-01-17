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

        public virtual T Create(T obj)
        {
            _inOutContext.Add(obj);
            _inOutContext.SaveChanges();
            return obj;
        }

        public virtual async Task<T> CreateAsync(T obj)
        {
            await _inOutContext.AddAsync(obj);
            await _inOutContext.SaveChangesAsync();
            return obj;
        }

        public virtual IEnumerable<T?> GetAllAsNoTracking()
            => _inOutContext.Set<T>()
                            .AsNoTracking()
                            .ToList();

        public virtual async Task<IEnumerable<T?>> GetAllAsNoTrackingAsync()
            => await _inOutContext.Set<T>()
                                  .AsNoTracking()
                                  .ToListAsync();

        public virtual T? GetByIdAsNoTracking(long id)
            =>  _inOutContext.Set<T>()
                             .AsNoTracking()
                             .Where(x => x.Id == id)
                             .FirstOrDefault();

        public virtual async Task<T?> GetByIdAsNoTrackingAsync(long id)
            => await _inOutContext.Set<T>()
                                  .AsNoTracking()
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();

        public virtual IEnumerable<T?> GetAll()
            =>  _inOutContext.Set<T>()
                             .ToList();

        public virtual async Task<IEnumerable<T?>> GetAllAsync()
            => await _inOutContext.Set<T>()
                                  .ToListAsync();

        public virtual T? GetById(long id)
            => _inOutContext.Set<T>()
                            .Where(x => x.Id == id)
                            .FirstOrDefault();

        public virtual async Task<T?> GetByIdAsync(long id)
            => await _inOutContext.Set<T>()
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();

        public virtual T Update(T obj)
        {
            _inOutContext.Entry(obj).State = EntityState.Modified;
            _inOutContext.SaveChanges();

            return obj;
        }

        public virtual async Task<T> UpdateAsync(T obj)
        {
            _inOutContext.Entry(obj).State = EntityState.Modified;
            await _inOutContext.SaveChangesAsync();

            return obj;
        }

        public virtual void Delete(long id)
        {
            var obj = GetById(id);

            if (obj != null)
            {
                _inOutContext.Remove(obj);
                _inOutContext.SaveChanges();
            }
        }

        public virtual async Task DeleteAsync(long id)
        {
            var obj = await GetByIdAsync(id);

            if (obj != null)
            {
                _inOutContext.Remove(obj);
                await _inOutContext.SaveChangesAsync();
            }
        }
    }
}