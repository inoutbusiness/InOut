﻿using InOut.Domain.Entities;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IBranchRepository : IBaseRepository<Branch>
    {
        Task<bool> ExistsByLocationId(long locationId);
    }
}