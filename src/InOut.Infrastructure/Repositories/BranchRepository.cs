using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InOut.Infrastructure.Repositories
{
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        private readonly InOutContext _context;

        public BranchRepository(InOutContext inOutContext) : base(inOutContext) 
        {
            _context = inOutContext;
        }

        public async Task<bool> ExistsByLocationId(long locationId)
            => await _context.Branches.AnyAsync(x => x.LocationId == locationId);
    }
}